using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [Display(Name="Project Name")]
        [StringLength(100, ErrorMessage ="Project name cannot exceed 100 characters")]
        public required string Name { get; set; }

        [Display(Name = "description")]
        [DataType(DataType.MultilineText)] //using notation to enforce date datatype as an entry
        [StringLength(500, ErrorMessage ="Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Display(Name="Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Status")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
        public string? Status { get; set; }
    }
}
