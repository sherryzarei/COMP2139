using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace A1_COMP2139.Models
{
    public class Hotel
    {
        public enum Amenity
        {
            FreeWiFi,
            Parking,
            SwimmingPool,
            Gym,
            Spa,
            Restaurant,
            RoomService,
            AirportShuttle,
            FamilyRooms,
            PetFriendly
        }
        [Key]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Hotel name must be between 3 and 100 characters.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Location must be between 3 and 200 characters.")]
        public string Location { get; set; }

        public List<Amenity>? Amenities { get; set; } = [];


        [Required]
        [Range(0, 500, ErrorMessage = "Availability must be between 0 and 500.")]
        public int Availability { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.00.")]
        public float Price { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload Image (jpeg, png, jpg)")]
        public IFormFile? ImageFile { get; set; }
    }
}
