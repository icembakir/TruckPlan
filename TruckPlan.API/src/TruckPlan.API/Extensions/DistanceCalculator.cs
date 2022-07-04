using TruckPlan.DataAccess.Models;

namespace TruckPlan.API.Helpers
{
    public static class DistanceCalculator
    {
        public static Task<double> CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = deg2rad(lat2 - lat1); // deg2rad below
            var dLon = deg2rad(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return Task.FromResult(d);
        }
        
        public static double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public static async Task<double> CalculateTotalDistanceOfSingleVoyage(this List<Coordinate> coordinates)
        {
            double totalDistance = 0;
            for (int i = 0; i < coordinates.Count - 1; i++)
            {
                totalDistance += await CalculateDistance(coordinates[i].Latitude, coordinates[i].Longitude, coordinates[i + 1].Latitude, coordinates[i + 1].Longitude);
            }
            
            return totalDistance;
        }

        public static async Task<double> CalculateTotalDistanceOfVoyages(this List<Voyage> voyages)
        {
            double totalDistance = 0;
            foreach(var voyage in voyages)
            {
                totalDistance += await voyage.Coordinate.CalculateTotalDistanceOfSingleVoyage();
            }

            return totalDistance;
        }
    }
}
