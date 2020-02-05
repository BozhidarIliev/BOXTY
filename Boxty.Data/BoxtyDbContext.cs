using Boxty.Data.Configurations;
using Boxty.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Boxty.Data
{
    public class BoxtyDbContext : DbContext
    {
        public BoxtyDbContext(DbContextOptions<BoxtyDbContext> options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeesConfiguration());
            builder.ApplyConfiguration(new OrderItemsConfiguration());
        }
    }
}
