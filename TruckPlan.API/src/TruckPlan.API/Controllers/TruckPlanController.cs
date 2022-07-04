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
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [HttpPost("SendCoordinate")]
        public async Task<IActionResult> PostGps([FromBody] VoyageRequestDto voyageRequestDto)
        {
            try
            {
                if (await _truckPlanService.CheckIfVoyageExists(voyageRequestDto.VoyageId))
                {
                    await _truckPlanService.UpdateVoyageAsync(voyageRequestDto.VoyageId, voyageRequestDto.Coordinate);

                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    await _truckPlanService.CreateVoyageAsync(voyageRequestDto);

                    return StatusCode(StatusCodes.Status201Created);
                }
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
