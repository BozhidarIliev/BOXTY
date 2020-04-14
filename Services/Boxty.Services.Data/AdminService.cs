namespace Boxty.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services.Interfaces;
    using Boxty.ViewModels.OutputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AdminService : IAdminService
    {
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<BoxtyUser> userManager;

        public AdminService(IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<BoxtyUser> userManager)
        {
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IEnumerable<UserOutputModel> AllUsers(string type)
        {
            var users = userManager.Users.ToListAsync().Result;
            IEnumerable<UserOutputModel> modelUsers = mapper.Map<IEnumerable<UserOutputModel>>(users);

            foreach (BoxtyUser user in users)
            {
                IList<string> role = userManager.GetRolesAsync(user).Result;
                modelUsers.First(x => x.Id == user.Id).Role = role.FirstOrDefault() ?? GlobalConstants.DefaultRole;
            }

            if (string.IsNullOrEmpty(type) || type == GlobalConstants.ReturnAllUsers)
            {
                return modelUsers;
            }

            modelUsers = modelUsers.Where(x => x.Role.ToLower() == type.ToLower());

            return modelUsers;
        }

        public IEnumerable<UserOutputModel> FilterRoles(string filter, IEnumerable<UserOutputModel> result)
        {
            throw new System.NotImplementedException();
        }
    }
}
