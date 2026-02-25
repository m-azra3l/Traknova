using System.Text.Json;
using TelematicService.Application.Dtos;

namespace TelematicService.Application.MapServices
{
    public static class PayloadDeserializer
    {
        // Utility class for handling JSON deserialization of telemetry payloads
        // Provides a static method to convert raw JSON into strongly typed DTOs
        public static class TelematicsDeserializer
        {
            // Deserialize a JSON string into a list of TelematicsDto objects
            public static List<TelematicsDto> Deserialize(string jsonPayload)
            {
                // Configure JSON serializer options.
                // PropertyNameCaseInsensitive ensures that JSON property names
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<TelematicsDto>>(jsonPayload, options) ?? [];
            }
        }
    }
}
