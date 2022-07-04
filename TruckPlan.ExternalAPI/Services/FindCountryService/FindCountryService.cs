using Microsoft.Extensions.Logging;
using System.Net;
using Newtonsoft.Json;
using TruckPlan.ExternalAPI.Dtos;
using System.Net.Http;

namespace TruckPlan.ExternalAPI.Services.FindCountryService
{
    public class FindCountryService : IFindCountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FindCountryService> _logger;
        private string accessKey = Environment.GetEnvironmentVariable("FindCountryApiAccessKey");

        public FindCountryService(IHttpClientFactory httpClient, ILogger<FindCountryService> logger)
        {
            _httpClient = httpClient.CreateClient("FindCountryApi");
            _logger = logger;
        }

        public async Task<string> GetCountryByCoordinate(double latitude, double longitude)
        {
            try
            {
                var response = await _httpClient.GetAsync($"v1/reverse?access_key={accessKey}&query={latitude},{longitude}");
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return null;
                        
                    case HttpStatusCode.OK:                        
                        var content = await response.Content.ReadAsStringAsync();
                        _logger.LogError($"Response for coordinate: {latitude}/{longitude} - {content}");
                        var result = JsonConvert.DeserializeObject<LocationResponseDto>(content);
                        return result.Data[0].Country;
                        
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
