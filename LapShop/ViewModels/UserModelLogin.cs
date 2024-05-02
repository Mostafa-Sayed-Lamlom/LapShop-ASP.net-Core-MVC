using System.ComponentModel.DataAnnotations;

namespace LapShop.ViewModels
{
    public class UserModelLogin
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password length: Must be between 6 and 100 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#$%^&*()_+=-{}|;':\"\\|,.<>\\/?])[a-zA-Z0-9#$%^&*()_+=-{}|;':\"\\|,.<>\\/?]{6,}$", ErrorMessage = "Password Character requirements:\r\nMust include at least one lowercase letter\r\nMust include at least one uppercase letter\r\nMust include at least one number\r\nMust include at least one special symbol")]
        public string Password { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; } = string.Empty;
    }
}
