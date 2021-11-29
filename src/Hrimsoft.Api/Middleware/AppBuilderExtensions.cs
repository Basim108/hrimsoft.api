using System;
using Microsoft.AspNetCore.Builder;

namespace Hrimsoft.Api
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseRequestAutoLogger(this IApplicationBuilder builder)
            => builder.UseMiddleware<AutoLogMiddleware>();

        public static IApplicationBuilder UseRequestAutoLogger(this IApplicationBuilder builder, Action<AutoLogOptions> configure)
        {
            var options = new AutoLogOptions();
            configure?.Invoke(options);
            return builder.UseMiddleware<AutoLogMiddleware>(options);
        }
    }
}