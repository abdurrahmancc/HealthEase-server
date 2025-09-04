using HealthEase.DTOs;

namespace HealthEase.Interfaces
{
    public interface IMasterDataService
    {
        Task<List<AppLanguage>> GetLanguagesService();
        Task<List<AppListCountry>> GetCountriesService();
    }


}
