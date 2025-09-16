using HealthEase.Data;
using HealthEase.Models.Doctor;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace HealthEase.Helpers
{
    public class DoctorHelper : IDoctorHelper
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorHelper(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DoctorModel> GetOrCreateDoctorAsync(string? userId = null)
        {
            var userGuid = GetCurrentUserId(userId);

            if (!IsDoctor())
                throw new UnauthorizedAccessException("You are not authorized to perform this action");

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userGuid);

            if (doctor == null)
            {
                doctor = new DoctorModel
                {
                    DoctorId = Guid.NewGuid(),
                    UserId = userGuid
                };
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }

            return doctor;
        }

        public Guid GetCurrentUserId(string? userId = null)
        {
            var UId = userId ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(UId))
                throw new UnauthorizedAccessException("User is not authorized");

            if (!Guid.TryParse(UId, out var userGuid))
                throw new ArgumentException("Invalid User ID");

            return userGuid;
        }

        public bool IsDoctor()
        {
            return _httpContextAccessor.HttpContext?.User.IsInRole("Doctor") ?? false;
        }
    }
}
