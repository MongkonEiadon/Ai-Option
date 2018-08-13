using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using EventFlow;
using EventFlow.Queries;
using iqoption.data.Services;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using iqoption.domain.IqOption.Queries;
using iqoption.domain.Users;
using iqoption.domain.Users.Commands;
using iqoption.domain.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace ai.option.web.Controllers {
    [Authorize]
    public class PortalController : Controller {
        private readonly ICommandBus _commandBus;
        private readonly IIqOptionAccountService _iqOptionAccountService;
        private readonly IMapper _mapper;
        private readonly IQueryProcessor _queryProcessor;

        public PortalController(IMapper mapper,
            ILogger logger,
            IQueryProcessor queryProcessor,
            ICommandBus commandBus,
            IUserService userService,
            IIqOptionAccountService iqOptionAccountService) {
            _mapper = mapper;
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
            _iqOptionAccountService = iqOptionAccountService;
        }

        public IActionResult Index() => View();

        [HttpGet]
        [Authorize]
        public IActionResult IqOptionAccount() => View();

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetIqOptionAccountTask() {
            var iqoptionUsers = await _queryProcessor.ProcessAsync(
                new GetIqOptionAccountByAiOptionUserIdQuery(HttpContext.User.Identity.Name), default(CancellationToken));

            var models = await _iqOptionAccountService
                .GetIqOptionAccountsByUserIdAsync(HttpContext.User.Identity.Name)
                .ContinueWith(t => _mapper.Map<List<IqOptionAccountViewModel>>(t.Result));


            return PartialView("Portal/_IqOptionProfileResponsePartial", models);
        }

        public IActionResult AddIqOptionAccoutToUser(IqOptionRequestViewModel requestViewModel) => View("Index");
        


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
            var result = await _commandBus.PublishAsync(
                new CreateOrUpdateIqAccountCommand(IqIdentity.New, iqoptionAccount,
                    HttpContext?.User?.Identity?.Name), CancellationToken.None);
            if (result.IsSuccess) {

                //use command to update
                await _commandBus.PublishAsync(
                    new SetActiveAccountcommand(IqIdentity.New,
                        new ActiveAccountItem(true, (int) requestViewModel.ProfileResponseViewModel.UserId)),
                    CancellationToken.None);

                return RedirectToAction("IqOptionAccount", "Portal");
            }

            return BadRequest("ไม่สามารถเพิ่มบัญชีได้");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteIqOptionAccoutAsync(string iqAccountId) {
            var result = await _commandBus.PublishAsync(
                new DeleteIqAccountCommand(IqIdentity.New, Guid.Parse(iqAccountId)),
                CancellationToken.None);

            if (result.IsSuccess) return Ok();

            return BadRequest("ไม่สามารถลบบัญชีได้");
        }


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateIsActiveAsync(string iqAccountId) {


            var dto = await _iqOptionAccountService.GetAccountByIdAsync(Guid.Parse(iqAccountId));
            if (dto != null) {

                await _commandBus.PublishAsync(
                    new SetActiveAccountcommand(IqIdentity.New, new ActiveAccountItem(!dto.IsActive, dto.IqOptionUserId)), CancellationToken.None);
                
            } return Ok();
        }
        

        #region AccountManangement

        public IActionResult AccountManagement() => View();

        [HttpGet]
        [Authorize(Roles = UserLevel.Administrator)]
        public async Task<IActionResult> AccountManagementGetAsync() {

            var users = await _queryProcessor.ProcessAsync(
                new UsersLevelQuery(), CancellationToken.None);

            if (users.IsSuccess) {
                return PartialView("Portal/_TableRowUserLevel", users.Users);
            }

            return BadRequest("Get users not success!");
        }

        [HttpDelete]
        [Authorize(Roles = UserLevel.Administrator)]
        public async Task<IActionResult> DeleteAccountAsync(string id) {

            var deleteResult = await _commandBus.PublishAsync(new DeleteUserCommand(UserIdentity.New, id), CancellationToken.None);

            if (deleteResult.IsSuccess) {
                return Ok();
            }

            return BadRequest($"Can't delete account with id : {id}!, with {deleteResult.Message}");
        }

        [HttpPut]
        [Authorize(Roles = UserLevel.Administrator)]
        public async Task<IActionResult> ChangeUserLevelAsync(string id, string level) {

            var changeLevelResult =
                await _commandBus.PublishAsync(new ChangeUserRoleCommand(UserIdentity.New, id, level),
                    CancellationToken.None);

            if (changeLevelResult.IsSuccess) return Ok();
            return BadRequest(changeLevelResult.Message);

        }


        #endregion
    }
}