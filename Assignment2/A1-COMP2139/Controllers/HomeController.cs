using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using A1_COMP2139.Models;
using A1_COMP2139.Data;

namespace A1_COMP2139.Controllers;
[ServiceFilter(typeof(LoggingActionFilter))]
public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }


    public JsonResult GetHotelSearchingData(string SearchBy, string SearchValue)
    {
        List<Hotel> hotelList = new List<Hotel>();
        if (SearchBy == "HotelName")
        {
            hotelList = _context.Hotels.Where(x => x.HotelName.Contains(SearchValue) || SearchValue == null).ToList();
        }
        else if (SearchBy == "Location")
        {
            hotelList = _context.Hotels.Where(x => x.Location.Contains(SearchValue) || SearchValue == null).ToList();
        }
        // Add other search criteria as needed
        return Json(hotelList);
    }

    public JsonResult GetCarSearchingData(string SearchBy, string SearchValue)
    {
        List<Car> carList = new List<Car>();
        if (SearchBy == "Id")
        {
            try
            {
                int Id = Convert.ToInt32(SearchValue);
                carList = _context.Cars.Where(x => x.CarId == Id || SearchValue == null).ToList();
            }
            catch (FormatException)
            {
                Console.WriteLine("{0} Is Not a Id", SearchValue);
            }
            return Json(carList);
        }
        else if (SearchBy == "Company")
        {
            carList = _context.Cars.Where(x => x.RentalCompany.Contains(SearchValue) || SearchValue == null).ToList();
            return Json(carList);
        }
        else
        {
            carList = _context.Cars.Where(x => x.Manufacturer.Contains(SearchValue) || SearchValue == null).ToList();
            return Json(carList);
        }
    }

    public JsonResult GetFlightSearchingData(string SearchBy, string SearchValue)
    {
        List<Flight> flightList = new List<Flight>();
        if (SearchBy == "Airline")
        {
            flightList = _context.Flights.Where(x => x.Airline.Contains(SearchValue) || SearchValue == null).ToList();
        }
        else if (SearchBy == "Departure")
        {
            flightList = _context.Flights.Where(x => x.Departure.Contains(SearchValue) || SearchValue == null).ToList();
        }
        // Add other search criteria as needed
        return Json(flightList);
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
