using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorServiceModel
    {
        [Key]
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
