using Contracts.Items;
using Domain.Data;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Transactions;

namespace IntegrationTests
{
    public class UnitTest1
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UnitTest1(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

        }

        [Theory]
        [InlineData("api/items")]
        public async Task ItemsControllerDefaultCallWillListAllItems(string url)
        {
            var scope = _factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<DataContext>();

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await db.Database.EnsureCreatedAsync();

                var item = new Item()
                {
                    Name = "TEST",
                };

                db.Items.Add(item);
                await db.SaveChangesAsync();

                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<GetItemsQueryResponseDto>(content);

                Assert.NotNull(responseDto);
                Assert.Equal(item.ExternalId, responseDto.Results.First().ExternalId);
                Assert.Single(responseDto.Results);

                transaction.Dispose();
            }
        }

        [Theory]
        [InlineData("api/items")]
    }
}