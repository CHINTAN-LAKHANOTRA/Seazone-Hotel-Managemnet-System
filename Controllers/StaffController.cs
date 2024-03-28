using Microsoft.AspNetCore.Mvc;
using Seazone.Models;
using System.Data.SqlClient;

namespace Seazone.Controllers
{
    public class StaffController : Controller
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public IActionResult Index()
        {
            List<Staff> staffList = new List<Staff>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM STAFF";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Staff staff = new Staff
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = reader["fullName"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Password = reader["password"].ToString(),
                        Gender = reader["gender"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        JoiningDate = Convert.ToDateTime(reader["joiningDate"]),
                        UserType = reader["user_type"].ToString(),
                        TshId = reader["Staff_no"].ToString(),
                        Designation = reader["Designation"].ToString(),
                        Salary = Convert.ToInt32(reader["Salary"])
                    };
                    staffList.Add(staff);
                }
                reader.Close();
            }

            return View(staffList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Staff staff)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"INSERT INTO STAFF (fullName, email, phone, password, gender, DOB, joiningDate, user_type, Designation, Salary) 
                                     VALUES (@FullName, @Email, @Phone, @Password, @Gender, @DOB, @JoiningDate, @UserType, @Designation, @Salary)";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FullName", staff.FullName);
                        command.Parameters.AddWithValue("@Email", staff.Email);
                        command.Parameters.AddWithValue("@Phone", staff.Phone);
                        command.Parameters.AddWithValue("@Password", staff.Password);
                        command.Parameters.AddWithValue("@Gender", staff.Gender);
                        command.Parameters.AddWithValue("@DOB", staff.DOB);
                        command.Parameters.AddWithValue("@JoiningDate", staff.JoiningDate);
                        command.Parameters.AddWithValue("@UserType", staff.UserType ?? ""); // Ensure UserType is not null
                        command.Parameters.AddWithValue("@Designation", staff.Designation ?? ""); // Ensure Designation is not null
                        command.Parameters.AddWithValue("@Salary", staff.Salary);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to add staff. Please try again."); // Add error message to ModelState
                    return View(staff); // Return the view with model to display validation errors
                }
            }
            return View(staff); // Return the view with model if ModelState is not valid
        }

        public IActionResult Update(int id)
        {
            // Retrieve staff data by id and pass it to the Update view
            // Example: Staff staff = GetStaffById(id);
            // return View(staff);
            return View();
        }

        [HttpPost]
        public IActionResult Update(Staff staff)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"UPDATE STAFF SET fullName = @FullName, email = @Email, phone = @Phone, password = @Password, 
                 gender = @Gender, DOB = @DOB, joiningDate = @JoiningDate, user_type = @UserType, 
                 Designation = @Designation, Salary = @Salary WHERE Staff_no = @Stno";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@FullName", staff.FullName);
                        command.Parameters.AddWithValue("@Email", staff.Email);
                        command.Parameters.AddWithValue("@Phone", staff.Phone);
                        command.Parameters.AddWithValue("@Password", staff.Password);
                        command.Parameters.AddWithValue("@Gender", staff.Gender);
                        command.Parameters.AddWithValue("@DOB", staff.DOB);
                        command.Parameters.AddWithValue("@JoiningDate", staff.JoiningDate);
                        command.Parameters.AddWithValue("@UserType", staff.UserType ?? "");
                        command.Parameters.AddWithValue("@Designation", staff.Designation ?? "");
                        command.Parameters.AddWithValue("@Salary", staff.Salary);
                        command.Parameters.AddWithValue("@Stno", staff.Stno);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Staff not found.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    TempData["ErrorMessage"] = "Failed to update staff. Please try again.";
                }
            }

            return View(staff);
        }

        public IActionResult Delete()
        {


            return View();
        }



        [HttpGet]
        public IActionResult CheckStaff(string staffNo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM STAFF WHERE Staff_no = @StaffNo";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@StaffNo", staffNo);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Staff exists, return JSON data
                    Staff staff = new Staff
                    {
                        FullName = reader["fullName"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Password = reader["password"].ToString(),
                        Gender = reader["gender"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        JoiningDate = Convert.ToDateTime(reader["joiningDate"]),
                        Designation = reader["Designation"].ToString(),
                        Salary = Convert.ToInt32(reader["Salary"])
                    };
                    return Json(new { exists = true, staff = staff });
                }
            }

            // Staff does not exist, return JSON data
            return Json(new { exists = false });
        }
    }
}
