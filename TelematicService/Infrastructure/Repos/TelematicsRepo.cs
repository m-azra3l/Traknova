using Serilog;
using System.Text.Json;
using TelematicService.Application.Dtos;
using TelematicService.Application.MapServices;
using TelematicService.Domain.Entities;
using TelematicService.Domain.Interfaces;
using TelematicService.Infrastructure.Contexts;

namespace TelematicService.Infrastructure.Repos
{
    // Repository implementation for handling Telematics data ingestion
    public class TelematicsRepo(AppDbContext dc) : ITelematicsRepo
    {
        // Injected DbContext instance for database operations
        private readonly AppDbContext dc = dc;

        // Ingest a single TelematicsDto record into the database
        public async Task IngestDataAsync(TelematicsDto telematicData)
        {
            // Always wrap persistence in try/catch and log errors for resilience
            try
            {
                // Map DTO to EF Core entity
                Telematics telematics = telematicData.ToEntity();

                // Add entity to DbContext and persist
                dc.Telematics.Add(telematics);
                await dc.SaveChangesAsync();

                // Raise domain events based on alerts
                if (telematicData.CrashDetection is not null)
                {
                    // In production: publish CrashDetectedEvent with unique event ID to a broker
                    string crashEvent = JsonSerializer.Serialize(telematicData.CrashDetection);
                    Log.Logger.Warning($"Vehicle {telematicData.VehicleId} crash detected.\nCrash Event as at  {telematicData.TimeStamp:g} :\n {crashEvent}");
                }

                if (telematicData.OverSpeedingAlert)
                {
                    // Publish OverSpeedingEvent with event ID
                    Log.Logger.Warning($"Vehicle {telematicData.VehicleId} overspeeding at {telematics.GpsSpeed} km/h.");
                }

                if (telematicData.TowingAlert)
                {
                    // Publish TowingEvent with event ID
                    Log.Logger.Warning($"Vehicle {telematicData.VehicleId} towing alert raised.");
                }

                if (telematicData.UnplugAlert)
                {
                    // Publish UnplugEvent with event ID
                    Log.Logger.Warning($"Vehicle {telematicData.VehicleId} unplug alert raised.");
                }

                if (telematicData.IdlingAlert)
                {
                    // Publish IdlingEvent with event ID
                    Log.Logger.Warning($"Vehicle {telematicData.VehicleId} idling alert raised.");
                }

                // Other alerts can be published similarly
            }
            catch (Exception ex)
            {
                // Log error with Serilog for observability
                Log.Logger.Error(ex, "Error ingesting telemetry data for Vehicle {VehicleId}", telematicData.VehicleId);
            }
        }

        // Ingest a batch of TelematicsDto records (bulk insert preferred for performance)
        public async Task IngestDataListAsync(List<TelematicsDto> telematicData)
        {
            // Collect events for later publishing
            List<string> crashEvents = [];
            List<string> overSpeedingEvents = [];

            // Prepare entity list for bulk insert
            List<Telematics> telematics = [];

            foreach (TelematicsDto data in telematicData)
            {
                Telematics entity = data.ToEntity();

                if (data.CrashDetection is not null)
                {
                    // Collect crash events for later publishing
                    string crashEvent = JsonSerializer.Serialize(data.CrashDetection);
                    crashEvents.Add($"Vehicle {data.VehicleId} crash detected.\nCrash Event as at {data.TimeStamp:g}:\n {crashEvent}");
                }

                if (data.OverSpeedingAlert)
                {
                    // Collect overspeeding events for later publishing
                    overSpeedingEvents.Add($"Vehicle {data.VehicleId} overspeeding at {data.GpsSpeed} km/h.");
                }

                // Add entity to batch list
                telematics.Add(entity);
            }

            // Bulk insert is faster than AddRangeAsync for large datasets
            await dc.Telematics.AddRangeAsync(telematics);
            await dc.SaveChangesAsync();

            // Publish collected events (in production: send to broker/queue)

            if (crashEvents is { Count: > 0 })
            {
                foreach (string crashEvent in crashEvents)
                {
                    Log.Logger.Warning(crashEvent);
                }
            }

            if (overSpeedingEvents is { Count: > 0 })
            {
                foreach (string speedEvent in overSpeedingEvents)
                {
                    Log.Logger.Warning(speedEvent);
                }
            }
        }
    }
}
