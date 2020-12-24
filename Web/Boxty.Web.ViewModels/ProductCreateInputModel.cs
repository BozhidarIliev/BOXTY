namespace Boxty.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ProductCreateInputModel : IMapTo<Product>
    {
        public int Id { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.01, GlobalConstants.MaxPriceValue)]
        [Required(ErrorMessage="Price cannot be zero!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description cannot be empty!")]
        [MinLength(5)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category field cannot be empty")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        [Required(ErrorMessage = "You cannot create Product without image!")]
        [DisplayName("Upload File")]
        public IFormFile Image { get; set; }

    }

}
