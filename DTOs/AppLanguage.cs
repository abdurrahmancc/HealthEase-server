using System.ComponentModel.DataAnnotations.Schema;

namespace HealthEase.DTOs
{
    [Table("AppLanguages")]
    public class AppLanguage
    {
        public int Id { get; set; }
        public string LangCode { get; set;}
        public string LanguageName { get; set; }
    }
}
