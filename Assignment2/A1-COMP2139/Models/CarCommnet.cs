using System.ComponentModel.DataAnnotations;

namespace A1_COMP2139.Models
{
    public class CarComment
    {
        [Key]
        public int carCommentId { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Project Comments cannot exceed 500 characters.")]
        public string? Content { get; set; }

        [Display(Name = "Posted Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }

        // Foreign key
        public int carId { get; set; }

        // Navigation Property
        public Car car { get; set; }
    }
}
