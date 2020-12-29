namespace Boxty.Web.ViewModels
{
    using Boxty.Common;
    using Boxty.Services;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservationCreateInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        [FutureDateAttribute(ErrorMessage = "Select a date in the future!")]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(1, GlobalConstants.RestaurantDailyWorkingHours, ErrorMessage = "The Duration doesn't meet the Restaurant's working hours!")]
        public int Duration { get; set; }

    }
}
