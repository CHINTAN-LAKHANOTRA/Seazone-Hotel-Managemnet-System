using System.ComponentModel.DataAnnotations;

namespace Seazone.Models
{
    public class Staff
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Joining Date is required")]
        [DataType(DataType.Date)]
        public DateTime JoiningDate { get; set; }

        public string UserType { get; set; }

        public string TshId { get; set; }

        public string Designation { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public int Salary { get; set; }

        public string Stno { get; set; }
    }
}
