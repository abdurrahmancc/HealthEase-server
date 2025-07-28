using AutoMapper;
using HealthEase.Data;
using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using HealthEase.Models.Auth;

namespace HealthEase.Services.Auth
{
    public class UserService :IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(AppDbContext appDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<UserDto> GetLoginUserService()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return null;

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
