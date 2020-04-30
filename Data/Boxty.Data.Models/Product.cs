namespace Boxty.Data.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class Product : BaseDeletableModel<int>, ICreatorInfo, IMapFrom<OrderItem>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
