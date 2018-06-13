using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ai.option.web.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<UserDto> _userManager;
        private readonly SignInManager<UserDto> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<UserDto> userManager,
            SignInManager<UserDto> signInManager,
            IMapper mapper,
            ILogger logger) {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null) => View();
        

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInAsync(LoginViewModel loginViewModel) {

            var returnUrl = ViewData["ReturnUrl"]?.ToString();

            var result = await 
                _signInManager.PasswordSignInAsync(loginViewModel.EmailAddress, loginViewModel.Password, true, false);

            if (result.Succeeded) {
                return !string.IsNullOrEmpty(returnUrl)
                    ? RedirectToAction(returnUrl)
                    : RedirectToAction("Index", "AiOptionPortal");
            }


            ViewData["ErrorMessage"] = "Login failed!";
            return View("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register() => View("Register");

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(LoginViewModel model) {

            if (ModelState.IsValid) {
                var user = _mapper.Map<UserDto>(model);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    return await LogInAsync(model);
                }

                else {
                    ModelState.AddModelError("ErrorMessage", string.Join(", ",result.Errors.Select(x => x.Description)));
                }
            }


            return View("Register");
        }

        //
        // POST: /Login/LogOut
        [Authorize]
        public async Task<IActionResult> LogOut() {

            await _signInManager.SignOutAsync();

            _logger.LogInformation(4, "logged out");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}