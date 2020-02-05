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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeesConfiguration());
        }
    }
}
