using Microsoft.AspNetCore.Mvc;
using Seazone.Models;

namespace Seazone.Controllers
{
    public class Admin_DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = new Admin_Dashboard();
            model.SelectedContainer = "default"; // Set default value or fetch from session/query string/etc.
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeContainer(string container)
        {
            // Process the container parameter as needed
            // For example, you could set it in a session variable
            HttpContext.Session.SetString("SelectedContainer", container);

            // Return JSON response indicating success or any other data
            return Json(new { success = true });
        }
    }
}
