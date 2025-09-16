using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HealthEase.Enums;
using System;

namespace HealthEase.Models.Doctor
{

    public class DoctorBusinessHourModel
    {
        [Key]
        public int BusinessHourId { get; set; }
        public WeekDay Day { get; set; }
        public TimeSpan FromUTC { get; set; }
        public TimeSpan ToUTC { get; set; }
        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }

}
