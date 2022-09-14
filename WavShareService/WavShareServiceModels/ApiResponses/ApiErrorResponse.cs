using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.ApiResponses
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
            Status = StatusCodes.Status500InternalServerError;
            Message = "";
            TimeStamp = DateTime.UtcNow;
        }

        public ApiErrorResponse(int status, string message, DateTime timeStamp)
        {
            Status = status;
            Message = message;
            TimeStamp = timeStamp;
        }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("timeStamp")]
        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
}
}
