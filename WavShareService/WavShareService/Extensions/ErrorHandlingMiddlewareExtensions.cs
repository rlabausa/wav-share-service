using WavShareService.Middleware;

namespace WavShareService.Extensions
{
    /// <summary>
    /// Error-handling middleware extensions for the <see cref="IApplicationBuilder"/> class.
    /// </summary>
    public static class ErrorHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Adds middleware for global error handling.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
