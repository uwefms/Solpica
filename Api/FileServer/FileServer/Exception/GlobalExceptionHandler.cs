using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace FileServer.Exception
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
            System.Exception exception, CancellationToken cancellationToken)
        {
            if (exception is TimeoutException)
            {
                await MyException.ExceptionMessage(httpContext, exception,
              HttpStatusCode.RequestTimeout, "Es ist ein Timeout aufgetreten");

                return true;
            }

            if (exception is ArgumentException)
            {

                await MyException.ExceptionMessage(httpContext, exception,
                             HttpStatusCode.BadRequest, "Es ist ein Argument Fehler aufgetreten");
                return true;
            }
            else
            {
                await MyException.ExceptionMessage(httpContext, exception,
                    HttpStatusCode.InternalServerError, "Es ist ein unerwarteter Fehler aufgetreten");
                return true;
            }

            // return false;
        }

	} // end of class

} // end of namespace
