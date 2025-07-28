namespace HealthEase.Models.Doctor
{
    public class DoctorService
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public Doctor Doctor { get; set; }
    }
}
