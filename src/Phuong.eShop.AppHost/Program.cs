var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin();

builder.AddProject<Projects.Phuong_eShop_IdentityService>("IdentityService")
    .WithReference(postgres.AddDatabase("identityDb"));

builder.AddProject<Projects.Phuong_eShop_CatalogService>("CatalogService")
    .WithReference(postgres.AddDatabase("catalogDb"));

builder.AddProject<Projects.Phuong_eShop_BlazorApp>("BlazorApp");

builder.Build().Run();