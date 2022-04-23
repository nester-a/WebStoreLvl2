using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;

namespace WebStore.DAL.Sqlite.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebStoreDBSqlLite(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WebStoreDB>(opt => opt.UseSqlite(connectionString, o => o.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().ToString())));
        return services;
    }
}
