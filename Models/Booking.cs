using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Seazone.Models
{
    public class Booking
    {
        // Properties for booking details
        public int Id { get; set; } // Auto-incremented by the database
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Aadhar { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string RoomType { get; set; }

        // Additional services as a list of strings
        public List<string> AddedServices { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        // Amount to be calculated based on room type and services
        public decimal Amount { get; set; }

        // Constructor to initialize AddedServices list
        public Booking()
        {
            AddedServices = new List<string>();
        }

        // Method to serialize AddedServices list to JSON string
        public string SerializeAddedServices()
        {
            return JsonSerializer.Serialize(AddedServices);
        }

        // Method to deserialize JSON string to AddedServices list
        public void DeserializeAddedServices(string addedServicesJson)
        {
            AddedServices = JsonSerializer.Deserialize<List<string>>(addedServicesJson);
        }

        public decimal Bill { get; set; }
    }
}
