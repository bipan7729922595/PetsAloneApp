namespace PetAloneTest
{
    using Microsoft.EntityFrameworkCore;
    using PetsAlone.Core;
    using Xunit;

    public class PetsControllerTests
    {
        private DbContextOptions<PetsDbContext> _options;

        public PetsControllerTests()
        {
            var connectionString = "Filename=PetsAlone.db";

            _options = new DbContextOptionsBuilder<PetsDbContext>()
                .UseSqlite(connectionString)
                .Options;
        }

        [Fact]
        public void GetAll_ReturnsOrderedPets()
        {
            using (var context = new PetsDbContext(_options))
            {
                var petsService = new PetsService();

                var result = petsService.GetAll(context);

                Assert.Equal(3, result.Count);
                Assert.Equal(2025, result[0].MissingSince.Year);  // Newest pet first
            }
        }
    }
}
