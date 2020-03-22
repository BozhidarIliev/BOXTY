using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels.OutputModels
{
    public class UserOutputModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
