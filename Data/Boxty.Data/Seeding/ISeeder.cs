namespace Boxty.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(BoxtyDbContext dbContext, IServiceProvider serviceProvider);
    }
}
