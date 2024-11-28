using Aspire.Hosting.ApplicationModel;
using Google.Protobuf.WellKnownTypes;

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username");
var password = builder.AddParameter("password");

var postgres = builder.AddPostgres("postgres", username, password).WithDataVolume().WithPgAdmin();
var identityDb = postgres.AddDatabase("identityDb");
var catalogDb = postgres.AddDatabase("catalogDb");

var keycloak = builder.AddKeycloak("keycloak", 8080, username, password).WithDataVolume();

builder.AddProject<Projects.Phuong_eShop_IdentityService>("IdentityService").WithReference(identityDb);
builder.AddProject<Projects.Phuong_eShop_CatalogService>("CatalogService").WithReference(catalogDb);
builder.AddProject<Projects.Phuong_eShop_BlazorApp>("BlazorApp").WithReference(keycloak);

builder.Build().Run();