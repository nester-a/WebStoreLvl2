using WebStore.DAL.Extensions;
using WebStore.DAL.Sqlite.Extensions;
using WebStore.Interfaces.Services;
using WebStore.Services.InSQL;

namespace WebStore.WebAPI.Infrasturture.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebStoreDB(this IServiceCollection services, IConfiguration configuration)
    {
        var db_connection_string_name = configuration["Database"];
        var db_connection_string = configuration.GetConnectionString(db_connection_string_name);
        switch (db_connection_string_name)
        {
            case "SqlServer":
            case "DockerDB":
                return services.AddWebStoreDBSqlServer(db_connection_string).AddTransient<IDbInitializer, DbInitializer>();
            case "Sqlite":
                return services.AddWebStoreDBSqlLite(db_connection_string).AddTransient<IDbInitializer, DbInitializer>();
            default:
                throw new InvalidOperationException($"База данных формата {db_connection_string_name} не поддерживается");
        }
    }
}
