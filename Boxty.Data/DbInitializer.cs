using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boxty.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BoxtyDbContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
                if (context.OrderItems.Any())
                {
                    return;
                }
                else
                {
                    OrderItem[] orderItems = new OrderItem[]
                    {
                    new OrderItem() { Title = "Chicken Boxty", Summary = "Filet of Free-range Irish Chicken, Smoked Bacon & Leek Cream Sause, Boxty Pancake, House Salad", Type = "Main Course", ImageUrl = "https://www.tasteofhome.com/wp-content/uploads/2017/10/Creamy-Chicken-Boxty_exps141524_THHC2238742B09_23_4b_RMS-1-696x696.jpg", Price = 20} ,
                    new OrderItem() { Title = "Leek & Potato Soup", Summary = "Classic Irish Recipie of Potato & Leek Soup, Soda Bread", Type = "Starter", ImageUrl = "https://www.lanascooking.com/wp-content/uploads/2013/03/Leek-and-Potato-soup-feature.jpg", Price = 15}
                    };
                    context.OrderItems.AddRange(orderItems);
                }

                context.SaveChanges();
            }
        }
    }
}
