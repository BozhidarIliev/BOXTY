namespace Boxty.Controllers.Api
{
    using Boxty.Models;
    using Boxty.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public ActionResult CreateOrder(BaseOrder[] orders)
        {
            // orderService.CreateOrder(order,orders);
            return null;
        }
    }
}
