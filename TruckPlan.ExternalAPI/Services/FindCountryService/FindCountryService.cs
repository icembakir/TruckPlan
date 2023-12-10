using Microsoft.Extensions.Logging;
using System.Net;
using Newtonsoft.Json;
using TruckPlan.ExternalAPI.Dtos;
using System.Net.Http;
using TruckPlan.ExternalAPI.Configuration;

namespace TruckPlan.ExternalAPI.Services.FindCountryService
{
    public class FindCountryService : IFindCountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FindCountryService> _logger;
        private readonly ExternalApiSettings _externalApiSettings;

        public FindCountryService(IHttpClientFactory httpClient, ILogger<FindCountryService> logger, ExternalApiSettings externalApiSettings)
        {
            _httpClient = httpClient.CreateClient("FindCountryApi");
            _logger = logger;
            _externalApiSettings = externalApiSettings;
        }

        public async Task<LocationResponseDto> GetCountryByCoordinate(double latitude, double longitude)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v1/reverse?access_key={_externalApiSettings.ApiKey}&query={latitude},{longitude}");
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        _logger.LogError("External api coud not be found");
                        return null;
                        
                    case HttpStatusCode.OK:                        
                        var content = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation($"Response for coordinate: {latitude}/{longitude} - {content}");
                        var result = JsonConvert.DeserializeObject<LocationResponseDto>(content);
                        return result;
                        
                    default:
                        _logger.LogInformation($"External api returned non handled status code: {response.StatusCode}");
                        return null;
                }                
            }
            catch (Exception)
            {
                _logger.LogError($"Error while getting country by coordinate: {latitude}/{longitude}");
                return null;
            }
        }
    }
}
