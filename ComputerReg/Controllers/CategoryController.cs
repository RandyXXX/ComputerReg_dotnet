using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ComputerReg.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        IConfiguration configuration;
        public CategoryController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


    }
}
