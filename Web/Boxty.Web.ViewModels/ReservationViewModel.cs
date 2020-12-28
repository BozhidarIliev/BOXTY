namespace Boxty.Web.ViewModels
{
    using System;

    using Boxty.Data.Models;
    using Boxty.Services.Mapping;

    public class ReservationViewModel : IMapFrom<Reservation>
    {
        public int Id { get; set; }

        public string PersonName { get; set; }

        public string PersonEmail { get; set; }

        public string PersonNumber { get; set; }

        public int NumberOfSeats { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get;  set; }

        public DateTime CreatedOn { get; set; }

        public bool Confirmed { get; set; }
    }
}
