using HealthEase.DTOs.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace HealthEase.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorBasicInfoCreateDto> UpdateBasicInfoService(DoctorBasicInfoCreateDto info);
        Task<string> UpdatePhotoUrlServiceeAsync(IFormFile file);
    }
}
