using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckPlan.ExternalAPI.Dtos;

namespace TruckPlan.ExternalAPI.Services.FindCountryService
{
    public interface IFindCountryService
    {
        Task<LocationResponseDto> GetCountryByCoordinate(double latitude, double longitude);
    }
}
