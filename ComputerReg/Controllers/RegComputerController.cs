using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ComputerReg.Controllers
{
    [Route("[controller]")]
    public class RegComputerController : Controller
    {
        IConfiguration configuration;
        public RegComputerController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public outCategory DataHandler(inCategory inU)
        //{

        //}
    }
}
