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
        [HttpGet("Index/{projectId:int?}")]
        public async Task<IActionResult> Index(int? projectId)
        {
            var tasksQuery = _context.ProjectTasks.AsQueryable();

            if (projectId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId.Value);
            }
            var tasks = await tasksQuery.ToListAsync();
            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefaultAsync(t => t.ProjectTaskId == id); // Corrected to filter by ProjectTaskId

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost("Create/{projectId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                await _context.ProjectTasks.AddAsync(task);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            var projects = await _context.Projects.ToListAsync();

            ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Create/{projectId:int}")]
        public async Task<IActionResult> Create(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
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

        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (id != task.ProjectTaskId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            var projects = await _context.Projects.ToListAsync();
            ViewBag.Projects = new SelectList(projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefaultAsync(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            var projects = await _context.Projects.ToListAsync();
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = _context.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }                          
            return View(task);
        }

        [HttpPost("DeleteConfirmed/{ProjectTaskId:int}"), ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProjectTaskId)
        {
            var task = await _context.ProjectTasks.FindAsync(ProjectTaskId);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            return NotFound();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(int? projectID, string searchString)
        {
            var taskQuery = _context.ProjectTasks.AsQueryable();
            bool searchPerformed = !String.IsNullOrEmpty(searchString);

            // if projectID was passed 
            if (projectID.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.ProjectId == projectID.Value);
            }

            if (!searchPerformed)
            {
                taskQuery = taskQuery.Where(t => t.Title.Contains(searchString)
                || t.Description.Contains(searchString));
            }

            var tasks = await taskQuery.ToListAsync();
            ViewBag.projectID = projectID;
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", tasks);
        }
    }
}
