namespace Boxty.Extensions
{
	using System.Linq;
	using System.Threading.Tasks;
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
			, RoleManager<IdentityRole> roleManager)
		{
			if (!roleManager.Roles.Any())
			{
				await SeedRoles(userManager, roleManager);
			}

			// Call the next delegate/middleware in the pipeline
			await _next(context);
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
				UserName = "user",
				Email = "user@user.com"
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