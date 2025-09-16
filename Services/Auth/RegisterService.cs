using AutoMapper;
using HealthEase.Data;
using HealthEase.DTOs.Auth;
using HealthEase.Interfaces;
using HealthEase.Models.Auth;
using Microsoft.EntityFrameworkCore;

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

            var emailExists = await _appDbContext.Users.AnyAsync(u => EF.Functions.Like(u.Email, registerData.Email));

            if (emailExists)
            {
                throw new Exception("Email already registered.");
            }

            var newUser = _mapper.Map<UserModel>(registerData);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(registerData.Password);
            newUser.Id = Guid.NewGuid();
            newUser.LastLoginDate = DateTime.Now;
            newUser.CreateAt = DateTime.Now;
            newUser.UpdateAt = DateTime.Now;
            newUser.Username = newUser.Email.Split('@')[0];

            await _appDbContext.AddAsync(newUser);
            await _appDbContext.SaveChangesAsync();


            var defaultRole = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Patient");
            if (defaultRole != null)
            {
                var newUserRole = new ApplicationUserRole
                {
                    UserId = newUser.Id,
                    RoleId = defaultRole.Id,
                };
                await _appDbContext.AddAsync(newUserRole);
                await _appDbContext.SaveChangesAsync();
            }


            return _mapper.Map<RegisterResponseDto>(newUser);
        }

    }
}
