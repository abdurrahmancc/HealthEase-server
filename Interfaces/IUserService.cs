using HealthEase.DTOs;
using HealthEase.DTOs.Auth;
using HealthEase.Enums;

namespace HealthEase.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetLoginUserService();
        Task<PaginatedResult<UserDto>> GetUsersService(int pageNumber, int pageSize, string search, UserStatus? status, string role, string country);
        Task<object> UpdateRoleService(Guid id, UserRole role);
        Task<UserDto> GetUserByIdService(Guid id);
        Task<UserDto> GetUserByEmailService(string email);
    }
}
