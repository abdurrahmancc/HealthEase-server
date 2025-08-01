using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorExperienceModel : IValidatableObject
    {
        [Key]
        public Guid ExperienceId { get; set; }

        [Required]
        public string Title { get; set; }        
        
        [Required]
        public string HospitalLogo { get; set; }

        [Required]
        public string Hospital { get; set; }

        [Required]
        public string YearOfExperience { get; set; }

        [Required]
        public string Location { get; set; }

        public string Employement { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public string StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyWorking { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate == null && IsCurrentlyWorking != true)
            {
                yield return new ValidationResult(
                    "Either EndDate must be provided or IsCurrentlyWorking must be true.",
                    new[] { nameof(EndDate), nameof(IsCurrentlyWorking) });
            }
        }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }

    }
}
