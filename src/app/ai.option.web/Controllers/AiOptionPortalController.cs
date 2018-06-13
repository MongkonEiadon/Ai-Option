using System.Threading.Tasks;
using ai.option.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ai.option.web.Controllers {

    [Authorize]
    public class AiOptionPortalController : Controller {

        public AiOptionPortalController() {

        }

        public IActionResult Index() {
            return View();
        }




        public async Task<IActionResult> AddIqOptionAccountAsync(IqOptionRequest  request) {


            return View("Index");
        }
    }
}