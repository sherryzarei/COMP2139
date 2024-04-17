using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A1_COMP2139.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int? FlightId { get; set; }
        public int? HotelId { get; set; }
        public int? CarId { get; set; }
        public Flight? Flight { get; set; }
        public Hotel? Hotel { get; set; }
        public Car? Car { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestNumber { get; set; }
        [Required]
        public string ReceiptNumber { get; set; }

        // Add UserId to link the booking to a specific user
        public string? UserId { get; set; } // Identity user ID
        public string? Username { get; set; } // Username
        public string? Email { get; set; } // User Email

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; } // Navigation property
    }
}
