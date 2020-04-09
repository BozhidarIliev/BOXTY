using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Models
{
    public class BaseOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime SentOn { get; set; }
    }
}
