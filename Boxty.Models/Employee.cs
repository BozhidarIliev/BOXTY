using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Models
{
    public enum Roles
    {
        Driver,
        Kitchen,
        Manager,
        Server
    }
    public class Employee
    {
        [Key]
        public int Id { get; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Roles Role { get; set; }
    }
}
