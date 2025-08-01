using AutoMapper;
using HealthEase.DTOs.Doctor;
using HealthEase.Models.Doctor;

namespace HealthEase.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<DoctorBasicInfoModel, DoctorBasicInfoCreateDto>();
            CreateMap<DoctorBasicInfoCreateDto, DoctorBasicInfoModel>();
            CreateMap<DoctorBasicInfoCreateDto, DoctorMembershipModel>();
            CreateMap<DoctorMembershipModel, DoctorBasicInfoCreateDto>();
            CreateMap<DoctorMembershipModel, DoctorMembershipCreateDto>();
            CreateMap<DoctorMembershipCreateDto, DoctorMembershipModel>();
        }
    }
}
