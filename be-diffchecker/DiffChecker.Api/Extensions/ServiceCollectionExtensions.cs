using Database.Logic;
using DiffChecker.Api.Mappers;
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
            ArgumentNullException.ThrowIfNull(services);

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

            services.AddSingleton<ApiMapper>();

            return services;
        }
    }
}
