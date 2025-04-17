using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileServer.Exception
{
    public class TimeOutException : IExceptionHandler
    {
        private readonly ILogger<TimeOutException> _logger;

        public TimeOutException(ILogger<TimeOutException> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            System.Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Es ist ein Timeout aufgetreten");

            if (exception is TimeoutException)
            {
                await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = (int)HttpStatusCode.RequestTimeout,
                    Type = exception.GetType().Name,
                    Title = "Es ist ein Timeout aufgetreten",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                });

                return true;
            }

            return false;

		} // end of method

	} // end of class

} // end of namespace
