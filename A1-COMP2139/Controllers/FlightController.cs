using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A1_COMP2139.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FlightController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var flights = _context.Flights.Where(f => f.Availability > 0).ToList();
            return View(flights);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,FlightNumber,Airline,Departure,Arrival,DepartureDate,ArrivalDate,Price,Availability,ImageFile")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(flight.ImageFile.FileName);
                string extension = Path.GetExtension(flight.ImageFile.FileName).ToLower();

                flight.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/Flights/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await flight.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Adjust as necessary
            }
            return View(flight);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var flight = _context.Flights.Find(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FlightId, Airline, Departure, Arrival, Price")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExist(flight.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        private bool FlightExist(int flightId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int flightId)
        {
            var project = _context.Flights.Find(flightId);
            if (project != null)
            {
                var bookings = _context.Book.Where(b => b.FlightId == flightId);
                _context.Book.RemoveRange(bookings);
                _context.Flights.Remove(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var flight = _context.Flights.FirstOrDefault(p => p.FlightId == id);
            return View(flight);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(string searchString)
        {
            var flightQuery = _context.Flights.Where(f => f.Availability > 0);
            bool searchPerformed = !string.IsNullOrEmpty(searchString);
            // Check if a search string is provided and filter flights by airline or departure
            if (searchPerformed)
            {
                flightQuery = flightQuery.Where(f => f.Airline.Contains(searchString) || f.Departure.Contains(searchString));
            }


            // Retrieve the filtered flights and pass them to the view
            var flights = await flightQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            //ViewData["Location"] = location;
            //ViewData["Date"] = date;
            return View("Index", flights);
        }
    }
}
