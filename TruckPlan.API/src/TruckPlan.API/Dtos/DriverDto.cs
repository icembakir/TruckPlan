using System;

namespace TruckPlan.API.Dtos
{
    public class DriverDto
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
