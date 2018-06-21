using System;
using System.Text;
using System.Threading.Tasks;
using ai.option.web.Models;
using ai.option.web.ViewModels;
using AutoMapper;
using iqoptionapi;
using Microsoft.AspNetCore.Mvc;

namespace ai.option.web.Controllers {
    public class IqOptionController : Controller {
        private readonly IMapper _mapper;

        public IqOptionController(IMapper mapper) {
            _mapper = mapper;
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
                var api = new IqOptionApi(requestViewModel.EmailAddress, requestViewModel.Password);

                var token = await api.GetTokenAsync()
                    .ContinueWith(t => api.GetProfileAsync())
                    .Unwrap();


                requestViewModel.ProfileResponseViewModel = _mapper.Map<IqOptionProfileResponseViewModel>(token);
                requestViewModel.IsPassed = true;

                return IqOptionProfile(requestViewModel);
            }
            catch (Exception ex) {
                requestViewModel.IsPassed = false;
                requestViewModel.Temp = ex.Message;
                return IqOptionProfile(requestViewModel);
            }
        }
        
    }
}