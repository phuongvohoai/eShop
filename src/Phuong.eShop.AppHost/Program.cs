var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin();

builder.AddProject<Projects.Phuong_eShop_IdentityService>("IdentityService")
    .WithReference(postgres.AddDatabase("identityDb"));

builder.AddProject<Projects.Phuong_eShop_CatalogService>("CatalogService")
    .WithReference(postgres.AddDatabase("catalogDb"));

builder.AddNpmApp("eShopWebApp", "../Phuong.eShop.WebApp", "dev")
    .WithHttpEndpoint(env: "PORT");

builder.Build().Run();