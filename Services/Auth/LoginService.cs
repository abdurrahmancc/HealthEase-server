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
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => (u.Email == loginData.Email || u.Username == loginData.Email) && u.Password == loginData.Password);
                if (user != null)
                {
                    var userClaims = _mapper.Map<UserJwtClaimsDto>(user);
                    var jwtToken = _jwtService.GenerateJwtToken(userClaims);
                    var result = _mapper.Map<LoginResponseDto>(user);
                    result.Token = jwtToken;
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while logging in.", ex);
            }

        }
    }
}
