using DSW1_CLIENTE_EF_ALAN_OLAYA_EDGAR_FABIAN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DSW1_CLIENTE_EF_ALAN_OLAYA_EDGAR_FABIAN.Controllers
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

        public IActionResult Privacy()
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
