using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swagger.PoC.Extension;

namespace Swagger.PoC
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _hostingEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPolicies();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc().AddJsonOptions(
                opts => { opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); }); ;

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.DescribeAllParametersInCamelCase();
                options.DescribeStringEnumsInCamelCase();

                options.SetSwaggerInfo()
                    .AddComments(_hostingEnvironment)
                    .ConfigureOAuth2();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecretKey"]))
                }
            });
            app.UseMvc(routes => routes.MapRoute(
                name: "Default",
                template: "api/v2"));
            app.UseSwagger();
            app.UseSwaggerUI(action =>
            {
                action.RoutePrefix = "api-docs";
                action.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
            });
        }
    }
}
