using System.ComponentModel.DataAnnotations;

namespace NetXUnit.Webapi.Models
{
    static class ErrorMessages
    {
        public const string Required = "The field is required. Cannot be null or empty";
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple =false)]
    public sealed class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute() : base(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
        {
            ErrorMessage = "Invalid email address";
        }
    }

    public class UserModel
    {
        [MaxLength(50), Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public string? LastName { get; set; }
        [Email, Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessages.Required), MaxLength(60)]
        public required string Email { get; set; }
    }
}
