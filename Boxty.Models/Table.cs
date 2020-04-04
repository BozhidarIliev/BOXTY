using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public string Taken { get; set; }
    }
}
