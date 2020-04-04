using Boxty.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Areas.Waiter.Controllers
{
    [Authorize(Roles = "waiter, admin")]
    public class WaiterController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Reservations()
        {
            return this.View();
        }
    }
}
