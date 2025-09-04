using HealthEase.Enums;
using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs.Doctor
{
    public class DoctorExperienceReadDto
    {
        public Guid ExperienceId { get; set; }

        public string Title { get; set; }

        public string HospitalLogo { get; set; }

        public string Hospital { get; set; }

        public string YearOfExperience { get; set; }

        public string Location { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public string JobDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsCurrentlyWorking { get; set; }

        public Guid DoctorId { get; set; }
    }
}
