using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ProjectComment
    {
        [Key]
        public int ProjectCommentId { get; set; }
        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters")]
        public string Content { get; set; }
        [Display(Name = "Posted Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }
        // foreign key
        public int ProjectId { get; set; }
        // navigation property
        public Project? Project { get; set; }
    }
}
