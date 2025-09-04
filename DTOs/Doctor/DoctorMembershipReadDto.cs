namespace HealthEase.DTOs.Doctor
{
    public class DoctorMembershipReadDto
    {
        public Guid MembershipId { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public Guid DoctorId { get; set; }
    }
}
