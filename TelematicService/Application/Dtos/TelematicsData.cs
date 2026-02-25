namespace TelematicService.Application.Dtos
{
    // Data Transfer Object (DTO) representing telemetry data received from vehicles
    public record TelematicsDto
    {
        // Vehicle identifier (required)
        // In production, prefer ULID or GUID for stronger referential integrity
        public required int VehicleId { get; set; }

        // Optional Trip identifier
        // Represents the trip session this telemetry belongs to
        public string? TripId { get; set; }

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

        // Optional crash detection data
        // Represents zero or one crash event associated with this telemetry record
        public CrashDetectionDto? CrashDetection { get; set; }
    }

    // DTO representing crash detection data
    public record CrashDetectionDto
    {
        // Magnitude of the crash impact 
        public float ImpactMagnitude { get; set; }

        // Indicates the direction of force applied during the crash
        public required string Axis { get; set; }
    }
}
