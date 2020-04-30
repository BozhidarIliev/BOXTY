namespace Boxty.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Boxty.Data.Models;
    using Boxty.ViewModels.OutputModels;
    using Boxty.Web.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface IAdminService
    {
        Task<IdentityResult> CreateRole(CreateRoleViewModel model);

        Task<EditRoleViewModel> GetUserRoles(ApplicationRole role);

        Task<List<UserRoleViewModel>> EditUsersInRole(ApplicationRole role);
    }
}
