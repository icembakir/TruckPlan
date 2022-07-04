using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlan.DataAccess.Models;

namespace TruckPlan.DataAccess.Repositories
{
    public interface ITruckPlanRepository
    {
        Task UpdateVoyageAsync(string voyageId, Coordinate cordinate, string country);
        Task<bool> CheckIfVoyageExists(string voyageId);
        Task CreateVoyageAsync(Voyage voyage);
        Task<List<Voyage>> GetAllVoyagesAsync();
    }
}
