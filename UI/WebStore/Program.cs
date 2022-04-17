using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.AuthorizationPolicies;
using WebStore.Infrastructure.Conventions;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.DTO;
using WebStore.Interfaces.TestAPI;
using WebStore.Services;
using WebStore.Services.InSQL;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Identity;
using WebStore.WebAPI.Clients.Orders;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Values;

var builder = WebApplication.CreateBuilder(args);

#region Services
// Регистрация сервисов
var services = builder.Services;
services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new AddAreasControllerRoute());
});

var configuration = builder.Configuration;
//var db_connection_string_name = configuration["Database"];
//var db_connection_string = configuration.GetConnectionString(db_connection_string_name);

//switch (db_connection_string_name)
//{
//    case "SqlServer":
//    case "DockerDB":
//        services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(db_connection_string));
//        break;

//    case "Sqlite":
//        services.AddDbContext<WebStoreDB>(opt => opt.UseSqlite(db_connection_string, o => o.MigrationsAssembly("WebStore.DAL.Sqlite")));
//        break;
//}

//services.AddTransient<IDbInitializer, DbInitializer>();

services.AddIdentity<User, Role>(/*opt => opt.*/)
   //.AddEntityFrameworkStores<WebStoreDB>()
   .AddDefaultTokenProviders();

services.AddHttpClient("WebStoreAPIIdentity", client => client.BaseAddress = new(configuration["WebAPI"]))
   .AddTypedClient<IUserStore<User>, UsersClient>()
   .AddTypedClient<IUserRoleStore<User>, UsersClient>()
   .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
   .AddTypedClient<IUserEmailStore<User>, UsersClient>()
   .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
   .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
   .AddTypedClient<IUserClaimStore<User>, UsersClient>()
   .AddTypedClient<IUserLoginStore<User>, UsersClient>()
   .AddTypedClient<IRoleStore<Role>, RolesClient>()
   //.AddPolicyHandler(GetRetryPolicy())
   //.AddPolicyHandler(GetCircuitBreakerPolicy())
   .SetHandlerLifetime(TimeSpan.FromMinutes(15));

services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore.GB";
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromDays(10);

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminAuthorization", policy => policy.Requirements.Add(new AdminAuthorizationPolicy(Role.Adinistrators)));
});

//services.AddSingleton<IProductData, SqlProductData>();
services.AddScoped<ICartService, InCookiesCartService>();
//services.AddScoped<IOrderService, SqlOrderService>();

//services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress = new(configuration["WebAPI"]));
//services.AddHttpClient<IEmployeesDTOData, EmployeesClient>(client => client.BaseAddress = new(configuration["WebAPI"]));
//services.AddHttpClient<IProductDTOData, ProductsClient>(client => client.BaseAddress = new(configuration["WebAPI"]));
//services.AddHttpClient<IOrderService, OrderClient>(client => client.BaseAddress = new(configuration["WebAPI"]));

services.AddHttpClient("WebStoreAPI", client => client.BaseAddress = new(configuration["WebAPI"]))
    .AddTypedClient<IEmployeesDTOData, EmployeesClient>()
    .AddTypedClient<IValuesService, ValuesClient>()
    .AddTypedClient<IProductDTOData, ProductsClient>()
    .AddTypedClient<IOrderService, OrderClient>();

services.AddAutoMapper(typeof(Program));

#endregion

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
//    await db_initializer.InitializeAsync(RemoveBefore: false);
//}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/throw", () =>
{
    throw new ApplicationException("Пример ошибки в приложении");
});

app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
