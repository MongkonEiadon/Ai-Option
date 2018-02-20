using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iqoption.core.data;
using iqoption.data;
using iqoption.data.Model;
using iqoption.data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iqoption.webapi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Person> _personRepository;
        private readonly iqOptionContext _iqOptionContext;

        public HomeController(IUnitOfWork unitOfWork, IRepository<Person> personRepository, iqOptionContext iqOptionContext)
        {
            _unitOfWork = unitOfWork;
            _personRepository = personRepository;
            _iqOptionContext = iqOptionContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
