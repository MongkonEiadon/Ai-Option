using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using iqoption.data.Model;
using iqoption.web.Models;
using iqoption.web.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace iqoption.web.Controllers
{
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public TokenController(
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager) {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        [Route("/token")]
        public async Task<IActionResult> Create(LoginViewModel viewModel) {
            var validation = new LoginViewModelValidation().Validate(viewModel);
            if (!validation.IsValid)
            {
                ViewData["ErrorMessage"] = validation.Errors.GetErrorDescriptions();

                return BadRequest();
            }

            var _user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == viewModel.Email);


            return Ok(GenerateJwtToken(viewModel.Email, _user));

        }

        private object GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: DateTime.Now.AddMilliseconds(30),
                signingCredentials: creds);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new
            {
                access_token = encodedJwt,
                refresh_token = Guid.NewGuid().ToString()
            };
        }
    }
}
