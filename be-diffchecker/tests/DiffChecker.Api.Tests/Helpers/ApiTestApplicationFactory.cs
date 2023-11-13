using Database.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace DiffChecker.Api.Tests.Helpers
{
    public class ApiTestApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<DatabaseContext>((container, options) =>
                {
                    // For local testing purposes we want to use the InMemory database.
                    // In case more complex features are required, e.g. transactions, a SQLite instance can be used instead.
                    options.UseInMemoryDatabase(nameof(DatabaseContext));
                    options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });
            });
        }

        internal void SetUpTest()
        {
            using var scope = Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            context.Database.EnsureDeleted();
            context.SaveChanges();
            context.Database.EnsureCreated();
        }

        internal TestHttpClient CreateUnauthorizedClient()
        {
            var client = CreateClient();

            return new TestHttpClient(client);
        }
    }
}
