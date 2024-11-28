using Phuong.eShop.BlazorApp.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
    options.ProviderOptions.DefaultScopes.Add("offline_access");
});

var apiUrl = builder.Configuration["CatalogApi:Url"] ?? throw new InvalidOperationException("CatalogApi:Url is missing from configuration");
builder.Services.AddHttpClient("CatalogApi", client => client.BaseAddress = new(apiUrl)).AddCatalogApiAuthorizationMessageHandler(apiUrl);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped(sp => new PageInfo());
builder.Services.AddScoped(sp => new PageInfo());
builder.Services.AddMudServices();
builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();

await builder.Build().RunAsync();