namespace HealthEase.Models.Doctor
{
    public class DoctorExperience
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string HospitalName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Doctor Doctor { get; set; }
    }
}
