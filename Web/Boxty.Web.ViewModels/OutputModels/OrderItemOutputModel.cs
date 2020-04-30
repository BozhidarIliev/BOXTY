namespace Boxty.Web.ViewModels
{
    using System;
    using AutoMapper;
    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Services.Mapping;

    public class OrderItemOutputModel : IMapFrom<OrderItem>, IMapTo<OrderItem>, IHaveCustomMappings
    {
        public int OrderId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int Amount { get; set; }

        public string Comment { get; set; }

        public Product Product { get; set; }

        public bool IsDeleted { get; set; }

        public string Status { get; set; }

        public decimal Subtotal => Product.Price * Amount;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderItemOutputModel, OrderItem>().ForMember(x => x.ProductId, opt => opt.Ignore());
        }
    }
}
