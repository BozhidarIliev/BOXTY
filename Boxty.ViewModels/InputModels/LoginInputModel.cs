﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.ViewModels.InputModels
{
    public class LoginInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.UserModelUserNameMaxLength, MinimumLength = ViewModelsConstants.UserModelUserNameMinLength)]
        [RegularExpression(ViewModelsConstants.RegexForValidationNicknameOrUsername)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}