using System.Net;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.web.Models;
using iqoptionapi;
using Microsoft.AspNetCore.Mvc;

namespace iqoption.web.Controllers
{
    public abstract class IqOptionApiControllerBase : Controller
    {
        public virtual IActionResult BadRequestWithResult(string msg) =>
            BadRequest(new ApiErrorResponseViewModel(HttpStatusCode.BadRequest, msg));

        public virtual IActionResult OkRequestWithResult(object result) => Ok(result);
    }

    [Route("api/[controller]")]
    public class IqOptionController : IqOptionApiControllerBase {
        private readonly IIqOptionApi _apiClient;


        public IqOptionController() {
        }


        [HttpPost]
        public async Task<IActionResult> VerifyIqOptionUserNameAndPasswordAsync(string email, string password) {


            using (var _apiClient = new IqOptionApi(email, password)) {

                if (await _apiClient.ConnectAsync()) {
                    return Ok();
                }
            }

            return Ok();
        }
    }

   
}