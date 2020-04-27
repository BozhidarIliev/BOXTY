using Boxty.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Web.Areas.KitchenStaff.Controllers
{
    [Area("KitchenStaff")]
    public class KitchenStaffController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;

        public KitchenStaffController(IOrderService orderService, IOrderItemService orderItemService)
        {
            this.orderService = orderService;
            this.orderItemService = orderItemService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

    }
}
