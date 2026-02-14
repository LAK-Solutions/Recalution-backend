using System.Net;

namespace Recalution.Application.Common.Exceptions;

public class AppException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}