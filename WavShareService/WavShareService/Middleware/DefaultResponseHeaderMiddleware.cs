using Microsoft.Extensions.Primitives;
using WavShareServiceModels.Constants;

namespace WavShareService.Middleware
{
    /// <summary>
    /// Global request handler for incoming HTTP requests.
    /// </summary>
    public class DefaultResponseHeaderMiddleware
    {
        private RequestDelegate _next;

        /// <summary>
        /// Constructs a <see cref="DefaultResponseHeaderMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next delegate/middleware in the pipeline.</param>
        public DefaultResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Add the default HTTP headers to the response.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
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
