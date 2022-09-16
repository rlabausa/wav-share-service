using WavShareServiceBLL;
using WavShareServiceDAL;

namespace WavShareService.Extensions
{
    public static class ServiceExtensions
    {
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
