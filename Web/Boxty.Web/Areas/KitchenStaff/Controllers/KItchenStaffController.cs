using Boxty.Common;
using Boxty.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxty.Web.Areas.KitchenStaff.Controllers
{
    [Area(GlobalConstants.KitchenStaffArea)]
    public class KitchenStaffController : Controller
    {
        public KitchenStaffController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }

    }
}
