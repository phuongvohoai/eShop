var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new PageInfo());
builder.Services.AddMudServices();
builder.Services.AddScoped<ICatalogBrandService, CatalogBrandService>();
builder.Services.AddScoped<ICatalogTypeService, CatalogTypeService>();

await builder.Build().RunAsync();