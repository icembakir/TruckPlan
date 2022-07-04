using TruckPlan.API.Dtos;

namespace TruckPlan.API.Services
{
    public interface ITruckPlanService
    {
        Task CreateVoyageAsync(VoyageRequestDto truckPlanRequestDto);
        Task UpdateVoyageAsync(string voyageId, CoordinateDto coordinate);
        Task<bool> CheckIfVoyageExists(string voyageId);
        Task<double> GetTotalDistanceOfVoyagesByFilters(int age, int month, int year, string country);
    }
}
