using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Domain.Data;
using Npgsql;
using Microsoft.AspNetCore.Builder;
using Infrastructure.GlobalExceptionHandler;

namespace IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<DataContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddTransient<GlobalExceptionHandler>();

                services.AddSingleton<DbConnection>(container =>
                {

                    var connection = new NpgsqlConnection("Host=localhost; Database=Tests; Username=Admin; Password=Admin1234; Include Error Detail =true");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<DataContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseNpgsql(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }

    public static class DBUtilities
    {
        public static void ReinitializeForTest(DataContext db)
        {
            db.Items.RemoveRange(db.Items);
            db.Items.RemoveRange(db.Items);
            db.SaveChanges();
        }
    }
}