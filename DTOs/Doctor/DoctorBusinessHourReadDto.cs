using HealthEase.Enums;


namespace HealthEase.DTOs.Doctor
{
    public class DoctorBusinessHourReadDto
    {
        public int BusinessHourId { get; set; }
        public WeekDay Day { get; set; }
        public TimeSpan FromUTC { get; set; }
        public TimeSpan ToUTC { get; set; }
        public Guid DoctorId { get; set; }
    }
}
