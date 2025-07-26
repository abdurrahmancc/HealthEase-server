using HealthEase.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Auth
{
    [Table("AspNetUsers")]
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 length of FirstName")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string PhotoUrl { get; set; }

        public string Role { get; set; } = UserRole.Patient.ToString();

        public string IPAddress { get; set; }

        public string Status { get; set; } = UserStatus.Active.ToString();

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public List<string> LoginDevices { get; set; } = new List<string>();

        public DateTime LastLoginDate { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
