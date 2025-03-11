using System.Text.Json;

namespace Api.Middlewares
{
    public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase) &&
                    context.Request.Path.Value is not null && !context.Request.Path.Value.Contains("swagger", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Inicio de la petición: {Method} {Path}", context.Request.Method, context.Request.Path);
                }

                await _next(context);

                if (!context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase) &&
                    context.Request.Path.Value is not null && !context.Request.Path.Value.Contains("swagger", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Fin de la petición: {Method} {Path} con código de estado {StatusCode}", context.Request.Method, context.Request.Path, context.Response.StatusCode);
                }


            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlerMiddleware> logger)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            context.Response.ContentType = "application/json";

            try
            {
                var exceptionToHandle = ex.InnerException ?? ex; // Usa la excepción interna si existe
                int statusCode = StatusCodes.Status500InternalServerError; // Todas las excepciones se manejan como error 500
                string message = "Ha ocurrido un error en el servidor";
                var errorDetails = GetExceptionDetails(exceptionToHandle);

                await WriteResponse(statusCode, message, false, errorDetails, exceptionToHandle);
            }
            catch (Exception unexpectedEx)
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    isSuccess = false,
                    operationHandled = false,
                    modelErrors = false,
                    message = "No se pudo controlar la excepción",
                    statusCode = StatusCodes.Status500InternalServerError,
                    errors = GetExceptionDetails(unexpectedEx)
                }, jsonOptions));

                logger.LogError(unexpectedEx, "Ocurrió un error al intentar manejar la excepción.");
            }

            async Task WriteResponse(int statusCode, string message, bool modelErrors, object errors, Exception loggedException)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = statusCode;
                }

                var response = new
                {
                    isSuccess = false,
                    operationHandled = true,
                    modelErrors,
                    message,
                    statusCode,
                    errors
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
                LogException(loggedException);
            }

            void LogException(Exception loggedException)
            {
                logger.LogError(loggedException, "Ocurrió un error mientras se ejecutaba la petición: {Method} {Path}",
                    context.Request.Method, context.Request.Path);
            }

            static object GetExceptionDetails(Exception exception) => new
            {
                code = exception.Message,
                description = exception.StackTrace ?? null
            };
        }


    }
}
