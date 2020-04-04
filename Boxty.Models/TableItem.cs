using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.Models
{
    public class TableItem
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }
        public int TableId { get; set; }
        public string WaiterId { get; set; }
    }
}
