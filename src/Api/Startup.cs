﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace swagger_poc
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvrionment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _hostingEnvrionment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(
                opts => { opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); }); ;

            services.AddSwaggerGen(setup =>
            {
                setup.DescribeAllEnumsAsStrings();
                setup.DescribeAllParametersInCamelCase();
                setup.DescribeStringEnumsInCamelCase();

                setup.SwaggerDoc("v2", new Info
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

                var comments = new XPathDocument($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnvrionment.ApplicationName}.xml");
                setup.OperationFilter<XmlCommentsOperationFilter>(comments);
                setup.IncludeXmlComments(() => comments);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(action =>
            {
                action.RoutePrefix = "api-docs";
                action.SwaggerEndpoint("/swagger/v2/swagger.json", "API v1");
            });
        }
    }
}