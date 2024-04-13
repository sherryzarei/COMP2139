using COMP2139_Labs.Models;
using COMP2139_Labs.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace COMP2139_Labs.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class UserRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<string>> GetUserRoleAsync(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (ApplicationUser u in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserID = u.Id;
                thisViewModel.FirstName = u.FirstName;
                thisViewModel.LastName = u.LastName;
                thisViewModel.Email = u.Email;
                thisViewModel.Roles = await GetUserRoleAsync(u);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID = {userId} can't be found!";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var roles = await _roleManager.Roles.ToListAsync(); // Materialize the roles list
            var model = new List<ManageUserRolesViewModel>();

            foreach (var role in roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleID = role.Id,
                    RoleName = role.Name,
                    Selected = await _userManager.IsInRoleAsync(user, role.Name) // Check if the user is in this role
                };
                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user from roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add user to roles");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}

