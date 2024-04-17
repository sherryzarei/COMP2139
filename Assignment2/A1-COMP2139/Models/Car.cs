using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace A1_COMP2139.Models
{
    public class Car : IValidatableObject
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Rental company name cannot exceed 50 characters.")]
        public string RentalCompany { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Manufacturer name cannot exceed 50 characters.")]
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Model name cannot exceed 50 characters.")]
        public string Model { get; set; }

        [Required]
        [Range(1886, int.MaxValue, ErrorMessage = "Year must be after 1886.")]
        public int Year { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Availability must be non-negative.")]
        public int Availability { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload Image (jpeg, png, jpg)")]
        public IFormFile? ImageFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Year > DateTime.Now.Year + 1)
            {
                yield return new ValidationResult(
                    $"Year cannot be in the future.",
                    new[] { nameof(Year) });
            }
        }
    }
}
