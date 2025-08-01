using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthEase.Models.Doctor
{

    public class DoctorBusinessHourModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }

        public List<BusinessDayHourModel> BusinessHours { get; set; } = new List<BusinessDayHourModel>();
    }

    public class BusinessDayHourModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DayOfWeek Day { get; set; }

        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }

        public bool IsWorking { get; set; }

        public Guid DoctorBusinessHourModelId { get; set; }

        [ForeignKey(nameof(DoctorBusinessHourModelId))]
        public DoctorBusinessHourModel DoctorBusinessHour { get; set; }
    }


}
