using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A1_COMP2139.Controllers
{
    [ServiceFilter(typeof(LoggingActionFilter))]
    [Route("[controller]/[action]")] // Sets the base route for this controller to /cars
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CarController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var cars = _context.Cars.Where(f => f.Availability > 0).ToList();
            return View(cars);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,RentalCompany,Manufacturer,Model,Year,Price,Availability,ImageFile")] Car car)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(car.ImageFile.FileName);
                string extension = Path.GetExtension(car.ImageFile.FileName).ToLower();
                car.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/Cars/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await car.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Adjust as necessary
            }
            return View(car);
        }

        [HttpGet("details/{id:int}")]
        public IActionResult Details(int id) 
        {
            var car = _context.Cars.FirstOrDefault(p => p.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var carQuery = _context.Cars.Where(f => f.Availability > 0);
            bool searchPerformed = !string.IsNullOrEmpty(searchString);

            // Check if a search string is provided and filter flights by airline or departure
            if (!string.IsNullOrEmpty(searchString))
            {
                carQuery = carQuery.Where(f => f.Manufacturer.Contains(searchString) || f.Model.Contains(searchString));
            }

            // Retrieve the filtered flights and pass them to the view
            var flights = await carQuery.ToListAsync();
            ViewData["SearchString"] = searchString;
            return View("Index", flights);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,RentalCompany,Manufacturer,Model,Year,Price,Availability,ImageName,ImageFile")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (car.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(car.ImageFile.FileName);
                        string extension = Path.GetExtension(car.ImageFile.FileName).ToLower();
                        car.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Images/Cars/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await car.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("delete/{id:int}"), ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

