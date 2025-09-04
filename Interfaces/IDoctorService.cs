using HealthEase.DTOs.Doctor;
using HealthEase.Models.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace HealthEase.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorBasicInfoCreateDto> UpdateBasicInfoService(DoctorBasicInfoCreateDto info);
        Task<DoctorBasicInfoCreateDto> GetBasicInfoService(string userId);
        Task<string> UpdatePhotoUrlServiceeAsync(IFormFile file);
        Task<List<DoctorMembershipReadDto>> UpdateMembershipsService(List<DoctorMembershipCreateDto> memberships, string userId);
        Task<List<DoctorMembershipReadDto>> GetMembershipsService(string userId);
        Task<List<DoctorExperienceReadDto>> AddUpdateExperiencesService(List<DoctorExperienceCreateDto> experiences, string userId);
        Task<List<DoctorExperienceReadDto>> GetExperiencesService(string userId);
        Task<List<DoctorEducationReadDto>> AddUpdateEducationsService(List<DoctorEducationCreateDto> educations, string userId);
        Task<List<DoctorEducationReadDto>> GetEducationsService(string userId);
        Task<List<DoctorClinicReadDto>> AddUpdateClinicsService(List<DoctorClinicCreateDto> clinics, string userId);
        Task<List<DoctorClinicReadDto>> GetClinicsService(string userId);
    }
}
