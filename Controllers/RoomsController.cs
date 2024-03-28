using Microsoft.AspNetCore.Mvc;
using Seazone.Models; // Import the namespace where the Room class is defined
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class RoomsController : Controller
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // GET: Rooms
        public IActionResult Index()
        {
            var rooms = new List<Room>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ROOMS";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var room = new Room
                            {
                                Id = (int)reader["Id"],
                                BedType = reader["bedType"].ToString(),
                                RoomType = reader["roomType"].ToString(),
                                Status = reader["status"].ToString(),
                                Description = reader["description"].ToString(),
                                PhotoUrl = reader["photo_url"].ToString(),
                                Features = reader["features"].ToString(),
                                PhotoUrl2 = reader["photo_url2"].ToString()
                            };

                            // Handle conversion errors for numeric fields
                            if (int.TryParse(reader["totalRooms"].ToString(), out int totalRooms))
                                room.TotalRooms = totalRooms;

                            if (int.TryParse(reader["capacity"].ToString(), out int capacity))
                                room.Capacity = capacity;

                            if (int.TryParse(reader["price"].ToString(), out int price))
                                room.Price = price;

                            rooms.Add(room);
                        }
                    }
                }
            }

            return View(rooms);
        }
    }
}
