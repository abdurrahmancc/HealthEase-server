using HealthEase.DTOs.Auth;

namespace HealthEase.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetLoginUserService();
    }
}
