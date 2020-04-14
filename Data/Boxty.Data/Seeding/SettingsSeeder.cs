namespace Boxty.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(BoxtyDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Settings.Any())
            {
                return;
            }

            await dbContext.Settings.AddAsync(new Setting { Name = "Setting1", Value = "value1" });
        }
    }
}
