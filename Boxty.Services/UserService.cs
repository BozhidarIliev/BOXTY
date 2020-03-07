namespace Boxty.Services
{
    using AutoMapper;
    using Boxty.Data;
    using Boxty.Models;
    using Boxty.Services;
    using Boxty.ViewModels.InputModels;
    using Boxty.ViewModels.OutputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : BaseService, IUserService
    {
        public UserService(SignInManager<BoxtyUser> signInManager, UserManager<BoxtyUser> userManager, BoxtyDbContext context, IMapper mapper)
            : base(userManager, context, mapper)
        {
            this.SignInManager = signInManager;
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


        public UserOutputModel GetUser(string username)
		{
			var user = this.Context.Users
				.Include(x => x.FirstName)
				.Include(x => x.LastName)
				.FirstOrDefault(x => x.UserName == username);

			var result = Mapper.Map<UserOutputModel>(user);
			result.Role = this.UserManager.GetRolesAsync(user).Result.FirstOrDefault() ?? GlobalConstants.DefaultRole;

			return result;
		}
	}
}