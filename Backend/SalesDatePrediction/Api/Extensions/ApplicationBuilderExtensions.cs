﻿namespace Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Juan Sales Date Prediction - Juan David Balanta");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
    }
}
