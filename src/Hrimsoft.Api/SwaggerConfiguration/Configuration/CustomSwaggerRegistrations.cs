using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Hrimsoft.Api
{
    /// <summary> Swagger configuration services registrations </summary>
    public static class CustomSwaggerRegistrations
    {
        /// <summary> Add and setup swagger middleware </summary>
        public static IServiceCollection AddHrimsoftSwagger(
            this IServiceCollection   services,
            IConfiguration            appConfig,
            IHostEnvironment          environment,
            ISwaggerConfig            swaggerConfig,
            Action<SwaggerGenOptions> customize      = null,
            bool                      enableExamples = false)
        {
            if (appConfig == null)
                throw new ArgumentNullException(nameof(appConfig));
            if (swaggerConfig == null)
                throw new ArgumentNullException(nameof(swaggerConfig));

            services.AddSwaggerGen(c =>
            {
                // Configuring different API sections
                c.SwaggerDoc(swaggerConfig.Route,
                             new OpenApiInfo
                             {
                                 Title       = swaggerConfig.Title,
                                 Version     = swaggerConfig.Version,
                                 Description = swaggerConfig.Description,
                             });
                if (enableExamples)
                    c.ExampleFilters();
                // Enabling Swagger to use XML documentation.
                foreach (var xmlFile in swaggerConfig.XmlDocFiles)
                {
                    var xmlPath = Path.Combine(environment.ContentRootPath, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
                c.EnableAnnotations();
                var idpUrlStr   = appConfig["Authorization:IdpUrl"];
                if (!string.IsNullOrWhiteSpace(idpUrlStr))
                {
                    var idpUrl   = new Uri(idpUrlStr);
                    var authUrl  = new Uri(idpUrl, "connect/authorize").AbsoluteUri;
                    var tokenUrl = new Uri(idpUrl, "connect/token").AbsoluteUri;
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                      {
                                                          Description = "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                                          Name        = "Authorization",
                                                          In          = ParameterLocation.Header,
                                                          Type        = SecuritySchemeType.ApiKey
                                                      });
                    c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                                                      {
                                                          Description = "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                                                          Name        = "Authorization",
                                                          Type        = SecuritySchemeType.OAuth2,
                                                          Flows = new OpenApiOAuthFlows
                                                                  {
                                                                      Password = new OpenApiOAuthFlow
                                                                                 {
                                                                                     AuthorizationUrl = new Uri(authUrl),
                                                                                     TokenUrl         = new Uri(tokenUrl),
                                                                                     Scopes = new Dictionary<string, string>
                                                                                              {
                                                                                                  {swaggerConfig.Route, swaggerConfig.ScopeDescription},
                                                                                              }
                                                                                 }
                                                                  }
                                                      });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                             {
                                                 {
                                                     new OpenApiSecurityScheme
                                                     {
                                                         Reference = new OpenApiReference
                                                                     {
                                                                         Type = ReferenceType.SecurityScheme,
                                                                         Id   = "oauth2"
                                                                     }
                                                     },
                                                     Array.Empty<string>()
                                                 }
                                             });
                }
                customize?.Invoke(c);
            });
            services.AddTransient<IHrimsoftJsonSerializeOptionsFactory, HrimsoftJsonSerializeOptionsFactory>();
            // services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        /// <summary> </summary>
        public static IApplicationBuilder UseHrimsoftSwagger(this IApplicationBuilder app, ISwaggerConfig swaggerConfig)
        {
            if (swaggerConfig == null)
                throw new ArgumentNullException(nameof(swaggerConfig));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{swaggerConfig.Route}/swagger.json",
                                  swaggerConfig.Title);
            });
            return app;
        }
    }
}