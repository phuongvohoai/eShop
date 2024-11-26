var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithDataVolume("postgres-data").WithPgAdmin();
var identityDb = postgres.AddDatabase("identityDb");
var catalogDb = postgres.AddDatabase("catalogDb");

var keycloak = builder.AddKeycloak("keycloak", 8080);

builder.AddProject<Projects.Phuong_eShop_IdentityService>("IdentityService").WithReference(identityDb);
builder.AddProject<Projects.Phuong_eShop_CatalogService>("CatalogService").WithReference(catalogDb);
builder.AddProject<Projects.Phuong_eShop_BlazorApp>("BlazorApp");

builder.Build().Run();