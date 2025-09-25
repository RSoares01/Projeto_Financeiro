using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // se usar EF Core
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Projeto_Financeiro.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // continua pipeline
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                KeyNotFoundException => HttpStatusCode.NotFound,
                DbUpdateException => HttpStatusCode.Conflict,
                _ => HttpStatusCode.InternalServerError
            };

            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Error = exception.Message,
                Details = exception.InnerException?.Message // em produção, talvez você queira remover isso
            };

            response.StatusCode = errorResponse.StatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await response.WriteAsync(JsonSerializer.Serialize(errorResponse, options));
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Error { get; set; } = string.Empty;
        public string? Details { get; set; }
    }
}
