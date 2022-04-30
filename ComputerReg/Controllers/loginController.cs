﻿using ComputerReg.Models;
using DataLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerReg.Controllers
{
    public class loginController : Controller
    {
        private readonly ILogger<loginController> _logger;
        
        IConfiguration configuration;

        public loginController(ILogger<loginController> logger)
        {
            _logger = logger;
        }

        public loginController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public IActionResult Index()
        {
            return PartialView();
        }


        [HttpPost]
        public outLoginMoudle SetLogin(inLogin inO)
        {

            clsBusiness clsData = new clsBusiness(configuration.GetValue<string>("ConnectionStrings:sqlConnect"));

            outLoginMoudle lm = new outLoginMoudle();






            return lm;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}