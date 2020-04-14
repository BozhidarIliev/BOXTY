namespace Boxty.Services
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using AutoMapper;
    using Boxty.Common;
    using Boxty.Data.Models;
    using Boxty.ViewModels;
    using Boxty.ViewModels.InputModels;
    using Boxty.ViewModels.OutputModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly UserManager<BoxtyUser> userManager;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(SignInManager<BoxtyUser> signInManager, UserManager<BoxtyUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.SignInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected SignInManager<BoxtyUser> SignInManager { get; }

        public BoxtyUser GetCurrentUser()
        {
            return this.userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
        }

        public SignInResult LogUser(LoginInputModel loginModel)
        {
            var user = this.userManager.FindByNameAsync(loginModel.UserName).Result;

            if (user == null)
            {
                return SignInResult.Failed;
            }

            var password = loginModel.Password;
            var result = this.SignInManager.PasswordSignInAsync(user, password, true, false).Result;

            return result;
        }

        public async Task<SignInResult> RegisterUser(RegisterInputModel registerModel)
        {
            var uniqueUsername = this.userManager.FindByNameAsync(registerModel.UserName).Result;

            if (uniqueUsername != null)
            {
                return SignInResult.Failed;
            }

            BoxtyUser user = mapper.Map<BoxtyUser>(registerModel);

            await this.userManager.CreateAsync(user);
            await this.userManager.AddPasswordAsync(user, registerModel.Password);
            await this.userManager.AddToRoleAsync(user, GlobalConstants.DefaultRole);
            var result = await this.SignInManager.PasswordSignInAsync(user, registerModel.Password, true, false);

            return result;
        }

        public async void Logout()
        {
            await this.SignInManager.SignOutAsync();
        }

        public UserOutputModel GetUser(string userName)
        {
            var user = this.userManager.FindByNameAsync(userName).Result;

            var result = mapper.Map<UserOutputModel>(user);
            result.Role = this.userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

            return result;
        }

        public async Task<IdentityResult> UpdateShippingInfo(UpdateUserViewModel model)
        {
            var user = this.userManager.FindByIdAsync(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
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
