/*using Application.Repositories;
using Domain.Data;
using Domain.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Interfaces;

namespace IntegrationTests
{
    [TestFixture]
    public class ItemRepositoryTests
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
        public async Task AddItemAsyncAddsAndSavesEntityInDb()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);
            var cancellationToken = new CancellationToken();

            var item1 = new Item()
            {
                Name = "TEST1",
                ExternalId = Guid.NewGuid(),
            };

            await repository.AddItemAsync(item1, cancellationToken);

            var result = await repository.GetByIdAsync<Item>(item1.ExternalId, cancellationToken);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("TEST1"));
            Assert.That(result.Id, Is.EqualTo(item1.Id));
        }

        [Test]
        public async Task GetAllAsyncExistingEntityReturnsListOfEntities()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);
            var cancellationToken = new CancellationToken();

            var item1 = new Item()
            {
                Name = "TEST1",
                ExternalId = Guid.NewGuid(),
            };

            var item2 = new Item()
            {
                Name = "TEST2",
                ExternalId = Guid.NewGuid()
            };

            await repository.AddItemAsync(item1, cancellationToken);
            await repository.AddItemAsync(item2, cancellationToken);

            var result = await repository.GetAllAsync(cancellationToken);

            Assert.That(result, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(result, Has.Count.EqualTo(2));

                result.ForEach(entity =>
                {
                    Assert.That(entity.Name, Is.Not.Null);
                    Assert.That(entity.Id == item1.Id || entity.Id == item2.Id);
                });
            });
        }

        private class MyEntityRepository : ItemsRepository
        {
            public MyEntityRepository(DataContext dbContext, IRecipeRepository recipeRepository) : base(dbContext)
            {
            }
        }
    }
}
*/