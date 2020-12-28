namespace Boxty.Web.ViewModels
{
    using AutoMapper;
    using Boxty.Data.Models;
    using Boxty.Services.Mapping;
    using System.Collections.Generic;

    public class OrderOutputModel : IMapFrom<Order>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public IEnumerable<OrderItemOutputModel> Items { get; set; }

        public string Status { get; set; }

        public decimal Total { get; set; }

        public string Destination { get; set; }

        public bool Delivery { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public string DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Order, OrderOutputModel>()
                .ForMember(x => x.Items, opt => opt.Ignore());
        }
    }
}
