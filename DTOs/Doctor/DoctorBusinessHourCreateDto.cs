using HealthEase.Enums;
using HealthEase.Models.Doctor;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.DTOs.Doctor
{
    public class DoctorBusinessHourCreateDto
    {
        public int BusinessHourId { get; set; }
        public WeekDay Day { get; set; }
        public TimeSpan FromUTC { get; set; }
        public TimeSpan ToUTC { get; set; }
        public Guid DoctorId { get; set; }
    }
}
