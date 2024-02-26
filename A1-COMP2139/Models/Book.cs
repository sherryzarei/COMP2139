using System.ComponentModel.DataAnnotations;

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
    }
}
