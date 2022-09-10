using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WavShareServiceModels.Exceptions
{
    public class ServiceException : Exception
    {
        private const string _messagePrefix = "Service Exception:";
        public int StatusCode { get; }

        public ServiceException(int statusCode, string message) : base($"{_messagePrefix} {message}")
        {
            StatusCode = statusCode;
        }

        public ServiceException(HttpStatusCode statusCode, string message): base($"{_messagePrefix} {message}")
        {
            StatusCode = (int)statusCode;
        }
    }
}
