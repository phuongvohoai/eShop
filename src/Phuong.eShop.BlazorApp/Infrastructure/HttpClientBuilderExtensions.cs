using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Phuong.eShop.BlazorApp.Infrastructure;

public static class HttpClientBuilderExtensions
{
    public static void AddCatalogApiAuthorizationMessageHandler(this IHttpClientBuilder httpClientBuilder, string catalogApiUrl)
    {
        httpClientBuilder.AddHttpMessageHandler(sp =>
        {
            return sp.GetRequiredService<AuthorizationMessageHandler>().ConfigureHandler(authorizedUrls: [catalogApiUrl]);
        });
    }
}