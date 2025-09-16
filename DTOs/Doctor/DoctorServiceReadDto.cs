namespace HealthEase.DTOs.Doctor
{
    public class DoctorServiceReadDto
    {
        public int? Id { get; set; }
        public string Speciality { get; set; }
        public string Service { get; set; }
        public decimal Price { get; set; }
        public string? About { get; set; }
        public Guid? DoctorId { get; set; }
    }
}
