using TruckPlan.DataAccess.Models;

namespace TruckPlan.API.Extensions
{
    public static class VoyageFilter
    {
        public static List<Voyage> DriverAtAge(this List<Voyage> voyages, int driverAge)
        {
            return voyages.Where(v => v.Driver.BirthDate.AddYears(driverAge) <=  DateTime.Now && 
                                      v.Driver.BirthDate.AddYears(driverAge) > DateTime.Now.AddYears(-1)).ToList();            
        }

        public static List<Voyage> VoyageAtMonthAndYear(this List<Voyage> voyages, int month, int year)
        {
            return voyages.Where(v => v.SignalReceivedDate.Month == month && v.SignalReceivedDate.Year == year).ToList();
        }

        public static List<Voyage> InCountry(this List<Voyage> voyages, string country)
        {
            return voyages.Where(v => v.Country.Contains(country)).ToList();
        }
        
    }
}
