namespace Boxty.Web.Areas.Administration.Controllers
{
    using Boxty.Common;
    using Boxty.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.Admin)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
