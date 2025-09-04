using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorEducationModel
    {
        [Key]
        public Guid EducationId { get; set; }
        public string InstituteName { get; set; }
        public string Course { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyOngoing { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
