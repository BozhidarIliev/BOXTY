namespace Boxty.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateUserViewModel
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MinLength(20)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
