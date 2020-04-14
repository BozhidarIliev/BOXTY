namespace Boxty.Services
{
    using System.Threading.Tasks;

    using Boxty.Data.Models;
    using Boxty.ViewModels;
    using Boxty.ViewModels.InputModels;
    using Boxty.ViewModels.OutputModels;
    using Microsoft.AspNetCore.Identity;

    public interface IUserService
    {
        BoxtyUser GetCurrentUser();

        SignInResult LogUser(LoginInputModel loginModel);

        Task<SignInResult> RegisterUser(RegisterInputModel registerModel);

        void Logout();

        UserOutputModel GetUser(string nickName);

        Task<IdentityResult> UpdateShippingInfo(UpdateUserViewModel model);

        bool CheckCurrentUserBeforePurchase();
    }
}
