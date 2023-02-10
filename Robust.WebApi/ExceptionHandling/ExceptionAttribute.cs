using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Net;


namespace Robust.WebApi.ExceptionHandling
{
    public class MyExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //Debug.WriteLine(context.Exception);

            var result = new ObjectResult(new
            {
                context.Exception.Message, // Or a different generic message
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Log the exception
            //_logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);

            // Set the result
            context.Result = result;
        }
    }
}
