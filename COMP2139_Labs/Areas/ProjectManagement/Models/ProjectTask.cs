using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        [Key]
        public int ProjectTaskId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        // Foreign key for project
        public int ProjectId { get; set; }

        // navigation property
        // This property allows for easy access to the related projecr entity from the task entity
        public Project? Project { get; set; }
    }
}
