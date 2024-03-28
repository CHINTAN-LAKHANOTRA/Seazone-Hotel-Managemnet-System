using System.ComponentModel.DataAnnotations;

namespace Seazone.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserType { get; set; }

        public string tshid { get; set; }
    }
}
