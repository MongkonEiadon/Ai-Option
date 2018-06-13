using System.Threading.Tasks;
using ai.option.web.ViewModels;
using iqoptionapi;
using Microsoft.AspNetCore.Mvc;

namespace ai.option.web.Controllers {
    public class IqOptionController : Controller {
        
        public IqOptionController() {

        }


        public IActionResult Index() {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetTokenAsync([Bind("EmailAddress,Password")] IqOptionRequest request) { 
            var response = new IqOptionResponse();

            using (var api = new IqOptionApi(request.EmailAddress, request.Password)) {

                var token = await api.GetTokenAsync();

                response.Token = token;
            }

            return Index();
        }
    }
}

