using Microsoft.EntityFrameworkCore;
using TelematicService.Infrastructure.Contexts;

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
