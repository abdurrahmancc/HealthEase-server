using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthEase.DTOs
{
    [Table("AppListCountries")]
    public class AppListCountry
    {
        public int Id { get; set; }

        public string CountryId { get; set; }

        public string Name { get; set; }

        public string Utc { get; set; }

        public int? Order { get; set; }

        public string LangCode { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }

        public string Prefix { get; set; }
    }
}
