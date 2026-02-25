using TelematicService.Application.Dtos;
using TelematicService.Domain.Entities;

namespace TelematicService.Application.MapServices
{
    // Static class containing extension methods for mapping DTOs to domain entities
    public static class TelematicsRequest
    {
        // Maps a TelematicsDto(incoming payload) to a Telematics entity(EF Core model)
        public static Telematics ToEntity(this TelematicsDto dto)
        {
            // Initialize a new Telematics entity with values from the DTO
            var telematics = new Telematics
            {
                VehicleId = dto.VehicleId,
                TripId = dto.TripId,
                TimeStamp = dto.TimeStamp,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                Altitude = dto.Altitude,
                Angle = dto.Angle,
                Satellites = dto.Satellites,
                GpsSpeed = dto.GpsSpeed,
                CanSpeed = dto.CanSpeed,
                CanEngineRpm = dto.CanEngineRpm,
                EngineTemperature = dto.EngineTemperature,
                CanTotalMileageMetres = dto.CanTotalMileageMetres,
                CanFuelLevelLitres = dto.CanFuelLevelLitres,
                CanFuelPercentage = dto.CanFuelPercentage,
                TripOdemeter = dto.TripOdemeter,
                TotalOdemeterMetres = dto.TotalOdemeterMetres,
                BatteryLevel = dto.BatteryLevel,
                BatteryVoltage = dto.BatteryVoltage,
                BatteryCurrent = dto.BatteryCurrent,
                GsmSignal = dto.GsmSignal,
                IgnitionActive = dto.IgnitionActive,
                IsMoving = dto.IsMoving,
                DigitalOutput1 = dto.DigitalOutput1,
                DigitalOutput2 = dto.DigitalOutput2,
                DigitalOutput3 = dto.DigitalOutput3,
                OilIndicatorActive = dto.OilIndicatorActive,
                TowingAlert = dto.TowingAlert,
                IdlingAlert = dto.IdlingAlert,
                OverSpeedingAlert = dto.OverSpeedingAlert,
                UnplugAlert = dto.UnplugAlert,
                EcoScore = dto.EcoScore,
                DataMode = dto.DataMode,
                NetworkType = dto.NetworkType,
                GreenDrivingType = dto.GreenDrivingType,
                EcoDurationInMS = dto.EcoDurationInMS,
                ObdEngineRpm = dto.ObdEngineRpm,
                OBDOEMTotalMileageKM = dto.OBDOEMTotalMileageKM,
                ObdOemFuelLevelLitres = dto.ObdOemFuelLevelLitres,
                ObdFuelLevelPercent = dto.ObdFuelLevelPercent
            };

            // Handle optional crash detection data
            // If present, create a CrashDetection entity and associate it with the Telematics record
            if (dto.CrashDetection is not null)
            {
                telematics.CrashDetections.Add(new CrashDetection
                {
                    Axis = dto.CrashDetection.Axis,
                    ImpactMagnitude = dto.CrashDetection.ImpactMagnitude,
                    Telematics = telematics
                });
            }

            // Return the fully mapped entity
            return telematics;
        }
    }
}
