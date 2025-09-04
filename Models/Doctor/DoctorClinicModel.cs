using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorClinicModel
    {
        [Key]
        public int ClinicId { get; set; }
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Address { get; set; }
        public List<string> ClinicLogos { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
