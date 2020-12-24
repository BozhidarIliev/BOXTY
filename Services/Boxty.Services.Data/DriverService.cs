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

        public List<OrderDriverViewModel> GetCurrentOrderItems()
        {
            var orders = orderService.GetAllOrders().Where(x => x.Delivery == true).AsQueryable().To<OrderDriverViewModel>().ToList();
            foreach (var order in orders)
            {
                order.OrderItems = orderItemService.GetCurrentOrderItemsByOrderId<OrderItemOutputModel>(order.Id).ToList();
                foreach (var item in order.OrderItems)
                {
                    var currentItemIndex = order.OrderItems.FindIndex(x => x.ProductId == item.ProductId && x.Comment == item.Comment);
                    if (currentItemIndex != -1)
                    {
                        order.OrderItems[currentItemIndex].Amount++;
                    }
                    else
                    {
                        order.OrderItems.Add(item);
                    }
                }
            }

            return orders;
        }



    }
}
