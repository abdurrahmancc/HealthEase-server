using AutoMapper;
using HealthEase.Data;
using HealthEase.DTOs.Auth;
using HealthEase.Helpers;
using HealthEase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthEase.Services.Auth
{
    public class LoginService : ILoginService
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;

        public LoginService(AppDbContext appDbContext, JwtService jwtService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> LoginUserService(LoginRequestDto loginData)
        {

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => (u.Email == loginData.Email || u.Username == loginData.Email) && u.Password == loginData.Password);
            if (user != null)
            {
                var userClaims = new UserJwtClaimsDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username,
                    Role = user.Role,
                    Country = user.Country,
                    CountryCode = user.CountryCode,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                var jwtToken = _jwtService.GenerateJwtToken(userClaims);
                var response = new LoginResponseDto
                {
                    Id = user.Id,
                    UserName = user.Username,
                    Status = user.Status,
                    Token = jwtToken
                };
                return response;
            }
            return null;
        }
    }
}
