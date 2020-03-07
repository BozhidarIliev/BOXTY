using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        
        public decimal ShoppingCartTotal { get; set; }
    }
}
