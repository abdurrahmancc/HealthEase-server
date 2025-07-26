using AutoMapper;
using HealthEase.DTOs.Auth;
using HealthEase.Models.Auth;

namespace HealthEase.Profiles.Auth
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterRequestDto, UserModel>();
            CreateMap<UserModel, RegisterRequestDto>();
            CreateMap<UserModel, RegisterResponseDto>();
        }
    }
}
