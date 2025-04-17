using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileServer.Exception
{
    public class MyException
    {
        public static async Task ExceptionMessage(HttpContext httpContext,
            System.Exception exception, HttpStatusCode httpStatusCode, 
            string Tittle)
        {
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)httpStatusCode,
                Type = exception.GetType().Name,
                Title = Tittle,
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            });
        }

	} // end of class

} // end of namespace
