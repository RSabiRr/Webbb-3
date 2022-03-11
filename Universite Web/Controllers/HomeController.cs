using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Universite_Web.Data;
using Universite_Web.Models;

namespace Universite_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _contex;

        public HomeController(AppDbContext contex)
        {
            _contex = contex;
        }
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
