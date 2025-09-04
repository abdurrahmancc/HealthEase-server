using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Doctor
{
    public class DoctorClinicReadDto
    {
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public List<string> ClinicLogos { get; set; }
        public Guid DoctorId { get; set; }
    }
}
