using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class DiningController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            List<Dining> menuItems = FetchMenuItemsFromDatabase();
            return View(menuItems);
        }

        private List<Dining> FetchMenuItemsFromDatabase()
        {
            List<Dining> menuItems = new List<Dining>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM MENUS";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Dining menuItem = new Dining
                        {
                            Id = reader.GetInt32(0),
                            PhotoUrl = reader.GetString(1),
                            ItemName = reader.GetString(2),
                            Description = reader.GetString(3),
                            Origin = reader.GetString(4),
                            Category = reader.GetString(5),
                            Meal = reader.GetString(6),
                            Rating = (float)reader.GetDouble(7),
                            Price = reader.GetInt32(8)
                        };
                        menuItems.Add(menuItem);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return menuItems;
        }
    }
}
