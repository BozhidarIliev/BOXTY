using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.Models
{
    public class Order 
    {
        public Order()
        {

        }
        [Key]
        public int Id { get; set; }
        public DateTime SentTime { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
