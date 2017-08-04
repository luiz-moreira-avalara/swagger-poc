using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Swagger.PoC.Filters
{
    public class SwaggerAuthorizationFilter : IDocumentFilter
    {
        private readonly IServiceProvider _provider;

        public SwaggerAuthorizationFilter(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {

            var http = _provider.GetRequiredService<IHttpContextAccessor>();
            var auth = _provider.GetRequiredService<IAuthorizationService>();

            var descriptions = context.ApiDescriptionsGroups.Items.SelectMany(group => group.Items);

            foreach (var description in descriptions)
            {
                if (IsAllowAnonymous(description))
                    continue;

                var authAttributes = description.ControllerAttributes()
                    .OfType<AuthorizeAttribute>()
                    .Union(description.ActionAttributes()
                        .OfType<AuthorizeAttribute>()).ToList();


                var notShowen = IsForbiddenDueAnonymous(http, authAttributes) ||
                                IsForbiddenDuePolicy(http, auth, authAttributes);

                if (!notShowen)
                    continue; 

                var route = $"/{description.RelativePath.TrimEnd('/')}";
                var path = swaggerDoc.Paths[route];

                RemoveMethod(swaggerDoc, description, path, route);
            }
        }

        private static void RemoveMethod(SwaggerDocument swaggerDoc, ApiDescription description, PathItem path, string route)
        {
            switch (description.HttpMethod)
            {
                case "DELETE":
                    path.Delete = null;
                    break;
                case "GET":
                    path.Get = null;
                    break;
                case "HEAD":
                    path.Head = null;
                    break;
                case "OPTIONS":
                    path.Options = null;
                    break;
                case "PATCH":
                    path.Patch = null;
                    break;
                case "POST":
                    path.Post = null;
                    break;
                case "PUT":
                    path.Put = null;
                    break;
                default: throw new ArgumentOutOfRangeException("Method name not mapped to operation");
            }

            if (path.Delete == null && path.Get == null &&
                path.Head == null && path.Options == null &&
                path.Patch == null && path.Post == null && path.Put == null)
                swaggerDoc.Paths.Remove(route);
        }

        private static bool IsAllowAnonymous(ApiDescription description)
        {
            return description.ActionDescriptor.FilterDescriptors.Any(x => x.Filter.GetType() == typeof(AllowAnonymousFilter));
        }

        private static bool IsForbiddenDuePolicy(
            IHttpContextAccessor http,
            IAuthorizationService auth,
            IEnumerable<AuthorizeAttribute> attributes)
        {
            var policies = attributes
                .Where(p => !string.IsNullOrEmpty(p.Policy))
                .Select(a => a.Policy)
                .Distinct();
            return policies.Any(p => Task.Run(async () => await auth.AuthorizeAsync(http.HttpContext.User, p)).Result ==
                                     false);
        }

        private static bool IsForbiddenDueAnonymous(
            IHttpContextAccessor http,
            IEnumerable<AuthorizeAttribute> attributes)
        {
            return attributes.Any() && !http.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
