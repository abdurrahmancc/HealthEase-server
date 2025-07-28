using AutoMapper;
using HealthEase.DTOs.Auth;
using HealthEase.Models.Auth;

namespace HealthEase.Profiles.Auth
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserDto>();
        }
    }
}
