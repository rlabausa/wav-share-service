using WavShareService.Middleware;

namespace WavShareService.Extensions
{
    /// <summary>
    /// Exception middleware extensions for the <see cref="IApplicationBuilder"/> class.
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Adds middleware for global error handling.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
