using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using iqoption.data.Services;
using iqoption.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iqoption.web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IPersonService _personService;

        public DashboardController(IPersonService personService) {
            _personService = personService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}