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
        Task<bool> CheckIfVoyageExists(string voyageId);
        Task<List<Voyage>> GetAllVoyagesAsync();
        Task SaveVoyageAsync(Voyage voyage);
    }
}
