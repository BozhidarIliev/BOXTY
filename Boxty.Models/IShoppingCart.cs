using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Models
{
    public interface IShoppingCart
    { 
        string Id { get; set; }
        ICollection<ShoppingCartItem> Items { get; set; }
    }
}
