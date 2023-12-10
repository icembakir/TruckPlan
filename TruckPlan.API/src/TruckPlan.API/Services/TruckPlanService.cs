using TruckPlan.API.Dtos;
using TruckPlan.DataAccess.Repositories;
using TruckPlan.DataAccess.Models;
using TruckPlan.ExternalAPI.Services.FindCountryService;
using TruckPlan.API.Extensions;
using TruckPlan.API.Helpers;
using TruckPlan.ExternalAPI.Dtos;
using AutoMapper;

namespace TruckPlan.API.Services
{
    public class TruckPlanService : ITruckPlanService
    {
        private readonly ITruckPlanRepository _truckPlanRepository;
        private readonly IFindCountryService _findCountryService;
        private readonly ILogger<TruckPlanService> _logger;
        private readonly IMapper _mapper;
        
        public TruckPlanService(
            ITruckPlanRepository truckPlanRepository, 
            IFindCountryService findCountryService, 
            ILogger<TruckPlanService> logger,
            IMapper mapper)
        {
            _truckPlanRepository = truckPlanRepository;
            _findCountryService = findCountryService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> CheckIfVoyageExists(string truckPlanId)
        {
            return await _truckPlanRepository.CheckIfVoyageExists(truckPlanId);
        }
        
        public async Task SaveVoyageAsync(VoyageRequestDto voyageRequestDto)
        {
            var latitude = voyageRequestDto.Coordinate.Latitude;
            var longitude = voyageRequestDto.Coordinate.Longitude;
            
            var locationResponse = GetCountryByCoordinateAsync(latitude, longitude).Result;
            var location = _mapper.Map<Location>(locationResponse);

            var voyage = new Voyage
            {
                PartitionKey = $"VOYAGE#{voyageRequestDto.VoyageId}",
                SortKey = voyageRequestDto.VoyageId,
                Driver = new Driver
                {
                    Name = voyageRequestDto.Driver.Name,
                    Surname = voyageRequestDto.Driver.Surname,
                    BirthDate = voyageRequestDto.Driver.BirthDate
                },
                Truck = new Truck
                {
                    TruckId = voyageRequestDto.Truck.TruckId,
                    LicencePlate = voyageRequestDto.Truck.LicencePlate
                },
                Location = new List<Location>
                {
                    location
                },
                Coordinate = new List<Coordinate>
                {
                    new Coordinate
                    {
                        Latitude = latitude,
                        Longitude = longitude
                    }
                },
                SignalReceivedDate = voyageRequestDto.SignalReceivedDate
            };

            await _truckPlanRepository.SaveVoyageAsync(voyage);

        }

        public async Task<double> GetTotalDistanceOfVoyagesByFilters(int? age, int month, int year, string country)
        {
            _logger.LogInformation($"Getting total distance of voyages based on filters, Age:{age} Month:{month} Year:{year} Country:{country}.");
            var voyages = await _truckPlanRepository.GetAllVoyagesAsync();

            var filteredVoyages = voyages.DriverAtAge(age).VoyageAtMonthAndYear(month, year).InCountry(country);

            if (filteredVoyages == null)
            {
                return 0;
            }

            return await filteredVoyages.CalculateTotalDistanceOfVoyages();
        }

        private async Task<LocationResponseDto> GetCountryByCoordinateAsync(double latitude, double longitude)
        {
            return await _findCountryService.GetCountryByCoordinate(latitude, longitude);
        }

        

    }
}
