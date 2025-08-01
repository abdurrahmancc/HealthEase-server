using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class AwardModel
    {
        [Key]
        public int AwardId { get; set; }

        [Required]
        public string AwardName { get; set; }

        [Required]
        public DateTime AwardYear { get; set; }

        [Required]
        public string AwardDescription { get; set; }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
