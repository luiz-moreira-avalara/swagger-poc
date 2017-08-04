using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swagger.PoC.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swagger.PoC.Extension
{
    public static class SwashbuckleExtensions
    {
        public static SwaggerGenOptions ConfigureOAuth2(this SwaggerGenOptions options)
        {
            var schema = new OAuth2Scheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Type = "apiKey",
            };
            schema.Extensions.Add("in", "header");
            schema.Extensions.Add("name", "Authorization");
            options.AddSecurityDefinition("api_key", schema);
            options.DocumentFilter<SwaggerAuthorizationFilter>();

            return options;
        }

        public static SwaggerGenOptions AddComments(this SwaggerGenOptions options, IHostingEnvironment hostingEnvironment)
        {
            var comments =
                new XPathDocument(
                    $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{hostingEnvironment.ApplicationName}.xml");
            options.OperationFilter<XmlCommentsOperationFilter>(comments);
            options.IncludeXmlComments(() => comments);

            return options;
        }

        public static SwaggerGenOptions SetSwaggerInfo(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v2", new Info
            {
                Contact = new Contact
                {
                    Email = "guilhermebfm@gmail.com",
                    Name = "Luiz Guilherme Bauer Fraga Moreira",
                    Url = "https://wwww.github.com/luiz-moreira-avalara/swagger-poc"
                },
                Description = "Swagger proof of concept",
                Version = "Version 2",
                Title = "Swagger PoC",
            });

            return options;
        }

        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
                {
                    config.AddPolicy("pet", p => p.RequireClaim("scope", "pet"));
                    config.AddPolicy("store", p => p.RequireClaim("scope", "store"));
                    config.AddPolicy("user", p => p.RequireClaim("scope", "user"));
                })
                .AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
