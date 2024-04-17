using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace A1_COMP2139.Controllers
{
    public class HotelCommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HotelCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int hotelId)
        {
            var comments = await _context.HotelComments
                .Where(c => c.hotelId == hotelId)
                .OrderByDescending(c => c.DatePosted)
                .ToListAsync();

            return Json(comments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] HotelComment comment)
        {
            if (!ModelState.IsValid)
            {
                comment.DatePosted = DateTime.Now;
                _context.HotelComments.Add(comment);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Comment added successfully." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid comment data. ", error = errors });
        }
    }
}
