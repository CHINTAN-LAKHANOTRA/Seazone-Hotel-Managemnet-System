using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class Admin_AmenitiesController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // Action method to display the list of amenities
        public IActionResult Index()
        {
            try
            {
                List<Amenities> amenitiesList = GetAmenitiesFromDatabase();
                return View(amenitiesList);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error: {ex.Message}");

                // Return an error view or redirect to an error page
                return RedirectToAction("Error", "Home");
            }
        }

        // Action method to display the form for adding amenities
        public IActionResult Add_Amenities()
        {
            return View();
        }

        // Action method to handle the addition of amenities
        [HttpPost]
        public IActionResult Add(Amenities amenities)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"INSERT INTO AMENITIES (amenityName, description, photo_url1, photo_url2, price, serviceType) 
                                         VALUES (@AmenityName, @Description, @PhotoUrl1, @PhotoUrl2, @Price, @ServiceType)";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@AmenityName", amenities.AmenityName);
                        command.Parameters.AddWithValue("@Description", amenities.Description);
                        command.Parameters.AddWithValue("@PhotoUrl1", amenities.PhotoUrl1 ?? "");
                        command.Parameters.AddWithValue("@PhotoUrl2", amenities.PhotoUrl2 ?? "");
                        command.Parameters.AddWithValue("@Price", amenities.Price);
                        command.Parameters.AddWithValue("@ServiceType", amenities.ServiceType ?? "");

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to add amenities. No rows affected.");
                            return View(amenities);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine($"Error: {ex.Message}");

                    // Return an error view or redirect to an error page
                    ModelState.AddModelError("", "Failed to add amenities. Please try again later.");
                    return View(amenities);
                }
            }
            else
            {
                return View(amenities);
            }
        }

        // Action method to display the form for updating amenities
        public IActionResult Update_Amenities()
        {
            // Logic to retrieve amenities for update
            return View();
        }

        // Action method to handle the update of amenities
        [HttpPost]
        public IActionResult Update(Amenities amenities)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"UPDATE AMENITIES 
                                         SET amenityName = @AmenityName, 
                                             description = @Description, 
                                             photo_url1 = @PhotoUrl1, 
                                             photo_url2 = @PhotoUrl2, 
                                             price = @Price, 
                                             serviceType = @ServiceType 
                                         WHERE Id = @Id";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", amenities.Id);
                        command.Parameters.AddWithValue("@AmenityName", amenities.AmenityName);
                        command.Parameters.AddWithValue("@Description", amenities.Description);
                        command.Parameters.AddWithValue("@PhotoUrl1", amenities.PhotoUrl1 ?? "");
                        command.Parameters.AddWithValue("@PhotoUrl2", amenities.PhotoUrl2 ?? "");
                        command.Parameters.AddWithValue("@Price", amenities.Price);
                        command.Parameters.AddWithValue("@ServiceType", amenities.ServiceType ?? "");

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Failed to update amenities. No rows affected.");
                            return View(amenities);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine($"Error: {ex.Message}");

                    // Return an error view or redirect to an error page
                    ModelState.AddModelError("", "Failed to update amenities. Please try again later.");
                    return View(amenities);
                }
            }
            else
            {
                return View(amenities);
            }
        }

        // Action method to display the form for deleting amenities
        public IActionResult Delete_Amenities()
        {
            // Logic to retrieve amenities for deletion
            return View();
        }

        // Action method to handle the deletion of amenities
        [HttpPost]
        public IActionResult DeleteAmenities(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM AMENITIES WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to delete amenities. No rows affected.");
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error: {ex.Message}");

                // Return an error view or redirect to an error page
                ModelState.AddModelError("", "Failed to delete amenities. Please try again later.");
                return View();
            }
        }

        // Action method to retrieve amenities from the database
        private List<Amenities> GetAmenitiesFromDatabase()
        {
            List<Amenities> amenitiesList = new List<Amenities>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM AMENITIES";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

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

                    amenitiesList.Add(amenity);
                }
            }

            return amenitiesList;
        }
    }
}
