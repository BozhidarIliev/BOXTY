namespace Boxty.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {

        public async Task SeedAsync(BoxtyDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<BoxtyUser>>();

            var user = await userManager.FindByNameAsync("admin");
            if (user != null)
            {
                return;
            }

            user = 
                new BoxtyUser
                {
                    UserName = "admin",
                    Email = "admin@boxty.com",
                    EmailConfirmed = true,
                };
            await userManager.CreateAsync(user, "123456");

            await userManager.AddToRoleAsync(user, GlobalConstants.Admin);
        }
    }
}
