namespace Boxty.Web.ViewModels
{
    using System;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class OrderItemOutputModel : IMapFrom<OrderItem>, IMapTo<OrderItem>
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Amount { get; set; }

        public string Comment { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public bool IsDeleted { get; set; }

        public string Status { get; set; }

        public decimal Subtotal => Product.Price * Amount;
    }
}
