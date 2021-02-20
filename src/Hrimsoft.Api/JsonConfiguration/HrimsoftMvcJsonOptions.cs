using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Hrimsoft.Api
{
    /// <summary>
    /// Sets the default json settings for apis
    /// </summary>
    public static class HrimsoftMvcJsonOptions
    {
        /// <summary>
        /// Sets the default json settings for all apis
        /// </summary>ы
        /// <param name="builder"></param>
        /// <param name="customize">You can finally customize JsonOptions as you wish</param>
        /// <returns></returns>
        public static IMvcBuilder AddHrimsoftJsonOptions(this IMvcBuilder builder, Action<JsonOptions> customize = null)
        {
            return builder.AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opt.JsonSerializerOptions.PropertyNamingPolicy   = new SnakeCaseNamingPolicy();
                opt.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new SnakeCaseNamingPolicy()));
                customize?.Invoke(opt);
            });
        }
    }
}