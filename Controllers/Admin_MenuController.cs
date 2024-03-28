using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Seazone.Models;

namespace Seazone.Controllers
{
    public class Admin_MenuController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            List<Dining> menus = new List<Dining>();

            string query = "SELECT id, item_name, origin, category, price FROM MENUS";

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
                                Dining menu = new Dining
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    ItemName = reader["item_name"].ToString(),
                                    Origin = reader["origin"].ToString(),
                                    Category = reader["category"].ToString(),
                                    Price = Convert.ToInt32(reader["price"])
                                };

                                menus.Add(menu);
                            }
                        }
                    }
                }

                return View(menus);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error occurred while retrieving menu items: " + ex.Message;
                return View();
            }
        }
    }
}
