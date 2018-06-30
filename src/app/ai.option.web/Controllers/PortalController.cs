using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using EventFlow;
using EventFlow.Queries;
using iqoption.apiservice;
using iqoption.data.Model;
using iqoption.data.Services;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace ai.option.web.Controllers {
    [Authorize]
    public class PortalController : Controller {
        private readonly IIqOptionAccountService _iqOptionAccountService;
        private readonly ILogger _logger;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public PortalController(IMapper mapper,
            ILogger logger,
            IQueryProcessor queryProcessor,
            ICommandBus commandBus,
            IUserService userService,
            IIqOptionAccountService iqOptionAccountService) {
            _mapper = mapper;
            _logger = logger;
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
            _userService = userService;
            _iqOptionAccountService = iqOptionAccountService;
        }

        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> IqOptionAccount() {

            var iqoptionUsers = await _queryProcessor.ProcessAsync(
                new GetIqOptionAccountByUserIdQuery(HttpContext.User.Identity.Name), default(CancellationToken));

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
        [AllowAnonymous]
        [Route("TokenAsync")]
        public async Task<IActionResult> PostTokenAsync() {
            var client = new RestClient("https://auth.iqoption.com/api/v1.0/login");
            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("content-type", "multipart/form-data");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("email", "mongkon.eiadon@gmail.com", ParameterType.QueryString);
            request.AddParameter("password", "Code11054", ParameterType.QueryString);

            var result = await client.ExecuteTaskAsync(request);
            return Ok(new {
                server = result.Server,
                statuscode = result.StatusCode,
                cookies = result.Cookies,
                message = result.Content,
                issuccessful = result.IsSuccessful,
                errorMessage = result.ErrorMessage,
                exception = result.ErrorException,
                headers = result.Headers
            });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddIqOptionAccountAsync(IqOptionRequestViewModel requestViewModel) {
            var iqoptionAccount = _mapper.Map<IqAccount>(requestViewModel);
            var result = await _commandBus.PublishAsync(new CreateOrUpdateIqAccountCommand(IqOptionIdentity.New, iqoptionAccount, HttpContext.User.Identity.Name), CancellationToken.None);
            
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