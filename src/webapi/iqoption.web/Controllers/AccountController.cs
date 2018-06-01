using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iqoption.core.Extensions;
using iqoption.data.Model;
using iqoption.web.Models;
using iqoption.web.Validations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Rest.Azure;

namespace iqoption.web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserDto> _userManager;
        private readonly SignInManager<UserDto> _signInManager;
        

        public AccountController(
            IConfiguration configuration,
            UserManager<UserDto> userManager,
            SignInManager<UserDto> signInManager
        ) {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string ReturnUrl { get; set; }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl= "") {
            ReturnUrl  = returnUrl;
            return View(nameof(Login));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var validation = new LoginViewModelValidation().Validate(viewModel);
            if (!validation.IsValid) {
                ViewData["Failed"] = validation.Errors.GetErrorDescriptions();

                return View();
            }
            
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, false);

            if (result.Succeeded) {
                var _user = _userManager.Users.SingleOrDefault(x => x.Email == viewModel.Email);

                var claims = new List<Claim>() {
                    new Claim(ClaimTypes.Email, viewModel.Email),
                    new Claim(ClaimTypes.NameIdentifier, _user.Id)
                };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(userIdentity);
                var authProperties = new AuthenticationProperties();
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principle, 
                    authProperties);

                var dashBoardViewModel = new DashboardViewModel() {
                    LogInUser = new UserViewModel() {Email = viewModel.Email, UserId = _user.Id}
                };

                if(string.IsNullOrEmpty(ReturnUrl))
                    return RedirectToAction(nameof(DashboardController.Index), "Dashboard", dashBoardViewModel);
                else {
                    return Redirect(ReturnUrl);
                }
            }

            ViewData["Failed"] = "ไม่สามารถเข้าสู่ระบบได้ กรุณาตรวจสอบ email และ password อีกครั้ง!";
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel viewModel) {

            var user = new UserDto() {
                Email = viewModel.Email,
                UserName = viewModel.Email
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded) {
                viewModel.Password = string.Empty;

                ViewData["Success"] = $"ลงทะเบียนด้วย {user.Email} สำเร็จ, คุณสามารถเข้าสู่ระบบเพื่อเริ่มต้นใช้งานได้เลย!";


                return View("Register");
            }

            TempData["Failed"] = "ไม่สามารถสร้างผู้ใช้งานใหม่ได้!";

            return View();
        }
   
    }
}
