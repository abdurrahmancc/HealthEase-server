using HealthEase.DTOs.Auth;

namespace HealthEase.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterResponseDto> RegisterUserService(RegisterRequestDto registerData);
    }
}
