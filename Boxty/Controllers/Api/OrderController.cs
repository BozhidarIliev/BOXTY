using Boxty.Models;
using Boxty.Services;
using Boxty.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Controllers.Api
{
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
            //orderService.CreateOrder(order,orders);
            return null;
        }
    }
}
