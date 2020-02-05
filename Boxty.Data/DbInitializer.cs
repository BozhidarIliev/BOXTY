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
            context.Database.EnsureCreated();

            if (context.Employees.Any())
            {
                return;
            }

            Employee[] employees = new Employee[]
            {
                new Employee{FirstName = "Jessie", LastName = "Prescot", Role = Roles.Manager, Username= "jess"},
                new Employee{FirstName = "Jim", LastName = "Scott", Role = Roles.Kitchen, Username= "jim"},
                new Employee{FirstName = "Bob", LastName = "Anderson", Role = Roles.Server, Username= "bob"}
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
