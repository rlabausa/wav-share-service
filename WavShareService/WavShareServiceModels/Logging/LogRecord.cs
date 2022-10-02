using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavShareServiceModels.Logging
{
    public class LogRecord
    {
        [JsonPropertyName("correlationId")]
        public Guid CorrelationId { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonPropertyName("elapsedMilliSeconds")]
        public long? ElapsedMilliseconds { get; set; }

        [JsonPropertyName("traceLevel")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TraceLevel? TraceLevel { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("exceptionType")]
        public string? ExceptionType { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
