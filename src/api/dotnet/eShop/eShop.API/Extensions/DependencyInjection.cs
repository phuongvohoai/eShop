namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();

        services.AddDbContext<EShopIdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Identity")));

        services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<EShopIdentityDbContext>();
    }

    public static void UpdateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var identityDbContext = scope.ServiceProvider.GetRequiredService<EShopIdentityDbContext>();
        identityDbContext.Database.Migrate();
    }
}
