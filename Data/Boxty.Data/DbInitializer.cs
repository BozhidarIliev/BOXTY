namespace Boxty.Data
{
    using System.Linq;

    using Boxty.Models;

    public static class DbInitializer
    {
        public static void Initialize(BoxtyDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
                if (context.Products.Any())
                {
                    return;
                }
                else
                {
                    Product[] items = new Product[]
                    {
                    new Product() { Name = "Chicken Boxty", Description = "Filet of Free-range Irish Chicken, Smoked Bacon & Leek Cream Sause, Boxty Pancake, House Salad", ImageUrl = "https://www.tasteofhome.com/wp-content/uploads/2017/10/Creamy-Chicken-Boxty_exps141524_THHC2238742B09_23_4b_RMS-1-696x696.jpg", Price = 20 },
                    new Product() { Name = "Leek & Potato Soup", Description = "Classic Irish Recipie of Potato & Leek Soup, Soda Bread", ImageUrl = "https://www.lanascooking.com/wp-content/uploads/2013/03/Leek-and-Potato-soup-feature.jpg", Price = 15 },
                    };
                    context.Products.AddRange(items);
                }

                context.SaveChanges();
            }
        }
    }
}
