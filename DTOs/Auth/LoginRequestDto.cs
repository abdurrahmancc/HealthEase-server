using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Auth
{
    public class LoginRequestDto
    {
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_]{3,20}|[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$",
        ErrorMessage = "Provide valid username (3‑20 chars) or email.")]
        public string Email { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

    }
}
