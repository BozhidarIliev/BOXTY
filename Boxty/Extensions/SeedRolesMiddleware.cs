namespace Boxty.Extensions
{
	using System;
	using System.Collections.Generic;
    using System.Linq;
	using System.Threading.Tasks;
    using Boxty.Data;
    using Boxty.Models;
    using Boxty.Services;
    using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;

	public class SeedRolesMiddleware
	{
		private readonly RequestDelegate _next;

		public SeedRolesMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context,
			UserManager<BoxtyUser> userManager
			, RoleManager<IdentityRole> roleManager,
			BoxtyDbContext dbContext)
		{
			if (!roleManager.Roles.Any())
			{
				await SeedRoles(userManager, roleManager);
			}
			if (!dbContext.Tables.Any())
			{
				await SeedTables(dbContext);
			}
			if (!dbContext.TableItems.Any())
			{
				await SeedTableItems(dbContext);
			}

			// Call the next delegate/middleware in the pipeline
			await _next(context);
		}

		private async Task SeedTableItems(BoxtyDbContext dbContext)
		{
			await dbContext.TableItems.AddAsync(new TableItem
			{
				TableId = 1,
				Amount = 1,
				Product = dbContext.Products.First(x => x.Id == 1)
			});
			dbContext.SaveChanges();
		}

		private async Task SeedTables(BoxtyDbContext context)
		{
			List<Table> tables = new List<Table>();
			for (int i = 1; i <= 20; i++)
			{
				tables.Add(new Table
				{
					Taken = "false"
				});
			}
			await context.AddRangeAsync(tables);
			context.SaveChanges();
		}

		private async Task SeedRoles(
			UserManager<BoxtyUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.Admin
			});
			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.Waiter
			});

			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.Manager
			});

			await roleManager.CreateAsync(new IdentityRole
			{
				Name = GlobalConstants.DefaultRole
			});

			var user = new BoxtyUser
			{
				UserName = "admin",
				Email = "admin@admin.com",
				FirstName = "admin",
				LastName = "adminov"
			};

			var normalUser = new BoxtyUser
			{ 
				UserName = "bojo",
				Email = "user@user.com",
				FirstName = "bojo",
				LastName = "iliev"
			};

			string normalUserPass = "123";
			string adminPass = "123";

			await userManager.CreateAsync(user, adminPass);
			await userManager.CreateAsync(normalUser, normalUserPass);

			await userManager.AddToRoleAsync(user, GlobalConstants.Admin);
			await userManager.AddToRoleAsync(normalUser, GlobalConstants.DefaultRole);
		}
	}
}