namespace Boxty.Web.Controllers
{

    using Boxty.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class OrderItemController : Controller
    {
        private readonly IOrderItemService orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            this.orderItemService = orderItemService;
        }

        public IActionResult CurrentOrderItems()
        {
            return null;
        }
    }
}
