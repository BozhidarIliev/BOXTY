namespace Boxty.Data.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Models;
    using System;

    public class Reservation : BaseDeletableModel<int>
    {
        public string PersonName { get; set; }

        public string PersonEmail { get; set; }

        public string PersonNumber { get; set; }

        public int NumberOfSeats { get; set; }

        public virtual Table Table { get; set; }
        public int TableId { get; set; }
        public int NumberOfHours { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool Confirmed { get; set; }
    }
}
