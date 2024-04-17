using A1_COMP2139.Models;
using A1_COMP2139.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace A1_COMP2139.Controllers
{
    
        [Authorize(Roles = "SuperAdmin")]
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
            public async Task<IActionResult> Manager(string userId)
            {
                ViewBag.userId = userId;
                var user = await _userManager.FindByIdAsync(userId);
                var model = new List<ManageUserRolesViewModel>(); // Declare model here

                if (user == null)
                {
                    ViewBag.userId = userId;
                    var user1 = await _userManager.FindByIdAsync(userId);
                    if (user1 == null)
                    {
                        ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                        return View("Not Found");
                    }

                    ViewBag.UserName = user1.UserName;
                    foreach (var role in _roleManager.Roles)
                    {
                        var userRolesViewModel = new ManageUserRolesViewModel
                        {
                            RoleID = role.Id,
                            RoleName = role.Name
                        };

                        if (await _userManager.IsInRoleAsync(user1, role.Name))
                        {
                            userRolesViewModel.Selected = true;
                        }
                        else
                        {
                            userRolesViewModel.Selected = false;
                        }
                        model.Add(userRolesViewModel);
                    }
                }
                return View(model);
            }

        [HttpGet]
        public async Task<IActionResult> Manage(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleID = role.Id,
                    RoleName = role.Name,
                    Selected = await _userManager.IsInRoleAsync(user, role.Name)
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

