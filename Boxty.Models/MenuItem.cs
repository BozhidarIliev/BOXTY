using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
