using Dapper;
using Dapper.FluentMap;
using DataAccess.Maps;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection instance)
        {
            SqlMapper.AddTypeHandler(new ContentParseMap());
            FluentMapper.Initialize(config => { config.AddMap(new ContentMap()); });
            instance.AddScoped<IContentRepository, ContentRepository>();
        }
    }
}