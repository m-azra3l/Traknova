using Microsoft.EntityFrameworkCore;
using TelematicService.Application.Dtos;
using TelematicService.Infrastructure.Contexts;
using TelematicService.Infrastructure.Repos;

namespace TelematicsTest
{
    // Test suite for repo implementations
    public class TelematicsRepoTest
    {
        // Creates a new in‑memory AppDbContext for testing
        private static AppDbContext GetInMemoryDbContext()
        {
            // Configure EF Core to use an in‑memory database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Ulid.NewUlid().ToString())
                .Options;

            // Return a new context instance with these options.
            return new AppDbContext(options);
        }

        /// <summary>
        /// Test to persist single record with crash detection
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Persist single record with crash detection")]
        public async Task IngestDataAsync_PersistsSingleRecordWithCrashDetection()
        {
            // Arrange: set up in-memory DB and repo
            var dbContext = GetInMemoryDbContext();
            var repo = new TelematicsRepo(dbContext);

            // Create sample DTO with crash detection
            var dto = new TelematicsDto
            {
                VehicleId = 123,
                TripId = Guid.NewGuid().ToString(),
                TimeStamp = DateTime.UtcNow,
                Longitude = 5.6f,
                Latitude = 7.8f,
                GpsSpeed = 80,
                CrashDetection = new CrashDetectionDto
                {
                    Axis = "X",
                    ImpactMagnitude = 9.5f
                }
            };

            // Act: ingest single DTO
            await repo.IngestDataAsync(dto);

            // Assert: verify record and crash detection persisted
            var saved = await dbContext.Telematics.Include(t => t.CrashDetections).FirstOrDefaultAsync();
            Assert.NotNull(saved);
            Assert.Equal(123, saved.VehicleId);
            Assert.Single(saved.CrashDetections);
            Assert.Equal("X", saved.CrashDetections.First().Axis);
        }

        /// <summary>
        /// Test to persist multiple records from provided payload file
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Persist multiple records from payload file")]
        public async Task IngestDataListAsync_PersistsMultipleRecordsFromPayloadFile()
        {

        }
    }
}
