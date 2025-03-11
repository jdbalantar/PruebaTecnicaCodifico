using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class RequestFilter(ILogger<RequestFilter> logger) : Attribute, IActionFilter
    {
        private readonly ILogger<RequestFilter> logger = logger;

        /// <summary>
        /// Método que se ejecuta cuando se comienza una petición
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                logger.LogWarning("La solicitud contiene errores de validación");
            ValidarModelo(context);
        }

        /// <summary>
        /// Método que se ejecuta cuando finaliza la petición
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context) { }

        private static void ValidarModelo(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var errors = context.ModelState
                .Where(x => x.Value!.Errors.Any())
                .SelectMany(x => x.Value!.Errors.Select(y => new
                {
                    field = x.Key,
                    error = y.ErrorMessage
                }))
                .ToList();

            context.Result = new BadRequestObjectResult(new
            {
                isSuccess = false,
                operationHandled = true,
                modelErrors = true,
                message = "La solicitud contiene errores de validación.",
                statusCode = 400,
                errors
            });
        }
    }
}
