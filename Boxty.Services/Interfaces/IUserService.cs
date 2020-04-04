using Boxty.Models;
using Boxty.ViewModels;
using Boxty.ViewModels.InputModels;
using Boxty.ViewModels.OutputModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boxty.Services
{
    public interface IUserService
    {
		BoxtyUser GetCurrentUser();
		SignInResult LogUser(LoginInputModel loginModel);

		Task<SignInResult> RegisterUser(RegisterInputModel registerModel);

		void Logout();

		UserOutputModel GetUser(string nickName);
		int UpdateShippingInfo(UpdateUserViewModel model);

		bool CheckCurrentUserBeforePurchase();
	}
}
