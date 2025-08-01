using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class ClinicModel
    {
        public int Id { get; set; }
        [Required]
        public string ClinicName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Addrerss { get; set; }
        public List<string> Gallery { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
