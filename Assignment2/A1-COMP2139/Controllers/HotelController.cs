using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static A1_COMP2139.Models.Hotel;

namespace A1_COMP2139.Controllers
{
    [Route("[controller]/[action]")]
    [ServiceFilter(typeof(LoggingActionFilter))]
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HotelController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var hotels = _context.Hotels.Where(f => f.Availability > 0).ToList();
            return View(hotels);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create([Bind("HotelId,HotelName,Location,Availability,Price,ImageFile")] Hotel hotel, List<Hotel.Amenity> Amenities)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(hotel.ImageFile.FileName);
                string extension = Path.GetExtension(hotel.ImageFile.FileName).ToLower();
                hotel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/Hotels/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await hotel.ImageFile.CopyToAsync(fileStream);
                }

                hotel.Amenities = Amenities;
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Adjust as necessary
            }
            return View(hotel);
        }

        [HttpGet("details/{id:int}")]
        public IActionResult Details(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(p => p.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(p => p.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int hotelId)
        {
            var project = _context.Hotels.Find(hotelId);
            if (project != null)
            {
                var bookings = _context.Book.Where(b => b.HotelId == hotelId);
                _context.Book.RemoveRange(bookings);
                _context.Hotels.Remove(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string searchString)
        {
            var hotelQuery = _context.Hotels.Where(h => h.Availability > 0);

            // Check if a search string is provided and filter hotels by name or location
            if (!string.IsNullOrEmpty(searchString))
            {
                hotelQuery = hotelQuery.Where(h => h.HotelName.Contains(searchString) || h.Location.Contains(searchString));
            }
            // Retrieve the filtered hotels and pass them to the view
            var hotels = await hotelQuery.ToListAsync();
            ViewData["SearchString"] = searchString;
            return View("Index", hotels);
        }
    }
}
