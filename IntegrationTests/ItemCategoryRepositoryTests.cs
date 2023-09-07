using Application.Repositories;
using Domain.Data;
using Domain.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests
{
    [TestFixture]
    public class ItemCategoryRepositoryTests
    {
        private DbContextOptions<DataContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var dbContext = new DataContext(_dbContextOptions);
            dbContext.Database.EnsureCreated();
        }

        [TearDown]
        public void TearDown()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetBylIdAsyncExistingEntityReturnsEntity()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            var newCategory = new ItemCategory()
            {
                Id = 0,
                Name = "TEST"
            };

            dbContext.ItemCategory.Add(newCategory);
            await dbContext.SaveChangesAsync();

            var result = await repository.GetByIdAsync(newCategory.Id, new CancellationToken());

            Assert.That(result, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(newCategory.Id));
                Assert.That(result.Name, Is.EqualTo(newCategory.Name));
            });
        }

        private class MyEntityRepository : ItemCategoryRepository
        {
            public MyEntityRepository(DataContext dbContext) : base(dbContext)
            {
            }
        }
    }
}
