using AutoMapper;
using HealthEase.DTOs.Auth;
using HealthEase.Models.Auth;

namespace HealthEase.Profiles.Auth
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginModel, LoginRequestDto>();
            CreateMap<LoginModel, LoginResponseDto>();
            CreateMap<LoginRequestDto, LoginResponseDto>();
            CreateMap<UserModel, LoginResponseDto>();
            CreateMap<UserModel, UserJwtClaimsDto>();
            CreateMap<UserDto, UserJwtClaimsDto>();
        }
    }
}
