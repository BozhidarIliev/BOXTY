namespace Boxty.Models
{
    using Boxty.Data.Common.Models;
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderItem : BaseDeletableModel<int>, ICreatorInfo
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public decimal Subtotal { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
