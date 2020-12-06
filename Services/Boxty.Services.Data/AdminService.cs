namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public class AdminService : IAdminService
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<BoxtyUser> userManager;

        public AdminService(RoleManager<ApplicationRole> roleManager, UserManager<BoxtyUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            ApplicationRole identityRole = new ApplicationRole
            {
                Name = model.RoleName,
            };

            return await roleManager.CreateAsync(identityRole);
        }

        public async Task<EditRoleViewModel> GetUserRoles(ApplicationRole role)
        {
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            // Retrieve all the Users
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return model;
        }

        public async Task<List<UserRoleViewModel>> EditUsersInRole(ApplicationRole role)
        {
            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return model;
        }
    }
}
