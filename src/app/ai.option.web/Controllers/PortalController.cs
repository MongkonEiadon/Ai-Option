using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoption.data.Model;
using iqoption.data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ai.option.web.Controllers {
    [Authorize]
    public class PortalController : Controller {
        private readonly IIqOptionAccountService _iqOptionAccountService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PortalController(IMapper mapper,
            ILogger logger,
            IUserService userService,
            IIqOptionAccountService iqOptionAccountService) {
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
            _iqOptionAccountService = iqOptionAccountService;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> IqOptionAccount() {
            var models = await _iqOptionAccountService
                .GetIqOptionAccountsByUserIdAsync(HttpContext.User.Identity.Name)
                .ContinueWith(t => _mapper.Map<List<IqOptionAccountViewModel>>(t.Result));


            return View(models);
        }

        public async Task<IActionResult> AddIqOptionAccoutToUser(
            IqOptionRequestViewModel requestViewModel
        ) {
            return View("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddIqOptionAccountAsync(IqOptionRequestViewModel requestViewModel) {
            var dto = _mapper.Map<IqOptionAccountDto>(requestViewModel);
            var existingUser = await _iqOptionAccountService.GetAccountByUserIdAsync(dto.IqOptionUserId);

            if (existingUser == null) {
                dto.User = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
                dto.LastSyned = DateTime.Now;

                var result = await _iqOptionAccountService.CreateAccountTask(dto);
                _logger.LogDebug($"Create User-IqoptionUser Id: {result}");
            }
            else {
                existingUser = _mapper.Map(requestViewModel, existingUser);
                existingUser.LastSyned = DateTime.Now;

                var result = await _iqOptionAccountService.UpdateAccountTask(existingUser);
                _logger?.LogDebug($"Update accont id:{result?.Id}");
            }

            return RedirectToAction("IqOptionAccount", "Portal");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateIsActiveAsync(Guid iqoptionAccountId) {


            var dto = await _iqOptionAccountService.GetAccountByIdAsync(iqoptionAccountId);
            if (dto != null) {
                dto.IsActive = !dto.IsActive;
                dto.UpdatedOn = DateTime.Now;
                await _iqOptionAccountService.UpdateAccountTask(dto);
            }

            return RedirectToAction("IqOptionAccount", "Portal");
        }
    }
}