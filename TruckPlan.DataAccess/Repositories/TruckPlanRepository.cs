using Amazon.DynamoDBv2.DataModel;
using TruckPlan.DataAccess.Models;

namespace TruckPlan.DataAccess.Repositories
{
    public class TruckPlanRepository : ITruckPlanRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly DynamoDBOperationConfig _operationConfig;

        public TruckPlanRepository(IDynamoDBContext context)
        {
            _context = context;
            _operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = "voyages-dev"
            };
        }

        public async Task<bool> CheckIfVoyageExists(string voyageId)
        {
            var voyage = await _context.LoadAsync<Voyage>($"VOYAGE#{voyageId}", voyageId, _operationConfig);
            return voyage != null;
        }

        public async Task<List<Voyage>> GetAllVoyagesAsync()
        {
            return await _context.ScanAsync<Voyage>(null, _operationConfig).GetRemainingAsync();            
        }

        public async Task SaveVoyageAsync(Voyage voyage, string country)
        {
            var voyageItem = await _context.LoadAsync<Voyage>(voyage.PartitionKey, voyage.SortKey, _operationConfig);

            if (voyageItem != null)
            {
                voyageItem.Coordinate.Add(voyage.Coordinate.First());
                voyageItem.Country.Add(country);                
            }
            else
            {
                voyageItem = voyage;
            }

            await _context.SaveAsync(voyageItem, _operationConfig);
        }
    }
}
