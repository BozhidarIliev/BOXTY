namespace Boxty.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class BoxtyUser : IdentityUser
    {
        public BoxtyUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
