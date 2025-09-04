using HealthEase.DTOs;
using HealthEase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthEase.Controllers
{
    [ApiController]
    [Route("v1/api")]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
        _masterDataService = masterDataService;
        }


        [HttpGet("GetLanguages")]
        public async Task<List<AppLanguage>> GetLanguages()
        {
            var languages = await _masterDataService.GetLanguagesService();
            return languages;
        }

        [HttpGet("GetCountries")]
        public async Task<List<AppListCountry>> GetCountries()
        {
            var countries = await _masterDataService.GetCountriesService();
            return countries;
        }

    }
}
