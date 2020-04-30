using Boxty.Data.Models;
using Boxty.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Web.ViewModels
{
    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
