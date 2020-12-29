namespace Boxty.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(BoxtyDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, GlobalConstants.DefaultRole);
            await SeedRoleAsync(roleManager, GlobalConstants.Admin);
            await SeedRoleAsync(roleManager, GlobalConstants.KitchenStaff);
            await SeedRoleAsync(roleManager, GlobalConstants.Driver);
            await SeedRoleAsync(roleManager, GlobalConstants.Waiter);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
            }
        }
    }
}
