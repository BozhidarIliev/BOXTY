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
		SignInResult LogUser(LoginInputModel loginModel);

		Task<SignInResult> RegisterUser(RegisterInputModel registerModel);

		void Logout();

		UserOutputModel GetUser(string nickName);
	}
}
