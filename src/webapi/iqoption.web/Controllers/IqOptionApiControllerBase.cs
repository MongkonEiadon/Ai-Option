using System.Net;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.web.Models;
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


        public IqOptionController() {

        }

        [HttpGet()]
        public IActionResult GetAvailableTradersAsync() {



            return Ok("");
        }
    }

   
}