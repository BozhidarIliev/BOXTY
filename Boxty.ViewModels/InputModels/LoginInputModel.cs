using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.ViewModels.InputModels
{
    public class LoginInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.UserModelNicknameMaxLength, MinimumLength = ViewModelsConstants.UserModelNicknameMinLength)]
        [RegularExpression(ViewModelsConstants.RegexForValidationNicknameOrUsername)]
        public string Nickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
