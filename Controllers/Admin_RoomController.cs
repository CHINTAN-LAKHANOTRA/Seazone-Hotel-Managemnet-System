using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Seazone.Models;

namespace Seazone.Controllers
{
    public class Admin_RoomController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            List<Room> rooms = new List<Room>();

            string query = "SELECT Id, bedType, roomType, capacity, price FROM ROOMS";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Room room = new Room
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    BedType = reader["bedType"].ToString(),
                                    RoomType = reader["roomType"].ToString(),
                                    Capacity = Convert.ToInt32(reader["capacity"]),
                                    Price = Convert.ToInt32(reader["price"])
                                };

                                rooms.Add(room);
                            }
                        }
                    }
                }

                return View(rooms);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error occurred while retrieving room data: " + ex.Message;
                return View();
            }
        }
    }
}
