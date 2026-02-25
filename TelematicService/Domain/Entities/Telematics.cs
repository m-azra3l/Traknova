using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TelematicService.Domain.Entities
{
    // Entity representing a single telemetry record captured from a vehicle
    [Index(nameof(TimeStamp))] // Non unique time based index on the timestamp
    public class Telematics
    {
        // Primary key for the Telematics entity.
        // ULID ensures globally unique, lexicographically sortable identifiers
        [Key]
        public Ulid Id { get; set; } = Ulid.NewUlid(); // Use Ulid to string if human readability is neccessary

        // Optional Trip identifier (currently stored as string)
        public string? TripId { get; set; } // Supposed foreign key of a trip in guid format, prefer ULID to string

        // Vehicle identifier (currently int)
        public required int VehicleId { get; set; } // Supposed foreign key of a vehicle, prefer ULID to string

        // Timestamp when telemetry was recorded
        public DateTime TimeStamp { get; set; }

        // GPS coordinates and positional data
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public int Altitude { get; set; }
        public int Angle { get; set; }
        public int Satellites { get; set; }

        // Speed and engine metrics
        public int GpsSpeed { get; set; }
        public int CanSpeed { get; set; }
        public int CanEngineRpm { get; set; }
        public int EngineTemperature { get; set; }

        // Mileage and fuel metrics
        public int CanTotalMileageMetres { get; set; }
        public int CanFuelLevelLitres { get; set; }
        public int CanFuelPercentage { get; set; }
        public int TripOdemeter { get; set; }
        public int TotalOdemeterMetres { get; set; }

        // Battery and electrical metrics
        public int BatteryLevel { get; set; }
        public float BatteryVoltage { get; set; }
        public float BatteryCurrent { get; set; }
        public int GsmSignal { get; set; }

        // Vehicle state flags
        public bool IgnitionActive { get; set; }
        public bool IsMoving { get; set; }

        // Digital outputs and indicators
        public bool DigitalOutput1 { get; set; }
        public bool DigitalOutput2 { get; set; }
        public bool DigitalOutput3 { get; set; }
        public bool OilIndicatorActive { get; set; }

        // Alerts and safety flags
        public bool TowingAlert { get; set; }
        public bool IdlingAlert { get; set; }
        public bool OverSpeedingAlert { get; set; }
        public bool UnplugAlert { get; set; }

        // Eco-driving metrics
        public int EcoScore { get; set; }
        public int DataMode { get; set; }
        public int NetworkType { get; set; }
        public int GreenDrivingType { get; set; }
        public int EcoDurationInMS { get; set; }

        // OBD (On-Board Diagnostics) metrics
        public int ObdEngineRpm { get; set; }
        public int OBDOEMTotalMileageKM { get; set; }
        public int ObdOemFuelLevelLitres { get; set; }
        public int ObdFuelLevelPercent { get; set; }

        // Navigation property for crash detection
        // Conceptually this is a one-to-one relationship: a telemetry record has zero or one crash detection
        // Modeled as a collection for more flexible handling in EF Core
        // This design choice allows you to avoid null navigation properties and simplifies data handling,
        // while still enforcing "zero or one" at the application level
        public ICollection<CrashDetection> CrashDetections { get; set; } = [];
    }
}
