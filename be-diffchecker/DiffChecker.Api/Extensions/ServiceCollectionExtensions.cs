using Database.Logic;
using Database.Logic.Profiles;
using DiffChecker.Api.Profiles;
using DiffChecker.Contracts.Services.V1;
using DiffChecker.Logic.Services.V1;
using System.Reflection;

namespace DiffChecker.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures default behavior and IoC.
        /// </summary>
        public static IServiceCollection AddProgramServices(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddControllers();
            services.AddApiVersioning();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(x =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.ConfigureDatabaseContext();

            services.AddScoped<IDiffService, DiffService>();

            services.AddAutoMapper(
                typeof(DatabaseProfile),
                typeof(ApiProfile));

            return services;
        }
    }
}
