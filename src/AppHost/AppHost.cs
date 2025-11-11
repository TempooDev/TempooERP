var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres-server")
    .WithPgAdmin()
    .WithLifetime(ContainerLifetime.Persistent);

var erpDb = postgres.AddDatabase("erp-database");

var api = builder.AddProject<Projects.TempooERP_Api>("erp-api")
    .WaitFor(erpDb)
    .WithReference(erpDb);

var web = builder.AddJavaScriptApp("tempooerp-web", "../../web/TempooERP","start")
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WaitFor(api)
    .WithReference(api)
    ;

api.WithEnvironment("Frontend__Origin", web.GetEndpoint("http"));

builder.Build().Run();
