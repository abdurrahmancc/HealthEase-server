using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Auth
{
    [Table("AspNetUserRoles")]
    public class ApplicationUserRole
    {
        [Key, Column(Order = 0)]
        public Guid UserId { get; set; }

        [Key, Column(Order = 1)]
        public Guid RoleId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
    }
}
