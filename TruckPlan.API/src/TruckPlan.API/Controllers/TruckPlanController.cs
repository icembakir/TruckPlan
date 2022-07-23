using Microsoft.AspNetCore.Mvc;
using TruckPlan.API.Dtos;
using TruckPlan.API.Services;

namespace TruckPlan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckPlanController : ControllerBase
    {
        private readonly ILogger<TruckPlanController> _logger;
        private readonly ITruckPlanService _truckPlanService;

        public TruckPlanController(ILogger<TruckPlanController> logger, ITruckPlanService truckPlanService)
        {
            _logger = logger;
            _truckPlanService = truckPlanService;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPut("SendCoordinate")]
        public async Task<IActionResult> PutGps([FromBody] VoyageRequestDto voyageRequestDto)
        {
            try
            {
                await _truckPlanService.SaveVoyageAsync(voyageRequestDto);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet("GetTotalDistanceByFilters")]
        public async Task<IActionResult> GetTotalDistanceOfVoyagesByFilters(int age, int month, int year, string country)
        {
            try
            {
                var totalDistance = await _truckPlanService.GetTotalDistanceOfVoyagesByFilters(age, month, year, country);
                
                return Ok(totalDistance);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
