using HealthEase.Enums;

namespace HealthEase.DTOs.Auth
{
    public class LoginResponseDto
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
