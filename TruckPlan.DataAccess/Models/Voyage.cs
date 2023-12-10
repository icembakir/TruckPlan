using Amazon.DynamoDBv2.DataModel;

namespace TruckPlan.DataAccess.Models
{
    public class Voyage
    {
        [DynamoDBHashKey]
        public string PartitionKey { get; set; }

        [DynamoDBRangeKey]
        public string SortKey { get; set; }
        public Driver Driver { get; set; }
        public Truck Truck { get; set; }
        public List<Location> Location { get; set; }
        public List<Coordinate> Coordinate { get; set; }
        public DateTime SignalReceivedDate { get; set; }
    }
}
