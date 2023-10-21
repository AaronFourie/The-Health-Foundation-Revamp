using Chaotic_Nature_XISD6329.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chaotic_Nature_XISD6329.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Archives()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Projects()
        {
            return View();
        }
        public IActionResult Support()
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