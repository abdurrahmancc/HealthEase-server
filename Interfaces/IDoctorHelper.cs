using HealthEase.Models.Doctor;

namespace HealthEase.Helpers
{
    public interface IDoctorHelper
    {
        Task<DoctorModel> GetOrCreateDoctorAsync(string? userId = null);
        Guid GetCurrentUserId(string? userId = null);
        bool IsDoctor();
    }
}

