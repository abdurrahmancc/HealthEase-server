using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Doctor
{
    public class DoctorEducationCreateDto
    {
        public Guid EducationId { get; set; }
        public string InstituteName { get; set; }
        public string Course { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyOngoing { get; set; }
        public Guid? DoctorId { get; set; }
    }
}
