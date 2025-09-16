namespace HealthEase.DTOs.Doctor
{
    public class SocialMediaReadDto
    {
        public int Id { get; set; }
        public string SocialMedia { get; set; }
        public string Link { get; set; }
        public Guid DoctorId { get; set; }
    }
}
