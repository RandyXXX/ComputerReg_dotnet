using ComputerReg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerReg.Controllers
{
    public class AAAAController : Controller
    {
        private readonly ILogger<AAAAController> _logger;

        public AAAAController(ILogger<AAAAController> logger)
        {
            _logger = logger;
        }

        public IActionResult test()
        {
            return View();
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
