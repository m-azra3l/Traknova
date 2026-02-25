using TelematicService.Application.Dtos;

namespace TelematicService.Domain.Interfaces
{
    // Contract interface for the Telematics repository
    // Defines the operations required to ingest telemetry data into the system
    public interface ITelematicsRepo
    {
        // Ingest a single telemetry record into the database
        // Accepts a TelematicsDto (mapped from JSON payload) and persists it
        // Implementation should handle validation, mapping, persistence, and event publishing
        Task IngestDataAsync(TelematicsDto telematicData);

        // Ingest a batch of telemetry records into the database
        // Accepts a list of TelematicsDto objects (after JSON deserialization)
        // Implementation should prefer bulk insert for performance and publish events for alerts
        Task IngestDataListAsync(List<TelematicsDto> telematicData);
    }
}
