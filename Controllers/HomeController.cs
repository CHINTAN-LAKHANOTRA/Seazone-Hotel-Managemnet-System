using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Diagnostics;

namespace Seazone.Controllers
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

        // Logout action
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Index", "Home"); // Redirect to home page after logout
        }
    }
}
