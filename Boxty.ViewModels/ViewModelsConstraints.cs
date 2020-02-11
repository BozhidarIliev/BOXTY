using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels
{
    class ViewModelsConstraints
    {
		public const int UserModelNicknameMaxLength = 100;

		public const int UserModelNicknameMinLength = 5;

		public const string ErrorMessageInRegisterModel = "Your nickname/username should contain only alphabet symbols and they should not be cyrilic";

		public const string RegexForValidationNicknameOrUsername = "[A-Za-z]+";

		public const string ConfirmPasswordDisplay = "Confirm Password";

		public const int TitleMaxLength = 100;

		public const int TitleMinLength = 5;

		public const string StoryImageDisplay = "Story Image";
	}
}
