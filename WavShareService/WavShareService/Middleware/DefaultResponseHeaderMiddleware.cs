using WavShareServiceModels.Constants;

namespace WavShareService.Middleware
{
    public class DefaultResponseHeaderMiddleware
    {
        private RequestDelegate _next;
        public DefaultResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            httpContext.Response.ContentType = Mime.MediaTypes.Json;

            await _next(httpContext);
        }
    }
}
