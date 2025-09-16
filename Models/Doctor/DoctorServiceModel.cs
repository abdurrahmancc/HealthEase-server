using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorServiceModel
    {
        [Key]
        public int Id { get; set; }
        public string Speciality { get; set; }
        public string Service { get; set; }
        public decimal Price { get; set; }
        public string About { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
