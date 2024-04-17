using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static A1_COMP2139.Models.FlightComment;
using static A1_COMP2139.Models.CarComment;
using static A1_COMP2139.Models.HotelComment;

namespace A1_COMP2139.Controllers
{
    public class CarCommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int carId)
        {
            var comments = await _context.CarComments
                .Where(c => c.carId == carId)
                .OrderByDescending(c => c.DatePosted)
                .ToListAsync();

            return Json(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CarComment comment)
        {
            if (!ModelState.IsValid)
            {
                comment.DatePosted = DateTime.Now;
                _context.CarComments.Add(comment);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Comment added successfully." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid comment data. ", error = errors });
        }
    }
}