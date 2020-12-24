namespace Boxty.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;

    public class ProductEditInputModel: IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, GlobalConstants.MaxPriceValue)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
