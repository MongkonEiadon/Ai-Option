using System.Net;
using iqoption.webapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iqoption.webapi.Controllers
{
    public abstract class IqOptionApiControllerBase : Controller
    {
        public virtual IActionResult BadRequestWithResult(string msg) =>
            BadRequest(new ApiErrorResponseViewModel(HttpStatusCode.BadRequest, msg));

            public virtual IActionResult OkRequestWithResult(object result) => Ok(result);
    }
}