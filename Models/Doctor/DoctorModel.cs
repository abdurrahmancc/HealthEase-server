using HealthEase.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorModel
    {
        [Key]
        public Guid DoctorId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public DoctorBasicInfoModel BasicInfo { get; set; }
        public List<AwardModel> Awards { get; set; }
        public List<ClinicModel> Clinics { get; set; }
        public List<DoctorAvailabilityModel> Availabilities { get; set; }
        public List<DoctorBusinessHourModel> BusinessHours { get; set; }
        public List<DoctorExperienceModel> Experiences { get; set; }
        public List<DoctorMembershipModel> Memberships { get; set; }
        public List<DoctorServiceModel> Services { get; set; }
        public List<EducationModel> Educations { get; set; }
    }

}
