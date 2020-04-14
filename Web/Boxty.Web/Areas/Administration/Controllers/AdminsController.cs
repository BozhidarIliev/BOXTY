namespace Boxty
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services;
    using Boxty.Services.Interfaces;
    using Boxty.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.Admin)]
    [Area("Administration")]
    public class AdminsController : Controller
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<BoxtyUser> userManager;

        public AdminsController(IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<BoxtyUser> userManager, IAdminService adminService)
        {
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.adminService = adminService;
        }

        [HttpGet]
        public IActionResult UsersList()
        {
            var users = adminService.AllUsers(null);
            return View(users);
        }

        [HttpPost]
        public IActionResult UsersList(string type)
        {
            var users = adminService.AllUsers(type);
            return View(users);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName,
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RolesList()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }
    }
}
