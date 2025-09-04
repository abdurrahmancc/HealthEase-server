using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Doctor
{
    public class DoctorClinicCreateDto
    {
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Address { get; set; }
        public List<string>? ClinicLogos { get; set; }
        public List<IFormFile>? ClinicLogosFile { get; set; }
        public Guid DoctorId { get; set; }
    }
}
