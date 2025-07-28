namespace HealthEase.Models.Doctor
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Specialty { get; set; }
        public string Degrees { get; set; }
        public string LanguagesSpoken { get; set; }
        public string ClinicAddress { get; set; }
        public string LocationDetails { get; set; }
        public bool IsFullTimeAvailable { get; set; }
        public bool IsOnlineTherapyAvailable { get; set; }
        public bool AcceptingNewPatients { get; set; }
        public int ReviewCount { get; set; }
        public double Rating { get; set; } 
        public double RecommendationPercentage { get; set; }
        public int YearsOfPractice { get; set; }
        public int AwardCount { get; set; }
        public string Bio { get; set; }
        public decimal MinSessionPrice { get; set; }
        public decimal MaxSessionPrice { get; set; }
        public bool IsChatAvailable { get; set; }
        public bool IsAudioCallAvailable { get; set; }
        public bool IsVideoCallAvailable { get; set; }
        public List<DoctorExperience> Experiences { get; set; }
        public List<DoctorService> Services { get; set; }
        public List<DoctorAvailability> Availabilities { get; set; }
    }

}
