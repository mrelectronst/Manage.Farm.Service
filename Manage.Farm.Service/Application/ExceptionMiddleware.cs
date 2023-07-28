using Manage.Farm.Service.Domain;
using System.Net;
using System.Text.Json;

namespace Manage.Farm.Service.API.Application;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var message = "server error";
            if (ex.GetType().BaseType == typeof(BaseException))
            {
                var customEx = (BaseException)ex;
                response.StatusCode = customEx.Code;
                message = customEx.Message;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var errorResponse = new
            {
                data = (object)null,
                success = false,
                message = message,
                statusCode = response.StatusCode
            };
            var errorJson = JsonSerializer.Serialize(errorResponse);
            _logger.LogError(ex.Message, ex);
            await response.WriteAsync(errorJson);
        }
    }
}
