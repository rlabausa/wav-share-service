using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using WavShareService.Health;
using WavShareServiceBLL;
using WavShareServiceDAL;
using WavShareServiceModels.ApiResponses;

namespace WavShareService.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> class.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add required controller configurations for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddConfiguredControllers(this IServiceCollection services)
        {
            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var modelState = actionContext.ModelState;
                        return new ApiValidationErrorResult(modelState);
                    };
                });

            return services;
        }

        /// <summary>
        /// Add required logging configurations for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns><see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddConfiguredLogging(this IServiceCollection services, IConfiguration? configuration = null)
        {
            services.AddLogging((logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddDebug();

                if (configuration != null)
                {
                    logging.AddConfiguration(configuration);
                }
            });

            return services;
        }

        /// <summary>
        /// Add required routing configurations for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns><see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddConfiguredRouting(this IServiceCollection services)
        {
            services.AddRouting(opts =>
            {
                opts.LowercaseUrls = true;
            });

            return services;
        }

        /// <summary>
        /// Add required health check configurations for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns><see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddConfiguredHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddCheck(
                        "WavShareDB",
                        new WavShareDbHealthCheck(configuration.GetConnectionString("WavShareDB")),
                        HealthStatus.Unhealthy,
                        new string[] { "db", "sqlserver" }
                    );

            return services;
        }


        /// <summary>
        /// Add all required transient services for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddTransientServices(this IServiceCollection services)
        {
            services.AddTransient<IAudioFileBLL, AudioFileBLL>();
            services.AddTransient<IAudioFileAdapter, AudioFileAdapter>();

            return services;
        }

        /// <summary>
        /// Add all required singleton services for the application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {

            //TODO: Implement Singleton Services 
            throw new NotImplementedException();
        }

    }
}
