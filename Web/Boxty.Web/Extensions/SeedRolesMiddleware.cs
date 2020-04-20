namespace Boxty.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Boxty.Common;
    using Boxty.Data;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            UserManager<BoxtyUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            BoxtyDbContext dbContext)
        {
            if (!roleManager.Roles.Any())
            {
                await SeedRoles(userManager, roleManager);
            }

            if (!userManager.Users.Any())
            {
                await SeedUsers(userManager, roleManager);
            }

            if (!dbContext.Tables.Any())
            {
                await SeedTables(dbContext);
            }

            if (!dbContext.Products.Any())
            {
                await SeedProducts(dbContext);
            }

            if (!dbContext.Categories.Any())
            {
                await SeedCategories(dbContext);
            }

            // Call the next delegate/middleware in the pipeline
            dbContext.SaveChanges();
            await _next(context);
        }

        private async Task SeedCategories(BoxtyDbContext dbContext)
        {
            Category[] items = new Category[]
            {
                new Category() { Name = "Main Course" },
                new Category() { Name = "Starter" },
            };
            await dbContext.Categories.AddRangeAsync(items);
        }

        private async Task SeedProducts(BoxtyDbContext dbContext)
        {
            Product[] items = new Product[]
            {
                new Product() { Name = "Chicken Boxty", Description = "Filet of Free-range Irish Chicken, Smoked Bacon & Leek Cream Sause, Boxty Pancake, House Salad", ImageUrl = "https://www.tasteofhome.com/wp-content/uploads/2017/10/Creamy-Chicken-Boxty_exps141524_THHC2238742B09_23_4b_RMS-1-696x696.jpg", Price = 20 },
                new Product() { Name = "Leek & Potato Soup", Description = "Classic Irish Recipie of Potato & Leek Soup, Soda Bread", ImageUrl = "https://www.lanascooking.com/wp-content/uploads/2013/03/Leek-and-Potato-soup-feature.jpg", Price = 15 },
            };
            await dbContext.Products.AddRangeAsync(items);
        }

        private async Task SeedTables(BoxtyDbContext context)
        {
            List<Table> tables = new List<Table>();
            for (int i = 1; i <= 20; i++)
            {
                tables.Add(new Table
                {
                    Status = GlobalConstants.TableIsAvailable,
                });
            }

            await context.Tables.AddRangeAsync(tables);
            context.SaveChanges();
        }

        private async Task SeedUsers(
            UserManager<BoxtyUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            var user = new BoxtyUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                FirstName = "admin",
                LastName = "adminov",
            };

            var normalUser = new BoxtyUser
            {
                UserName = "bojo",
                Email = "user@user.com",
                FirstName = "bojo",
                LastName = "iliev",
            };

            string normalUserPass = "123456";
            string adminPass = "123456";

            await userManager.CreateAsync(user, adminPass);
            await userManager.CreateAsync(normalUser, normalUserPass);

            await userManager.AddToRoleAsync(user, GlobalConstants.Admin);
            await userManager.AddToRoleAsync(normalUser, GlobalConstants.DefaultRole);
        }

        private async Task SeedRoles(
            UserManager<BoxtyUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = GlobalConstants.Admin,
            });
            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = GlobalConstants.Waiter,
            });

            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = GlobalConstants.Manager,
            });

            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = GlobalConstants.KitchenStaff,
            });

            await roleManager.CreateAsync(new ApplicationRole
            {
                Name = GlobalConstants.DefaultRole,
            });
        }
    }
}
