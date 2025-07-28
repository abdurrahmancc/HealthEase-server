using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.Models.Auth
{
    [Table("AspNetRoles")]
    public class ApplicationRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Description { get; set; }
    }

}
