using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.AuthorizationPolicies;
using WebStore.Infrastructure.Conventions;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.DTO;
using WebStore.Interfaces.TestAPI;
using WebStore.Services;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Identity;
using WebStore.WebAPI.Clients.Orders;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Values;

var builder = WebApplication.CreateBuilder(args);

#region Services
var services = builder.Services;
services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new AddAreasControllerRoute());
});

var configuration = builder.Configuration;

services.AddIdentity<User, Role>()
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

services.AddScoped<ICartService, InCookiesCartService>();


services.AddHttpClient("WebStoreAPI", client => client.BaseAddress = new(configuration["WebAPI"]))
    .AddTypedClient<IEmployeesDTOData, EmployeesClient>()
    .AddTypedClient<IValuesService, ValuesClient>()
    .AddTypedClient<IProductDTOData, ProductsClient>()
    .AddTypedClient<IOrderService, OrderClient>();

services.AddAutoMapper(typeof(Program));

#endregion

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


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
