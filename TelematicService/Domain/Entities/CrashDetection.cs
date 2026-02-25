using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TelematicService.Domain.Entities
{
    // Entity representing crash detection events captured from telematics data
    public class CrashDetection
    {
        // Primary key for the CrashDetection entity
        // Uses Ulid (Universally Unique Lexicographically Sortable Identifier) for uniqueness and ordering
        [Key]
        public Ulid Id { get; set; } = Ulid.NewUlid();
        public float ImpactMagnitude { get; set; }
        public required string Axis { get; set; }
        [ForeignKey(nameof(Telematics))]
        public Ulid TelematicsId { get; set; }
        public required Telematics Telematics { get; set; }
    }
}
