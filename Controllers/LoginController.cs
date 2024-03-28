using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Claims;

namespace YourProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            string query = "SELECT id, fullName FROM [dbo].[User] WHERE email = @Email AND password = @Password";
            int userId = 0;
            string fullName = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        userId = reader.GetInt32(0);
                        fullName = reader.GetString(1);
                    }
                }
            }

            if (userId > 0)
            {
                // Authentication successful
                var claims = new[] {
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home"); // Redirect to Index action of Home controller
            }
            else
            {
                // Authentication failed
                ViewBag.Error = "Invalid email or password. Please try again.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out
            return RedirectToAction("Index", "Home"); // Redirect to home page after logout
        }
    }
}
