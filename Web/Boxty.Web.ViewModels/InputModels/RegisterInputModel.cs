namespace Boxty.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        [Required]
        [StringLength(ViewModelsConstants.UserModelUserNameMaxLength, MinimumLength = ViewModelsConstants.UserModelUserNameMinLength)]
        [RegularExpression(ViewModelsConstants.RegexForValidationNicknameOrUsername, ErrorMessage = ViewModelsConstants.ErrorMessageInRegisterModel)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = ViewModelsConstants.ConfirmPasswordDisplay)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
