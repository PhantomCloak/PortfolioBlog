using System;
using BusinessLogic.Maps;
using BusinessLogic.Services;
using DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddContentServices(this IServiceCollection instance)
        {
            instance.AddRepositories();
            instance.AddScoped<IContentService, ContentService>();
        }
        public static void AddMapper(this IServiceCollection instance,Type startupClass)
        {
            instance.AddAutoMapper(c=> c.AddProfile(typeof(DtoMapper)),startupClass);
        }
    }
}