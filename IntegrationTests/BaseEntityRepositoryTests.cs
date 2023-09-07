using Application.Repositories;
using Domain.Data;
using Domain.Entities;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests
{
    [TestFixture]
    public class MyEntityRepositoryTests
    {
        private DbContextOptions<DataContext> _dbContextOptions;
        private Item TestingEntity;
        private ItemCategory TestingCategory;
        private readonly Guid ItemGuid = new("8601b686-5a38-4e1a-9064-f1dfe0eca503");

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var dbContext = new DataContext(_dbContextOptions);
            dbContext.Database.EnsureCreated();

            TestingCategory = new ItemCategory()
            {
                Id = 0,
                Name = "test"
            };

            TestingEntity = new Item()
            {
                Name = "test",
                ExternalId = ItemGuid,
                CategoryId = TestingCategory.Id,
            };
        }

        [TearDown]
        public void TearDown()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task GetByExternalIdExistingEntityReturnsEntity()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            dbContext.Items.Add(TestingEntity);
            await dbContext.SaveChangesAsync();

            var result = await repository.GetByIdAsync<Item>(ItemGuid, new CancellationToken());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(TestingEntity.Id));
        }

        [Test]
        public async Task GetByExternalIdAsyncExistingEntityReturnsEntity()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            dbContext.Items.Add(TestingEntity);
            await dbContext.SaveChangesAsync();

            var result = await repository.GetByIdAsync<Item>(ItemGuid, new CancellationToken());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(TestingEntity.Id));
        }


        [Test]
        public async Task GetByIdAsyncExistingEntityReturnsEntity()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            dbContext.Items.Add(TestingEntity);
            await dbContext.SaveChangesAsync();

            var result = await repository.GetByIdAsync<Item>(TestingEntity.ExternalId, new CancellationToken());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(TestingEntity.Id));
        }

        [Test]
        public void GetByExternalIdNonExistingEntityReturnsNull()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            var result = repository.GetByIdAsync<Item>(ItemGuid, new CancellationToken());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task SetAsDeletedExistingEntitySetsIsDeletedToTrue()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            dbContext.Items.Add(TestingEntity);
            await dbContext.SaveChangesAsync();

            await repository.SetAsDeleted<Item>(TestingEntity.ExternalId);

            var result = await dbContext.Items.SingleOrDefaultAsync(e => e.Id == TestingEntity.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.IsDeleted, Is.True);
        }

        [Test]
        public void SetAsDeletedNonExistingEntityThrowsNotFoundException()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await repository.SetAsDeleted<Item>(ItemGuid);
            });
        }

        [Test]
        public async Task CallSaveOrUpdateAsyncMethodSavesEntityInDb()
        {
            using var dbContext = new DataContext(_dbContextOptions);
            var repository = new MyEntityRepository(dbContext);

            dbContext.Items.Add(TestingEntity);

            var resultBeforeSave = await dbContext.Items.SingleOrDefaultAsync(e => e.Id == TestingEntity.Id);

            await repository.SaveChangesAsync(new CancellationToken());

            var resultAfterSave = await dbContext.Items.SingleOrDefaultAsync(e => e.Id == TestingEntity.Id);

            Assert.That(resultAfterSave, Is.Not.Null);
            Assert.That(resultBeforeSave, Is.Null);
        }

        private class MyEntityRepository : BaseEntityRepository<Item>
        {
            public MyEntityRepository(DataContext dbContext) : base(dbContext)
            {
            }
        }
    }
}
