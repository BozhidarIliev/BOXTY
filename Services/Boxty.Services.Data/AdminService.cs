namespace Boxty.Services
{
    using System.Collections.Generic;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.ViewModels.OutputModels;
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

        public IEnumerable<UserOutputModel> AllUsers(string type)
        {
            //var users = userManager.Users.To<UserOutputModel>().ToListAsync().Result;

            //foreach (var user in users)
            //{
            //    IList<string> role = userManager.GetRolesAsync(user).Result;
            //    users.First(x => x.Id == user.Id).Role = role.FirstOrDefault() ?? GlobalConstants.DefaultRole;
            //}

            //if (string.IsNullOrEmpty(type) || type == GlobalConstants.ReturnAllUsers)
            //{
            //    return users;
            //}

            //users = users.Where(x => x.Role.ToLower() == type.ToLower());

            //return users;
            return null;
        }

        public IEnumerable<UserOutputModel> FilterRoles(string filter, IEnumerable<UserOutputModel> result)
        {
            throw new System.NotImplementedException();
        }
    }
}
