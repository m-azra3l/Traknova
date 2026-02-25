using Microsoft.AspNetCore.Mvc;
using Serilog;
using TelematicService.Application.Dtos;
using TelematicService.Domain.Interfaces;

namespace TelematicService.Controllers
{
    // API Controller for handling Telematics ingestion requests
    [Route("api/[controller]")]
    [ApiController]
    public class TelematicsController(ITelematicsRepo telematicsRepo) : ControllerBase
    {
        // Repository instance injected via constructor
        private readonly ITelematicsRepo telematicsRepo = telematicsRepo;

        /// <summary>
        /// Endpoint to ingest a single telematics data payload
        /// Accepts a TelematicsDto in the request body and persists it using the repository
        /// </summary>
        /// <param name="telematicsData">Telemetry data payload mapped from JSON</param>
        /// <returns>HTTP 200 OK if successful, otherwise HTTP 500 with error message</returns>
        [HttpPost("ingest")]
        public async Task<IActionResult> IngestData([FromBody] TelematicsDto telematicsData)
        {
            try
            {
                // Call repository method to persist telemetry data
                await telematicsRepo.IngestDataAsync(telematicsData);

                // Return success response (prefer standardized result patterns in production)
                return Ok();
            }
            catch (Exception ex)
            {
                // Log error with unique error ID for traceability
                Log.Logger.Error(exception: ex, "An error occurred while processing request.");

                // Return generic error response (prefer standardized error contracts in production)
                return StatusCode(500, "Unexpected error occurred.");
            }
        }

        /// <summary>
        /// Endpoint to ingest a list of telematics data payload
        /// Accepts a list TelematicsDto in the request body and persists it using the repository
        /// </summary>
        /// <param name="telematicsData"></param>
        /// <returns>HTTP 200 OK if successful, otherwise HTTP 500 with error message</returns>
        [HttpPost("ingest/list")]
        public async Task<IActionResult> IngestListData([FromBody] List<TelematicsDto> telematicsData)
        {
            try
            {
                // Call repository method to persist telemetry data
                await telematicsRepo.IngestDataListAsync(telematicsData);

                // Return success response (prefer standardized result patterns in production)
                return Ok();
            }
            catch (Exception ex)
            {
                // Log error with unique error ID for traceability
                Log.Logger.Error(exception: ex, "An error occurred while processing request.");

                // Return generic error response (prefer standardized error contracts in production)
                return StatusCode(500, "Unexpected error occurred.");
            }
        }
    }
}
