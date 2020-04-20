namespace Boxty.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Data.Models;
    using Boxty.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly UserManager<BoxtyUser> userManager;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(SignInManager<BoxtyUser> signInManager, UserManager<BoxtyUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.SignInManager = signInManager;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected SignInManager<BoxtyUser> SignInManager { get; }

        public BoxtyUser GetCurrentUser()
        {
            return this.userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
        }

        public async Task<IdentityResult> UpdateShippingInfo(UpdateUserViewModel model)
        {
            var user = this.GetCurrentUser();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            return await this.userManager.UpdateAsync(user);
        }

        public bool CheckCurrentUserBeforePurchase()
        {
            BoxtyUser boxtyUser = this.GetCurrentUser();

            if (boxtyUser.FirstName == null || boxtyUser.LastName == null ||
                boxtyUser.PhoneNumber == null || boxtyUser.Address == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
