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
        public string SenderId { get; set; }
        public DateTime Date { get; set; }
        public string Delivery { get; set; }
        public string Status { get; set; }
    }
}
