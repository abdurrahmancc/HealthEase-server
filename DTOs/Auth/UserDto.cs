using HealthEase.Enums;

namespace HealthEase.DTOs.Auth
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string PhotoUrl { get; set; }

        public string Role { get; set; }

        public string IPAddress { get; set; }

        public string Status { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public List<string> LoginDevices { get; set; } = new List<string>();

        public DateTime LastLoginDate { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}
