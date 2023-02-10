using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using ILogger = Robust.LoggerService.ILogger;

namespace Robust.WebApi.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        private readonly ILogger logger;
        public ExceptionHandlingMiddleware
        (RequestDelegate requestDelegate, ILogger _logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = _logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private Task HandleException(HttpContext context, Exception ex)
        {
            //logger.LogError(ex.ToString());
            logger.LogError("Error Occured", ex);
            var errorMessageObject = new Error { Message = ex.Message, Code = "GE" };
            var statusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case InvalidMovieException:
                    errorMessageObject.Code = "M001";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    errorMessageObject.Code = "M000";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

            }
            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
