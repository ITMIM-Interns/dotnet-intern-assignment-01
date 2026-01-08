using System.Net;

namespace MiniApi.CustomMiddleware
{
    internal sealed class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandleAsync(ex, context);

            }
        }
        private async Task ExceptionHandleAsync(Exception exception, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "";
            switch (exception)
            {
                case NullReferenceException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    break;
                case ArgumentNullException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    break;
            }
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(new {message,statusCode});
        }

    }

 
}
