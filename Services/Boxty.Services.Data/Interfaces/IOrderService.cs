namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;

    using Boxty.Data.Models;
    using Boxty.Models;
    using Boxty.Web.ViewModels;

    public interface IOrderService
    {
        IEnumerable<T> GetAll<T>(int count);

        IEnumerable<OrderOutputModel> CurrentOrders();

        void CreateOrder(Order order, BaseOrder[] items);

        public void MarkAsDone(int productId, int orderId);

        public void RemoveFromOrders(int productId, int orderId);
    }
}
