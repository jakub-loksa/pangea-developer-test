using Database.Contracts.Stores;
using Database.Logic.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Logic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabaseContext(
            this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // TODO: When moving to PROD, it is essential to replace this with a regular
            // database provider, e.g. Microsoft.EntityFrameworkCore.SqlServer,
            // configure the connection string and create and apply migrations.
            services.AddDbContext<DatabaseContext>(config =>
                 config.UseInMemoryDatabase(nameof(DatabaseContext)));

            services.AddScoped<IDiffStore, DiffStore>();

            return services;
        }
    }
}
