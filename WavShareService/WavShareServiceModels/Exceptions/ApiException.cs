using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WavShareServiceModels.Exceptions
{
    public class ApiException : Exception
    {
        private static readonly string _messagePrefix = "Service Exception:";
        public int StatusCode { get; }

        public ApiException(int statusCode, string message) : base($"{_messagePrefix} {message}")
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message): base($"{_messagePrefix} {message}")
        {
            StatusCode = (int)statusCode;
        }
    }
}
