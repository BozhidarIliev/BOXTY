using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
