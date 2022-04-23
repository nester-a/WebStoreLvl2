using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;

namespace WebStore.DAL.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebStoreDBSqlServer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(connectionString));
        return services;
    }
}
