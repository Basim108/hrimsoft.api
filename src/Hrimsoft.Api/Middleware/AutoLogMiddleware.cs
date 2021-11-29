using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hrimsoft.Api
{
    [ExcludeFromCodeCoverage]
    public class AutoLogMiddleware
    {
        private readonly ILogger<AutoLogMiddleware> _logger;

        private readonly RequestDelegate _next;
        private readonly AutoLogOptions  _options;

        public AutoLogMiddleware(RequestDelegate next,
                                 ILoggerFactory  loggerFactory,
                                 AutoLogOptions  options)
        {
            _next    = next;
            _options = options;
            _logger  = loggerFactory.CreateLogger<AutoLogMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequestAsync(context);
            await _next(context);
        }

        private async Task LogRequestAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            context.Request.Body.Position = 0;
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync()
                                                                          .ConfigureAwait(false);
            context.Request.Body.Position = 0;
            var headersBuilder = new StringBuilder();
            foreach (var (name, value) in context.Request.Headers)
                headersBuilder.AppendLine($"{name}: {value}");
            _logger.Log(_options.LogLevel, HrimsoftApiLogs.LogRequest, headersBuilder.ToString(), requestBody);
        }
    }
}