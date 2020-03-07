using Boxty.Models;
using Boxty.ViewModels.OutputModels;
using System.Collections.Generic;

namespace Boxty.Models.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<OrderOutputModel> AllOrders();
        void CreateOrder(Order order);
    }
}