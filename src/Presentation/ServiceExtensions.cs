using BusinessLogic;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddUriService(this IServiceCollection instance)
        {
            instance.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            instance.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), request.Path.Value);
                return new UriService(absoluteUri);
            });
        }
    }
}