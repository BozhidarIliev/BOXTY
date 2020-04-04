using Boxty.Models;
using Boxty.ViewModels.OutputModels;
using System.Collections.Generic;

namespace Boxty.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderOutputModel> AllOrders();
        void CreateOrder(Order order);
        public void MarkAsDone(int productId, int orderId);
        public void RemoveFromOrders(int productId, int orderId);
    }
}