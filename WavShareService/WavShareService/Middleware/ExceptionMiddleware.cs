using Microsoft.AspNetCore.Http.Extensions;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using WavShareServiceModels.ApiResponses;
using WavShareServiceModels.Exceptions;
using WavShareServiceModels.Logging;

namespace WavShareService.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var timeStamp = DateTime.UtcNow;
            var requestUrl = httpContext.Request.GetDisplayUrl();

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
                record.TraceLevel = TraceLevel.Error;
                record.Message = caughtException.StackTrace;
                record.ExceptionType = caughtException.GetType().FullName;

                _logger.LogError(record.ToString());
            }
            else if (httpContext.Response.StatusCode >= StatusCodes.Status400BadRequest)
            {
                record.TraceLevel = TraceLevel.Error;

                //TODO: Fix request body logging
                record.Message = $"{httpContext.Response.StatusCode} - {httpContext.Response.Body}";
                _logger.LogError(record.ToString());
            }
            else
            {
                record.TraceLevel = TraceLevel.Info;
                _logger.LogInformation(record.ToString());
            }
        }

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
