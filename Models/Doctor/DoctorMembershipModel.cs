using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Doctor
{
    public class DoctorMembershipModel
    {
        [Key]
        public Guid MembershipId { get; set; }
        public string Title { get; set; }
        public string About { get; set; }

        public Guid DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DoctorModel Doctor { get; set; }
    }
}
    