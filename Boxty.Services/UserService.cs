namespace Boxty.Services
{
    using AutoMapper;
    using Boxty.Data;
    using Boxty.Models;
    using Boxty.Services;
    using Boxty.ViewModels;
    using Boxty.ViewModels.InputModels;
    using Boxty.ViewModels.OutputModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : BaseService, IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserService(SignInManager<BoxtyUser> signInManager, UserManager<BoxtyUser> userManager, BoxtyDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(userManager, context, mapper)
        {
            this.SignInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected SignInManager<BoxtyUser> SignInManager { get; }

        public SignInResult LogUser(LoginInputModel loginModel)
        {
            var user = this.Context.Users.FirstOrDefault(x => x.UserName == loginModel.UserName);

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
            bool uniqueUsername = this.Context.Users.All(x => x.UserName != registerModel.UserName);

            if (!uniqueUsername)
            {
                return SignInResult.Failed;
            }

            var user = Mapper.Map<BoxtyUser>(registerModel);

            await this.UserManager.CreateAsync(user);
            await this.UserManager.AddPasswordAsync(user, registerModel.Password);
            await this.UserManager.AddToRoleAsync(user, GlobalConstants.DefaultRole);
            var result = await this.SignInManager.PasswordSignInAsync(user, registerModel.Password, true, false);

            return result;
        }

        public async void Logout()
        {
            await this.SignInManager.SignOutAsync();
        }

        public UserOutputModel GetUser(string userName)
		{
            var user = this.UserManager.FindByNameAsync(userName).Result;

			var result = Mapper.Map<UserOutputModel>(user);
			result.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

			return result;
		}

        public int UpdateShippingInfo(UpdateUserViewModel model)
        {
            var user = Context.Users.First(x => x.Id == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;

            return Context.SaveChanges();
        }

        public bool CheckCurrentUserBeforePurchase()
        {
            BoxtyUser user = Context.Users.First(x => x.Id == httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            if (user.FirstName == null || user.LastName == null ||
                user.PhoneNumber == null || user.Address == null)
            {
                return true;
            }
            else return false;
        }
	}
}