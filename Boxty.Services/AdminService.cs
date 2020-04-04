using AutoMapper;
using Boxty.Data;
using Boxty.Models;
using Boxty.Services.Interfaces;
using Boxty.ViewModels;
using Boxty.ViewModels.OutputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services
{
    public class AdminService : IAdminService
    {
		private readonly BoxtyDbContext Context;
		private readonly IMapper mapper;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<BoxtyUser> userManager;

		public AdminService(BoxtyDbContext dbContext, IMapper mapper, RoleManager<IdentityRole> roleManager, UserManager<BoxtyUser> userManager)
        {
			this.Context = dbContext;
			this.mapper = mapper;
			this.roleManager = roleManager;
			this.userManager = userManager;
		}

		public IEnumerable<UserOutputModel> AllUsers(string type)
		{
			var users = userManager.Users.ToListAsync().Result;
			var modelUsers = mapper.Map<IEnumerable<UserOutputModel>>(users);

			foreach (var user in users)
			{
				var role = userManager.GetRolesAsync(user).Result;
				modelUsers.First(x => x.Id == user.Id).Role = role.FirstOrDefault() ?? GlobalConstants.DefaultRole;
			}

			if (!string.IsNullOrEmpty(type) && type != GlobalConstants.ReturnAllUsers)
			{
				modelUsers = modelUsers.Where(x => x.Role.ToLower() == type.ToLower());
			}
			
			return modelUsers;
		}

		public IEnumerable<UserOutputModel> FilterRoles(string filter, IEnumerable<UserOutputModel> result)
		{
			switch (filter)
			{
				case "user":
					result = result.Where(x => x.Role == GlobalConstants.DefaultRole).ToList();
					break;
				case "admin":
					result = result.Where(x => x.Role == GlobalConstants.Admin).ToList();
					break;
				default:
					result = result.OrderBy(s => s.FirstName).ToList();
					break;
			}
			return result;
		}
	}
}
