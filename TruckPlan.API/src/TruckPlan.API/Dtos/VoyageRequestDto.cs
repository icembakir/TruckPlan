namespace TruckPlan.API.Dtos
{
    public class VoyageRequestDto
    {
        public string VoyageId { get; set; }
        public DriverDto Driver { get; set; }
        public TruckDto Truck { get; set; }        
        public CoordinateDto Coordinate { get; set; }
        public DateTime SignalReceivedDate { get; set; }

    }
}
