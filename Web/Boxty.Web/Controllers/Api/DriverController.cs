namespace Boxty.Web.Controllers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Common;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.DriverArea)]
    [Route(GlobalConstants.DefaultApiRoute)]
    [Authorize(Roles = GlobalConstants.DriverControllerAuthorizeRoles)]
    [ApiController]
    public class DriverController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService, IOrderService orderService)
        {
            this.orderService = orderService;
            this.driverService = driverService;
        }

        [HttpGet]
        public List<OrderDriverViewModel> GetCurrentOrderItems()
        {
            return driverService.GetCurrentOrderItems();
        }

        [HttpPost]
        [Route("MarkAsCompleted")]
        public async Task MarkAsCompleted(int orderId)
        {
            await orderService.MarkAsCompleted(orderId);
        }
    }
}
