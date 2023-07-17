using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;
using TestProject.Dto.Core;

namespace TestProject.Core.Middlewares
{
    public sealed class MyCustomExceptionMiddleware : IMiddleware
    {
        readonly ILogger<MyCustomExceptionMiddleware> _logger;
        public MyCustomExceptionMiddleware(ILogger<MyCustomExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured on Request/Response -> {exStackTrace}", ex.StackTrace);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex switch
                {
                    ArgumentNullException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                var stackTrace = new StackTrace(ex);
                var result = JsonSerializer.Serialize<ExceptionModel>(new() { IsSuccess = false, Message = $"An error occured. ERROR: {ex.Message} - METHOD: {stackTrace.GetFrame(0)?.GetMethod()?.ReflectedType?.Name}.{stackTrace.GetFrame(0)?.GetMethod()?.Name}()" });
                await response.WriteAsync(result);
            }
        }
    }
}
