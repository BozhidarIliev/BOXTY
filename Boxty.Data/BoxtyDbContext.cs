using System;
using System.Collections.Generic;
using System.Text;
using Boxty.Data.Configurations;
using Boxty.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boxty.Data
{
    public class BoxtyDbContext : IdentityDbContext<BoxtyUser>
    {
        public BoxtyDbContext(DbContextOptions<BoxtyDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderItemsConfiguration());
            builder.ApplyConfiguration(new BoxtyUserConfiguration());
        }
    }
}
