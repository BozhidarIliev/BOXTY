namespace Boxty.Data.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class Product : BaseDeletableModel<int>, IMapFrom<OrderItem>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
