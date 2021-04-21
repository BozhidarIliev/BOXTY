namespace Boxty.Web.ViewModels
{
    using AutoMapper;
    using Boxty.Models;
    using Boxty.Services.Mapping;
    using System;

    public class TableItemViewModel : IMapTo<OrderItem>,IMapFrom<OrderItem>, IMapTo<TableItemViewModel>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string Status { get; set; } = "marked";

        public DateTime ModifiedOn { get; set; } = DateTime.Now;
    }
}
