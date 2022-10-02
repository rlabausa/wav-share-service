using WavShareService.Middleware;

namespace WavShareService.Extensions
{
    /// <summary>
    /// Default response header middleware extensions for the <see cref="IApplicationBuilder"/> class.
    /// </summary>
    public static class DefaultResponseHeaderMiddlewareExtensions
    {
        
        /// <summary>
        /// Adds middleware to append default response header data to incoming HTTP requests.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultResponseHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DefaultResponseHeaderMiddleware>();
        }
    }
}
