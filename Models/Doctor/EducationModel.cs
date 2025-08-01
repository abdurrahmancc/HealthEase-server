using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class EducationModel
    {
        [Key]
        public Guid EducatiopnId { get; set; }

        public string InstituteLogo { get; set; }
        public string InstituteName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyOngoing { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate == null && IsCurrentlyOngoing != true)
            {
                yield return new ValidationResult(
                    "Either EndDate must be provided or CurrentlyWorking must be true.",
                    new[] { nameof(EndDate), nameof(IsCurrentlyOngoing) });
            }
        }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
