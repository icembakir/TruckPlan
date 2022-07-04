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

        public async Task UpdateVoyageAsync(string voyageId, Coordinate cordinate, string country)
        {
            var voyage = await _context.LoadAsync<Voyage>($"VOYAGE#{voyageId}", _operationConfig);

            voyage.Coordinate.Add(cordinate);
            voyage.Country.Add(country);

            await _context.SaveAsync(voyage, _operationConfig);
        }

        public async Task<bool> CheckIfVoyageExists(string voyageId)
        {
            var voyage = await _context.LoadAsync<Voyage>($"VOYAGE#{voyageId}", voyageId, _operationConfig);
            return voyage != null;
        }

        public async Task CreateVoyageAsync(Voyage voyage)
        {
            await _context.SaveAsync<Voyage>(voyage, _operationConfig);
        }

        public async Task<List<Voyage>> GetAllVoyagesAsync()
        {
            return await _context.ScanAsync<Voyage>(null, _operationConfig).GetRemainingAsync();            
        }

    }
}
