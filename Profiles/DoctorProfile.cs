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
            CreateMap<DoctorMembershipModel, DoctorMembershipReadDto>();
            CreateMap<DoctorMembershipReadDto, DoctorMembershipModel>();
            CreateMap<DoctorMembershipCreateDto, DoctorMembershipReadDto>();
            CreateMap<DoctorMembershipReadDto, DoctorMembershipCreateDto>();

            CreateMap<DoctorExperienceCreateDto, DoctorExperienceModel>();
            CreateMap<DoctorExperienceCreateDto, DoctorExperienceReadDto>();
            CreateMap<DoctorExperienceModel, DoctorExperienceReadDto>();

            CreateMap<DoctorEducationModel, DoctorEducationReadDto>();
            CreateMap<DoctorEducationModel, DoctorEducationCreateDto>();
            CreateMap<DoctorEducationCreateDto, DoctorEducationModel>();
            CreateMap<DoctorEducationCreateDto, DoctorEducationReadDto>();

            CreateMap<DoctorClinicCreateDto, DoctorClinicReadDto>();
            CreateMap<DoctorClinicCreateDto, DoctorClinicModel>();
            CreateMap<DoctorClinicModel, DoctorClinicCreateDto>();
            CreateMap<DoctorClinicModel, DoctorClinicReadDto>();
            
            CreateMap<DoctorBusinessHourModel, DoctorBusinessHourCreateDto>();
            CreateMap<DoctorBusinessHourCreateDto, DoctorBusinessHourModel>();
            CreateMap<DoctorBusinessHourCreateDto, DoctorBusinessHourReadDto>();
            CreateMap<DoctorBusinessHourModel, DoctorBusinessHourReadDto>();

            CreateMap<DoctorServiceCreateDto, DoctorServiceModel>();
            CreateMap<DoctorServiceCreateDto, DoctorServiceReadDto>();
            CreateMap<DoctorServiceModel, DoctorServiceCreateDto>();
            CreateMap<DoctorServiceModel, DoctorServiceReadDto>();


            CreateMap<SocialMediaCreateDto, SocialMediaModel>();
            CreateMap<SocialMediaModel, SocialMediaCreateDto>();
            CreateMap<SocialMediaModel, SocialMediaReadDto>();
            CreateMap<SocialMediaCreateDto, SocialMediaReadDto>();
        }
    }
}
