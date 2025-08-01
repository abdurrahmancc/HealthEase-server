using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorBasicInfoModel
    {
        [Key]
        public Guid BasicInfoId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string Designation { get; set; }
        public string Specialty { get; set; }
        public List<string> LanguagesSpoken { get; set; }
        public bool IsFullTimeAvailable { get; set; }
        public bool IsOnlineTherapyAvailable { get; set; }
        public bool AcceptingNewPatients { get; set; }
        public int ReviewCount { get; set; }
        public double Rating { get; set; }
        public double RecommendationPercentage { get; set; }
        public int TotalYearsOfPractice { get; set; }
        public string Bio { get; set; }
        public decimal MinSessionPrice { get; set; }
        public decimal MaxSessionPrice { get; set; }
        public bool IsChatAvailable { get; set; }
        public bool IsAudioCallAvailable { get; set; }
        public bool IsVideoCallAvailable { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
