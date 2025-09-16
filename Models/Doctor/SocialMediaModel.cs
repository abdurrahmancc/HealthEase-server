using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class SocialMediaModel
    {
        public int Id { get; set; }
        public string SocialMedia { get; set; }
        public string Link { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
