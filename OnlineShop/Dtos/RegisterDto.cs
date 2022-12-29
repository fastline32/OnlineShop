using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage = "Password must have atleast 1 Uppercase, 1 Lowercase, 1 Digit, 1 non alphanumeric and min 6 characters")]
        public string Password { get; set; }
    }
}
