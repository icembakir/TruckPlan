using TruckPlan.API.Dtos;

namespace TruckPlan.API.Services
{
    public interface ITruckPlanService
    {
        Task SaveVoyageAsync(VoyageRequestDto voyageRequestDto);
        Task<bool> CheckIfVoyageExists(string voyageId);
        Task<double> GetTotalDistanceOfVoyagesByFilters(int age, int month, int year, string country);
    }
}