namespace Boxty.Web.Controllers.Api
{
    using System.Collections.Generic;
    using Boxty.Common;
    using Boxty.Services.Data.Interfaces;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area(GlobalConstants.DriverArea)]
    [Route(GlobalConstants.DefaultApiRoute)]
    [ApiController]
    public class DriverController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IOrderItemService orderItemService;
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.orderService = orderService;
            this.orderItemService = orderItemService;
            this.driverService = driverService;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.DriverControllerAuthorizeRoles)]
        public IEnumerable<OrderDriverViewModel> GetCurrentOrderItems()
        {
            return driverService.GetCurrentOrderItems();
        }
    }
}
