using Boxty.Data.Models;
using Boxty.Services.Data.Interfaces;
using Boxty.Services.Mapping;
using Boxty.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services.Data
{
    public class DriverService : IDriverService
    {
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;

        public DriverService(IOrderService orderService, IOrderItemService orderItemService)
        {
            this.orderService = orderService;
            this.orderItemService = orderItemService;
        }

        public IOrderItemService OrderItemService { get; }

        public IEnumerable<OrderDriverViewModel> GetCurrentOrderItems()
        {
            var orders = orderService.GetAllOrders().Where(x => x.Delivery == true).AsQueryable().To<OrderDriverViewModel>();
            foreach (var order in orders)
            {
                order.OrderItems = orderItemService.GetCurrentOrderItemsByOrderId<OrderItemOutputModel>(order.Id);
            }

            return orders;
        }



    }
}
