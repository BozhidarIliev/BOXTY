using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Address { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The phone number cannot exceed 10 numbers. ")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
