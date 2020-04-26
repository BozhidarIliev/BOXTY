namespace Boxty.Web.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

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
