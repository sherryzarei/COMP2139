using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using A1_COMP2139.Models;

namespace A1_COMP2139.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }


    public IActionResult GeneralSearch(string searchType, string searchString)
    {
        // Ensure the search type is provided and valid
        if (string.IsNullOrWhiteSpace(searchType) ||
            !new[] { "Flight", "Hotel", "Car" }.Contains(searchType))
        {
            return RedirectToAction("Index", "Home");
        }
        bool searchPerformed = !string.IsNullOrEmpty(searchString);

        // Redirect to the appropriate controller's search action based on the search type
        switch (searchType)
        {
            case "Flight":
                return RedirectToAction("Search", "Flight", new { searchString });
            case "Hotel":
                return RedirectToAction("Search", "Hotel", new { searchString });
            case "Car":
                return RedirectToAction("Search", "Car", new { searchString });
            default:
                return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult NotFound(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("NotFound");
        }
        return View("Error");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
