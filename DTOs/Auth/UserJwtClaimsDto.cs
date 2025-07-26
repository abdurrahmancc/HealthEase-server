using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Auth
{
    public class UserJwtClaimsDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        public string Role { get; set; } = "Patient";

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
