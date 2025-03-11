using Api.Extensions;
using Api.Filters;
using Api.Middlewares;
using Application.Extensions;
using Infrastructure.Extensions;

namespace Api
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers(options => options.Filters.Add(typeof(RequestFilter)));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplicationLayer();
            builder.Services.AddContextInfrastructure(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddEssentials();
            var app = builder.Build();
            app.ConfigureSwagger();
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseApiVersioning();
            app.MapControllers();
            await app.RunAsync();
        }
    }
}