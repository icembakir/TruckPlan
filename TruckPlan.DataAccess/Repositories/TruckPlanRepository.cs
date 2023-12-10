using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using TruckPlan.DataAccess.Models;

namespace TruckPlan.DataAccess.Repositories
{
    public class TruckPlanRepository : ITruckPlanRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly DynamoDBOperationConfig _operationConfig;
        private readonly ILogger<TruckPlanRepository> _logger;

        public TruckPlanRepository(IDynamoDBContext context, ILogger<TruckPlanRepository> logger)
        {
            _context = context;
            _operationConfig = new DynamoDBOperationConfig
            {
                OverrideTableName = "voyages-test"
            };
            _logger = logger;
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

        public async Task SaveVoyageAsync(Voyage voyage)
        {
            try
            {
                var voyageItem = await _context.LoadAsync<Voyage>(voyage.PartitionKey, voyage.SortKey, _operationConfig);
                _logger.LogInformation($"Voyage item: {voyageItem}");

                if (voyageItem != null)
                {
                    voyageItem.Coordinate.Add(voyage.Coordinate.First());
                    voyageItem.Location.Add(voyage.Location.First());
                    await _context.SaveAsync(voyageItem, _operationConfig);
                }
                else
                {
                    voyageItem = voyage;
                }

                await _context.SaveAsync(voyageItem, _operationConfig);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while saving voyage: {ex.Message} {ex}");
            }
            
        }
    }
}
