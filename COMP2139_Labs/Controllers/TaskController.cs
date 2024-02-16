using COMP2139_Labs.Data;
using COMP2139_Labs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace COMP2139_Labs.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(int projectId)
        {
            var tasks = _context.ProjectTasks.Where(t => t.ProjectId == projectId)
                .ToList();

            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefault(task => task.ProjectId == id);

            if(task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title", "Description", "ProjectId")] ProjectTask task)
        {
            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId});
            }
            ViewBag.Projects = new SelectList(_context.ProjectTasks, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet]
        public IActionResult Create(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if(project == null)
            {
                return NotFound();
            }
            var task = new ProjectTask
            {
                ProjectId = projectId,
            };
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind ("projecttaskId", "Title", "Description", "ProjectId")] ProjectTask task)
        {
            if(id != task.ProjectTaskId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }

            ViewBag.Projects = new SelectList(_context.ProjectTasks, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _context.ProjectTasks
                        .Include(t => t.Project)
                        .FirstOrDefault(t => t.ProjectTaskId == id);
            if(task == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }

        [HttpGet]
        public IActionResult Delete(int id)
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
            return View();
        }

        [HttpPost, ActionName("DeeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectTaskId)
        {
            var task = _context.ProjectTasks.Find(projectTaskId);
            if(task != null)
            {
                _context.ProjectTasks.Remove(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { ProjectId = task.ProjectId });
            }
            return NotFound();
        }
    }
}
