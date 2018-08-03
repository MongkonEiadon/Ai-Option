using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using ai.option.web.ViewModels;
using AutoMapper;
using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Queries;
using iqoption.apiservice;
using iqoption.domain.IqOption;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Commands;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ai.option.web.Controllers {

    [EnableCors("CorsPolicy")]
    public class IqOptionController : Controller {
        private readonly ILogger<IqOptionController> _logger;
        private readonly IQueryProcessor _queryProcessor;
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;

        public IqOptionController(IMapper mapper,
            ILogger<IqOptionController> logger,
            IQueryProcessor queryProcessor ,ICommandBus commandBus) {
            _mapper = mapper;
            _logger = logger;
            _queryProcessor = queryProcessor;
            _commandBus = commandBus;
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
                var loginObs = await _commandBus.PublishAsync(new IqLoginCommand(IqOptionIdentity.New, requestViewModel.EmailAddress, requestViewModel.Password), default(CancellationToken));
                if (loginObs.IsSuccess) {

                    var profile = await _queryProcessor.ProcessAsync(new GetProfileQuery(loginObs.Ssid), CancellationToken.None)
                        .ContinueWith(t => _mapper.Map<IqOptionProfileResponseViewModel>(t.Result.ProfileResult));

                    requestViewModel.ProfileResponseViewModel = profile;
                    requestViewModel.IsPassed = true;
                    requestViewModel.ProfileResponseViewModel.Ssid = loginObs.Ssid;
                    requestViewModel.ProfileResponseViewModel.SsidUpdated = DateTime.Now;

                    return IqOptionProfile(requestViewModel);
                }

                throw new Exception(loginObs.Message);

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