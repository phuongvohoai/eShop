using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Phuong.eShop.ServiceDefaults.Extensions;

public static class ApiServiceExtensions
{
    public static void AddApiServicesWithMediatR(this IHostApplicationBuilder builder, Assembly mediatrAssembly)
    {
        builder.AddApiServices();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(mediatrAssembly));
    }
    
    public static void AddMappings(this IServiceCollection services, Assembly mappingAssembly)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(mappingAssembly);
    }
    
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddAspireServiceDefaults();
        builder.Services.AddControllers();
        
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
    }
    
    public static void ConfigureApiServiceMiddleware(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapDefaultEndpoints();
        app.MapControllers();
    }
}