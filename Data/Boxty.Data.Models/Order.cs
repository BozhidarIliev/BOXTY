namespace Boxty.Data.Models
{
    using System;

    using Boxty.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public string Status { get; set; }

        public DateTime SentOn { get; set; }

        public string Destination { get; set; }

        public BoxtyUser Sender { get; set; }
    }
}
