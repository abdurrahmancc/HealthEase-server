using AutoMapper;
using HealthEase.Data;
using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using HealthEase.Models.Auth;

namespace HealthEase.Services.Auth
{
    public class RegisterService : IRegisterService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public RegisterService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<RegisterResponseDto> RegisterUserService(RegisterRequestDto registerData)
        {
            var newUser = _mapper.Map<UserModel>(registerData);
            newUser.Id = Guid.NewGuid();
            newUser.LastLoginDate = DateTime.Now;
            newUser.CreateAt = DateTime.Now;
            newUser.UpdateAt = DateTime.Now;
            newUser.Username = newUser.Email.Split('@')[0];

            await _appDbContext.AddAsync(newUser);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<RegisterResponseDto>(newUser);
        }

    }
}
