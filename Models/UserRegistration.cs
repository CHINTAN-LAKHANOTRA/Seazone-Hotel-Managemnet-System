using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Seazone
{
    public class UserRegistration
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Seazone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please select your gender")]
        public string Gender { get; set; }

        public string UserType { get; set; } = "guest";

        public string TshId { get; set; } = $"TSH{DateTime.Now.ToString("yyyyMMddHHmmss")}{1000}";

        public string AccStatus { get; set; } = "active";

        public bool Insert(UserRegistration user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = @"
                INSERT INTO [dbo].[User] (FullName, Email, Phone, Password, Gender, UserType, AccStatus)
                VALUES (@FullName, @Email, @Phone, @Password, @Gender, @UserType, @AccStatus);
            ";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", user.FullName);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Phone", user.Phone);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Gender", user.Gender);
                        command.Parameters.AddWithValue("@UserType", user.UserType);
                        command.Parameters.AddWithValue("@AccStatus", user.AccStatus);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during insertion: {ex.Message}");
                throw;
            }
        }
    }
}
