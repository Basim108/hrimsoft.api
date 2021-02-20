using System;
using System.Net.Http;
using System.Text.Json;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hrimsoft.Api
{
    public static class ConfigureHellangProblemDetails
    {
        /// <summary>
        /// Adds Hellang ProblemDetails Middlware with Hrimsoft Configuration
        /// </summary>
        public static IServiceCollection AddHrimsoftProblemDetails(this IServiceCollection services, IHostEnvironment env)
        {
            services.AddProblemDetails(options =>
            {
                // Only include exception details in a development or staging environment.
                options.IncludeExceptionDetails = (ctx, ex) => !env.IsProduction();
                options.OnBeforeWriteDetails = (ctx, problem) => ctx.RequestServices
                                                                    .GetService<ILogger<ProblemDetailsMiddleware>>()
                                                                    ?.LogError(JsonSerializer.Serialize(problem));
                options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
            return services;
        }
    }
}