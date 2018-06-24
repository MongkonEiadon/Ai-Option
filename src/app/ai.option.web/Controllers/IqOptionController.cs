using System;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using ai.option.web.Models;
using ai.option.web.ViewModels;
using AutoMapper;
using Castle.Core.Logging;
using iqoption.apiservice;
using iqoption.apiservice.Queries;
using iqoption.domain.IqOption.Command;
using iqoption.domain.IqOption.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ai.option.web.Controllers {


    [EnableCors("CorsPolicy")]
    public class IqOptionController : Controller {
        private readonly IMapper _mapper;
        private readonly ILogger<IqOptionController> _logger;
        private readonly IGetProfileCommandHandler _getProfileCommandHandler;
        private readonly ILoginCommandHandler _loginCommandHandler;

        public IqOptionController(IMapper mapper,
            ILogger<IqOptionController> logger,
            IGetProfileCommandHandler getProfileCommandHandler, 
            ILoginCommandHandler loginCommandHandler) {
            _mapper = mapper;
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

                await _loginCommandHandler
                    .ExecuteAsync(new LoginCommand(requestViewModel.EmailAddress, requestViewModel.Password))
                    .ContinueWith(t => _getProfileCommandHandler.RetreiveProfileQueryAsync(t.Result))
                    .Unwrap()
                    .ContinueWith(t => {
                        requestViewModel.ProfileResponseViewModel = _mapper.Map<IqOptionProfileResponseViewModel>(t.Result);
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