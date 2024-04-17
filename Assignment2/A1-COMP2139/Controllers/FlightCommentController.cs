using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A1_COMP2139.Controllers
{
    public class FlightCommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FlightCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int flightId)
        {
            var comments = await _context.FlightComments
                .Where(c => c.flightId == flightId)
                .OrderByDescending(c => c.DatePosted)
                .ToListAsync();

            return Json(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] FlightComment comment)
        {
            if (!ModelState.IsValid)
            {
                comment.DatePosted = DateTime.Now;
                _context.FlightComments.Add(comment);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Comment added successfully." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid comment data. ", error = errors });
        }
    }
}
