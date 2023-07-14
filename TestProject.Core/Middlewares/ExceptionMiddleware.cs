using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;
using TestProject.Dto.Core;

namespace TestProject.Core.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex switch
                {
                    ArgumentNullException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                var result = JsonSerializer.Serialize<ExceptionModel>(new() { IsSuccess = false, Message = $"An error occured. ERROR: {ex.Message}, METHOD: {new StackTrace(ex).GetFrame(0).GetMethod().Name}" });
                await response.WriteAsync(result);
            }
        }
    }
}
