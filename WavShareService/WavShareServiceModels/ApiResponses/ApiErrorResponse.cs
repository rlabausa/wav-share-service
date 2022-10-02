using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WavShareServiceModels.Constants;

namespace WavShareServiceModels.ApiResponses
{
    public class ApiErrorResponse
    {
        public static readonly string GenericErrorMessage = "One or more errors occurred during the request.";
        public static readonly string GenericValidationErrorMessage = "One or more validation errors occurred.";

        public ApiErrorResponse(int status, string message)
        {
            Status = status;
            Message = message;
            Details = null;
            TimeStamp = DateTime.UtcNow;
        }

        public ApiErrorResponse(int? status = null, string? message = null, object? details = null, DateTime? timeStamp = null)
        {
            Status = status ?? StatusCodes.Status500InternalServerError;
            Message = message ?? GenericErrorMessage;
            Details = details;
            TimeStamp = timeStamp ?? DateTime.UtcNow;
        }

        public ApiErrorResponse(ModelStateDictionary modelState)
        {
            Status = StatusCodes.Status400BadRequest;
            Message = GenericValidationErrorMessage;

            var errors = new Dictionary<string, IEnumerable>();

            foreach(string key in modelState.Keys)
            {
                var errorMessages = new List<string>();
                var modelStateEntry = modelState[key];
                
                foreach (var val in modelStateEntry.Errors)
                {
                    errorMessages.Add(val.ErrorMessage);
                }

                if (errorMessages.Count > 0)
                {
                    errors.Add(key, errorMessages);
                }

            }

            Details = errors;
            TimeStamp = DateTime.UtcNow;
        }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("details")]
        public object? Details { get; set; }

        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
}
}
