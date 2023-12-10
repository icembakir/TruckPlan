using TruckPlan.DataAccess.Models;

namespace TruckPlan.API.Extensions
{
    public static class VoyageFilter
    {
        public static List<Voyage> DriverAtAge(this List<Voyage> voyages, int? driverAge)
        {
            if (driverAge == null)
            {
                return voyages;
            }
            return voyages.Where(v => v.Driver.BirthDate.AddYears((int)driverAge) <=  DateTime.Now && 
                                      v.Driver.BirthDate.AddYears((int)driverAge) > DateTime.Now.AddYears(-1)).ToList();            
        }

        public static List<Voyage> VoyageAtMonthAndYear(this List<Voyage> voyages, int month, int year)
        {
            return voyages.Where(v => v.SignalReceivedDate.Month == month && v.SignalReceivedDate.Year == year).ToList();
        }

        public static List<Voyage> InCountry(this List<Voyage> voyages, string country)
        {
            return voyages.Where(x => x.Location.Any(l => l.Country == country)).ToList();
        }
        
    }
}
