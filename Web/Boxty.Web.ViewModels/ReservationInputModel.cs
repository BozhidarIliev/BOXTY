namespace Boxty.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReservationInputModel
    {
        public int Id { get; set; }
        [Required]
        public string PersonName { get; set; }

        [EmailAddress]
        [Required]
        public string PersonEmail { get; set; }

        [Phone]
        [Required]
        public string PersonNumber { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public DateTime StartTime{ get; set; }

        [Required]
        public int NumberOfHours { get; set; }

    }
}
