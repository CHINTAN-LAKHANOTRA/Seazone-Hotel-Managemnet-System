using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class ContactController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            Message message = new Message(); // Create a new instance of Message
            return View(message); // Pass the message to the view
        }

        [HttpPost]
        public IActionResult Index(Message message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO MESSAGES (fullName, email, subject, message, status, date) VALUES (@FullName, @Email, @Subject, @MessageContent, 'Pending', GETDATE())";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FullName", message.FullName);
                        command.Parameters.AddWithValue("@Email", message.Email);
                        command.Parameters.AddWithValue("@Subject", message.Subject);
                        command.Parameters.AddWithValue("@MessageContent", message.MessageContent);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Redirect to a confirmation page or do something else
                    return RedirectToAction("Confirmation");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    // Handle error, maybe return to the form with an error message
                    return View(message);
                }
            }

            // If the model state is not valid, return to the form page with errors
            return View(message);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
