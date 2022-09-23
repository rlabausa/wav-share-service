using Microsoft.Extensions.Primitives;
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

            StringValues guidOption;
            httpContext.Request.Headers.TryGetValue(Header.ClientCorrelId, out guidOption);

            string guid = guidOption == ClientCorrelIdHeaderValue.Generate ? Guid.NewGuid().ToString() : guidOption;

            httpContext.Response.Headers.Add(Header.ClientCorrelId, guid);

            await _next(httpContext);
        }
    }
}
