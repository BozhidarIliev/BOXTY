namespace Boxty.Services
{
    using System.Threading.Tasks;

    using Boxty.Data.Models;
    using Boxty.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface IUserService
    {
        BoxtyUser GetCurrentUser();

        Task<IdentityResult> UpdateShippingInfo(UpdateUserViewModel model);

        bool CheckCurrentUserBeforePurchase();
    }
}
