using HealthEase.DTOs.Auth;

namespace HealthEase.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto> LoginUserService(LoginRequestDto loginData);
    }
}
