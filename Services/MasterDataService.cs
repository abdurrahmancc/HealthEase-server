using HealthEase.Data;
using HealthEase.DTOs;
using HealthEase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthEase.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MasterDataService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<AppLanguage>> GetLanguagesService()
        {
            var languages = await _appDbContext.AppLanguages.ToListAsync();
            return languages;
        }

        public async Task<List<AppListCountry>> GetCountriesService()
        {
            var countries = await _appDbContext.AppListCountries.ToListAsync();
            return countries;
        }
    }
}
