using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using iqoption.data.Model;
using iqoption.webapi.Validations;
using iqoption.webapi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace iqoption.webapi.Controllers {
    [Route("[controller]/[action]")]
    public class AuthorizationController : IqOptionApiControllerBase {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthorizationController(
            IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager
        ) {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel query) {
            var result = await _signInManager.PasswordSignInAsync(query.Email, query.Password, false, false);
            if (result.Succeeded) {
                var _user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == query.Email);
                var token = GenerateJwtToken(query.Email, _user);

                return Ok(token);
            }

            return new UnauthorizedResult();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] LoginViewModel query) {
            try {
                var validation = new UserRegisterValidation().Validate(query);
                if (!validation.IsValid)
                    return BadRequestWithResult(validation.Errors.GetErrorDescriptions());

                var user = new User {
                    UserName = query.Email,
                    Email = query.Email
                };

                var result = await _userManager.CreateAsync(user, query.Password);
                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, false);

                    return OkRequestWithResult(GenerateJwtToken(query.Email, user));
                }
                else {
                    return BadRequest(string.Join(", ", result.Errors.Select(x => x.Description).Distinct()));
                }

                throw new Exception("Can't login!");
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private object GenerateJwtToken(string email, User user) {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                expires: DateTime.Now.AddMilliseconds(30),
                signingCredentials: creds);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            return new {
                access_token = encodedJwt,
                refresh_token = Guid.NewGuid().ToString()
            };

        }
    }
}