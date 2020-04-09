using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.Models
{
    public class Order 
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime SentOn{ get; set; }
        public string Destination { get; set; }
        public string Sender { get; set; }

    }
}
