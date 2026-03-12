using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookingWeb.API.BLL.Helpers
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public CustomException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public CustomException(string message) : this(HttpStatusCode.InternalServerError, message)
        {
        }

        public CustomException(string message, Exception innerException) : this(HttpStatusCode.InternalServerError, message, innerException)
        {
        }
    }
}
