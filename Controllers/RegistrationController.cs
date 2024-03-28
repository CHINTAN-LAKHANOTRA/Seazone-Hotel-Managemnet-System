using Microsoft.AspNetCore.Mvc;

namespace Seazone.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserRegistration user)
        {
            if (ModelState.IsValid)
            {
                UserRegistration userData = new UserRegistration();
                bool isInserted = userData.Insert(user);

                if (isInserted)
                {
                    TempData["msg"] = "Added successfully";
                    return RedirectToAction("Index", "Home"); // Redirect to a success page or another appropriate action
                }
                else
                {
                    TempData["msg"] = "Not Added. Something went wrong..!!";
                }
            }

            // If ModelState is not valid or insertion fails, return to the registration form with the entered data
            return View(user);
        }
    }
}
