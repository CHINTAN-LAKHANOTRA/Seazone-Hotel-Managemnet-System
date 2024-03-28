using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class AmenitiesController : Controller
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            List<Amenities> amenities = new List<Amenities>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Amenities";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Amenities amenity = new Amenities
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                AmenityName = reader["amenityName"].ToString(),
                                Description = reader["description"].ToString(),
                                PhotoUrl1 = reader["photo_url1"].ToString(),
                                PhotoUrl2 = reader["photo_url2"].ToString(),
                                Price = Convert.ToInt32(reader["price"]),
                                ServiceType = reader["serviceType"].ToString()
                            };

                            amenities.Add(amenity);
                        }
                    }
                }
            }

            return View(amenities);
        }
    }
}
