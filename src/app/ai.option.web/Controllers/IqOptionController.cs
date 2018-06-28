using System;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using iqopoption.core;
using iqoption.apiservice;
using iqoption.apiservice.Queries;
using iqoption.domain.IqOption.Command;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ai.option.web.Controllers {
    [EnableCors("CorsPolicy")]
    public class IqOptionController : Controller {
        private readonly IGetProfileCommandHandler _getProfileCommandHandler;
        private readonly ILogger<IqOptionController> _logger;
        private readonly ILoginCommandHandler _loginCommandHandler;
        private readonly IMapper _mapper;
        private readonly ISession _session;

        public IqOptionController(IMapper mapper,
            ISession session,
            ILogger<IqOptionController> logger,
            IGetProfileCommandHandler getProfileCommandHandler,
            ILoginCommandHandler loginCommandHandler) {
            _mapper = mapper;
            _session = session;
            _logger = logger;
            _getProfileCommandHandler = getProfileCommandHandler;
            _loginCommandHandler = loginCommandHandler;
        }


        public IActionResult Index() {
            return View();
        }

        public IActionResult IqOptionProfile(IqOptionRequestViewModel model) {
            return PartialView("IqOption/_IqOptionAccountViewPartial", model);
        }


        [HttpGet]
        public async Task<IActionResult> GetTokenAsync(IqOptionRequestViewModel requestViewModel) {
            if (string.IsNullOrEmpty(requestViewModel.EmailAddress) ||
                string.IsNullOrEmpty(requestViewModel.Password))
                return Ok();

            try {
                await _session
                    .Send(new LoginCommand(requestViewModel.EmailAddress, requestViewModel.Password))
                    .ContinueWith(t => {
                        if (t.Result.IsSuccess)
                            return _getProfileCommandHandler.RetreiveProfileQueryAsync(t.Result.Ssid);

                        throw new Exception(t.Result.Message);
                    })
                    .Unwrap()
                    .ContinueWith(t => {
                        requestViewModel.ProfileResponseViewModel =
                            _mapper.Map<IqOptionProfileResponseViewModel>(t.Result);
                        requestViewModel.IsPassed = true;
                    });


                return IqOptionProfile(requestViewModel);
            }

            catch (AggregateException arex) {
                _logger.LogWarning("GetToken Failed", arex.GetBaseException().Message);
                return BadRequest(arex.GetBaseException().Message);
            }

            catch (Exception ex) {
                _logger.LogCritical(nameof(IqOptionProfile), ex);
                return BadRequest(ex.Message);
            }
        }
    }
}