using COMP2139_Labs.Areas.ProjectManagement.Models;
using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectMnagement")]
    [Route("[area]/[controller]/[action]")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Index/{projectId:int}")]
        public IActionResult Index(int projectId)
        {
            var tasks = _context.ProjectTasks.Where(t => t.ProjectId == projectId)
                .ToList();

            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefault(t => t.ProjectTaskId == id); // Corrected to filter by ProjectTaskId

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost("Create/{projectId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { task.ProjectId });
            }
            ViewBag.Projects = new SelectList(_context.ProjectTasks, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Create/{projectId:int}")]
        public IActionResult Create(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null)
            {
                return NotFound();
            }
            var task = new ProjectTask
            {
                ProjectId = projectId,
            };
            return View(task);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (id != task.ProjectTaskId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { task.ProjectId });
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefault(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var task = _context.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpPost("DeleteConfirmed/{ProjectTaskId:int}"), ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int ProjectTaskId)
        {
            var task = _context.ProjectTasks.Find(ProjectTaskId);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { task.ProjectId });
            }
            return NotFound();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(int? projectID, string searchString)
        {
            var taskQuery = _context.ProjectTasks.AsQueryable();

            // if projectID was passed 
            if (projectID.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.ProjectId == projectID.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                taskQuery = taskQuery.Where(t => t.Title.Contains(searchString)
                || t.Description.Contains(searchString));
            }

            var tasks = await taskQuery.ToListAsync();
            ViewBag.projectID = projectID;
            return View("Index", tasks);
        }
    }
}
