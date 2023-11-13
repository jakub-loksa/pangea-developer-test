using Database.Logic;
using Database.Logic.Entities.Diff;
using Microsoft.Extensions.DependencyInjection;

namespace DiffChecker.Api.Tests.Helpers
{
    /// <summary>
    /// Base class for all integration tests.
    /// </summary>
    public class ApiTestBase : IClassFixture<ApiTestApplicationFactory>
    {
        protected readonly ApiTestApplicationFactory _factory;
        protected TestHttpClient _client = null!;

        protected ApiTestBase(ApiTestApplicationFactory factory)
        {
            _factory = factory;
        }

        internal DatabaseContext GetContext()
        {
            var scope = _factory.Services.CreateScope();

            return scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        }

        internal void SetupDiffs()
        {
            using var context = GetContext();

            context.Diffs.Add(new DiffEntity
            {
                Id = "SAME1",
                LeftDiff = "ABC123",
                RightDiff = "ABC123"
            });
            context.Diffs.Add(new DiffEntity
            {
                Id = "SHORT1",
                LeftDiff = "ABC1",
                RightDiff = "ABC123"
            });
            context.Diffs.Add(new DiffEntity
            {
                Id = "SAMESIZE1",
                LeftDiff = "ABC111A",
                RightDiff = "ABC999A"
            });
            context.Diffs.Add(new DiffEntity
            {
                Id = "LEFT_MISSING1",
                LeftDiff = null,
                RightDiff = "ABC999"
            });
            context.Diffs.Add(new DiffEntity
            {
                Id = "RIGHT_MISSING1",
                LeftDiff = "ABC111",
                RightDiff = null
            });

            context.SaveChanges();
        }
    }
}
