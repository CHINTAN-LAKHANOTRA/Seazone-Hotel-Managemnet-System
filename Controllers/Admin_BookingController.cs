using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class Admin_BookingController : Controller
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM BOOKING";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Aadhar = reader["Aadhar"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            RoomType = reader["RoomType"].ToString(),
                            AddedServices = DeserializeAddedServices(reader["AddedServices"].ToString()),
                            CheckInDate = (DateTime)reader["CheckInDate"],
                            CheckOutDate = (DateTime)reader["CheckOutDate"],
                            Bill = (int)reader["Bill"] // Change this line
                        };

                        bookings.Add(booking);
                    }

                    reader.Close();
                }
            }

            return View(bookings);
        }

        // Method to deserialize JSON string to AddedServices list
        private List<string> DeserializeAddedServices(string addedServicesJson)
        {
            return addedServicesJson.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
