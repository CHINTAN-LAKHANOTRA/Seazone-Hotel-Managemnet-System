using System.ComponentModel.DataAnnotations;

namespace Seazone.Models
{
    public class Amenities
    {
        public int Id { get; set; }

        [Display(Name = "Amenity Name")]
        public string AmenityName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Photo URL 1")]
        public string PhotoUrl1 { get; set; }

        [Display(Name = "Photo URL 2")]
        public string PhotoUrl2 { get; set; }

        public int Price { get; set; }

        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
    }
}
