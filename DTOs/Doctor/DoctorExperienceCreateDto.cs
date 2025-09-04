using HealthEase.Enums;
using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Doctor
{
    public class DoctorExperienceCreateDto
    {
        public Guid ExperienceId { get; set; }

        [Required]
        public string Title { get; set; }

        public string HospitalLogo { get; set; }

        public IFormFile? HospitalLogoFile { get; set; }

        [Required]
        public string Hospital { get; set; }

        [Required]
        public string YearOfExperience { get; set; }

        [Required]
        public string Location { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        [Required]
        public string JobDescription { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

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

        //public Guid DoctorId { get; set; }
    }
}
