using WavShareService.Middleware;

namespace WavShareService.Extensions
{
    public static class DefaultResponseHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseDefaultResponseHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DefaultResponseHeaderMiddleware>();
        }
    }
}
