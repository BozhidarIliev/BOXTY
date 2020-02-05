﻿using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Boxty.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
    }
}