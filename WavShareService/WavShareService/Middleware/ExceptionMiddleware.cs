using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using WavShareServiceModels.ApiResponses;
using WavShareServiceModels.Constants;
using WavShareServiceModels.Exceptions;
using WavShareServiceModels.Logging;

namespace WavShareService.Middleware
{
    /// <summary>
    /// Global exception handler for WavShareService Web API.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Constructs an <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next delegate/middleware in the pipeline.</param>
        /// <param name="logger">Logger populated through dependency injection.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Log data for the current HTTP request.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var timeStamp = DateTime.UtcNow;
            var requestUrl = httpContext.Request.GetDisplayUrl();

            StringValues correlIdString;
            httpContext.Response.Headers.TryGetValue(Header.ClientCorrelId, out correlIdString);
            Guid correlId;
            Guid.TryParse(correlIdString.ToString(), out correlId);

            Exception? caughtException = null;

            var stopWatch = Stopwatch.StartNew();

            try
            {
                await _next(httpContext);
            }
            catch (ApiException exc)
            {
                caughtException = exc;
                await HandleErrorResponse(httpContext, exc);
            }
            catch (SqlException exc)
            {
                caughtException = exc;
                await HandleErrorResponse(httpContext, exc);
            }
            catch (Exception exc)
            {
                caughtException = exc;
                await HandleErrorResponse(httpContext, exc);
            }

            stopWatch.Stop();

            LogRecord record = new LogRecord()
            {
                StartTime = timeStamp,
                ElapsedMilliseconds = stopWatch.ElapsedMilliseconds,
            };

            if (caughtException != null)
            {
                record.CorrelationId = correlId;
                record.TraceLevel = TraceLevel.Error;
                record.Message = caughtException.StackTrace;
                record.ExceptionType = caughtException.GetType().FullName;

                _logger.LogError(record.ToString());
            }
            else if (httpContext.Response.StatusCode >= StatusCodes.Status400BadRequest) // for other errors not caught as exceptions (e.g., model validation)
            {
                record.CorrelationId = correlId;
                record.TraceLevel = TraceLevel.Error;
                record.Message = $"{httpContext.Response.StatusCode} - {httpContext.Response.Body}";
                _logger.LogError(record.ToString());
            }
            else
            {
                record.CorrelationId = correlId;
                record.TraceLevel = TraceLevel.Info;
                _logger.LogInformation(record.ToString());
            }
        }

        /// <summary>
        /// Write custom <see cref="ApiErrorResponse"/> to the response body.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exc"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private Task HandleErrorResponse(HttpContext context, ApiException exc, DateTime? dateTime = null)
        {
            context.Response.StatusCode = exc.StatusCode;

            var timeStamp = dateTime ?? DateTime.UtcNow;
            var errorResponse = new ApiErrorResponse()
            {
                Status = exc.StatusCode,
                Message = exc.Message,
                Details = $"{exc.Message} {exc.StackTrace}",
                TimeStamp = timeStamp

            }.ToString();

            return context.Response.WriteAsync(errorResponse, Encoding.UTF8);
        }

        /// <summary>
        /// Write custom <see cref="ApiErrorResponse"/> to the response body.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exc"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private Task HandleErrorResponse(HttpContext context, SqlException exc, DateTime? dateTime = null)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var timeStamp = dateTime ?? DateTime.UtcNow;
            var errorResponse = new ApiErrorResponse()
            {
                Status = context.Response.StatusCode,
                Message = ApiErrorResponse.GenericErrorMessage,
                Details = $"ErrorCode: {exc.ErrorCode} - {exc.Message}",
                TimeStamp = timeStamp
            }.ToString();

            return context.Response.WriteAsync(errorResponse);
        }

        /// <summary>
        /// Write custom <see cref="ApiErrorResponse"/> to the response body.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exc"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private Task HandleErrorResponse(HttpContext context, Exception exc, DateTime? dateTime = null)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var timeStamp = dateTime ?? DateTime.UtcNow;
            var errorResponse = new ApiErrorResponse()
            {
                Status = context.Response.StatusCode,
                Message = ApiErrorResponse.GenericErrorMessage,
                Details = exc.Message,
                TimeStamp = timeStamp
            }.ToString();


            return context.Response.WriteAsync(errorResponse);
        }


    }
}
