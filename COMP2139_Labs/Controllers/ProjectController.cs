using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Models;

namespace COMP2139_Labs.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index() //Actions in controllers are only methods
        {
            var projects = new List<Project>()
            {
                new Project { ProjectId = 1, Name = "Project 1", Description = "My First Project"}
            };
            return View(projects);  //finds the view from the method Index()
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
