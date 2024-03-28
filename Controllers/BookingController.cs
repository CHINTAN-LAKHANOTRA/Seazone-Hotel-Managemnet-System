using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace Seazone.Controllers
{
    public class BookingController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            var model = new Booking();
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            return View(model);
        }
        [HttpPost]
        public IActionResult SubmitBooking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Format CheckInDate and CheckOutDate as 'YYYY-MM-DD'
                    string checkInDateStr = booking.CheckInDate.ToString("yyyy-MM-dd");
                    string checkOutDateStr = booking.CheckOutDate.ToString("yyyy-MM-dd");

                    // Convert amount to decimal
                    decimal amount = Convert.ToDecimal(Request.Form["amount"]);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Proceed with the new booking
                        string query = "INSERT INTO BOOKING (Name, Email, Aadhar, Phone, RoomType, AddedServices, CheckInDate, CheckOutDate, Bill) " +
                                       "VALUES (@Name, @Email, @Aadhar, @Phone, @RoomType, @AddedServices, @CheckInDate, @CheckOutDate, @Bill);";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Name", booking.Name);
                        command.Parameters.AddWithValue("@Email", booking.Email);
                        command.Parameters.AddWithValue("@Aadhar", booking.Aadhar);
                        command.Parameters.AddWithValue("@Phone", booking.Phone);
                        command.Parameters.AddWithValue("@RoomType", booking.RoomType);
                        command.Parameters.AddWithValue("@AddedServices", JsonSerializer.Serialize(booking.AddedServices));
                        command.Parameters.AddWithValue("@CheckInDate", checkInDateStr);
                        command.Parameters.AddWithValue("@CheckOutDate", checkOutDateStr);
                        command.Parameters.AddWithValue("@Bill", amount);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Success");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to book room.");
                            return BadRequest(ModelState);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details for debugging purposes
                    Console.WriteLine("Exception occurred: " + ex.ToString());

                    ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        public IActionResult Success(Booking booking)
        {
            ViewBag.SuccessMessage = "Room booked successfully!";
            return View(booking);
        }
    }
}
