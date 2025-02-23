using System.ComponentModel.DataAnnotations;

namespace NetXUnit.Webapi.Models
{
    public class LoginModel
    {
        [Required, MaxLength(60)]
        public required string Email { get; set; }

        [RegularExpression(pattern: "")]
        [Required(AllowEmptyStrings =false), MinLength(8)]
        public required string Password { get; set; }
    }

    public class  UserLoggedIn
    {
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
