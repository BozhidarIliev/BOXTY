using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
