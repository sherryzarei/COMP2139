﻿using COMP2139_Labs.Areas.ProjectManagement.Models;
using COMP2139_Labs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers
{

    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ProjectCommentController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProjectCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(int projectID)
        {

            var comments = await _context.ProjectComments
                                .Where(c => c.ProjectId == projectID)
                                .OrderByDescending(c => c.DatePosted)
                                .ToListAsync();

            return Json(comments);
        }



        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] ProjectComment comment)
        {

            if (ModelState.IsValid)
            {
                comment.DatePosted = DateTime.Now;
                _context.ProjectComments.Add(comment);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Comment added successfully" });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, message = "Invalid comment data", error = errors });
        }



    }
}
