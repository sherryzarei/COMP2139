using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace A1_COMP2139.Models
{
    public class Flight : IValidatableObject
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{2}\d{3,5}$", ErrorMessage = "Flight number must follow the pattern: Two letters followed by 3 to 5 numbers.")]
        public string FlightNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Airline name cannot be longer than 50 characters.")]
        public string Airline { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Departure location cannot be longer than 50 characters.")]
        public string Departure { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Arrival location cannot be longer than 50 characters.")]
        public string Arrival { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DepartureDate { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.00.")]
        public double Price { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Availability must be between 0 and 1000.")]
        public int Availability { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload Image (jpeg, png, jpg)")]
        public IFormFile? ImageFile { get; set; }
    

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArrivalDate <= DepartureDate)
            {
                yield return new ValidationResult(
                    "The arrival date must be after the departure date.",
                    new[] { nameof(ArrivalDate) });
            }

            // Example of a specific date-time format check (pseudo-code)
            // This is to illustrate the concept; actual implementation may vary based on specific requirements
            if (!IsDateTimeIncludingHourAndMinute(DepartureDate) || !IsDateTimeIncludingHourAndMinute(ArrivalDate))
            {
                yield return new ValidationResult(
                    "Dates must include year, month, day, hour, and minute.",
                    new[] { nameof(DepartureDate), nameof(ArrivalDate) });
            }
        }

        private bool IsDateTimeIncludingHourAndMinute(DateTime dateTime)
        {
            return true;
        }
    }
}
