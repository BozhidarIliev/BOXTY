namespace Boxty.Controllers.Api
{
    using Boxty.Models;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<OrderOutputModel> GetOrders()
        {
            //return orderService.CurrentOrders();
            return null;
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderItem[] orders)
        {
            // orderService.CreateOrder(order,orders);
            return null;
        }
    }
}
