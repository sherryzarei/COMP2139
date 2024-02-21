using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask>ProjectTasks { get; set; }
    }
}
