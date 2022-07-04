using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckPlan.ExternalAPI.Services.FindCountryService
{
    public interface IFindCountryService
    {
        Task<string> GetCountryByCoordinate(double latitude, double longitude);
    }
}
