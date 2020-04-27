using Boxty.Data.Models;
using Boxty.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.Web.ViewModels
{
    public class ProductOutputModel : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}
