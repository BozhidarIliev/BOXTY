namespace Boxty.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Data.Models;

    public class Product : BaseModel<int>
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
