using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TempooERP.Api.Modules;
using TempooERP.BuildingBlocks.API.Middleware;
using TempooERP.Infrastructure.Data;
using TempooERP.Infrastructure.Extensions;
using TempooERP.Modules.Catalog.Infrastructure;
using TempooERP.Modules.Sales.Infrastructure;
using TempooERP.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var frontendOrigin = builder.Configuration["Frontend:Origin"]
                      ?? "http://localhost:4200";

builder.Services.AddValidation();
builder.Services
    .AddInfrastructure(builder.Configuration)
    .ConfigureCatalogServices()
    .ConfigureSalesServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddScoped<SeedDatabase>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
            policy.WithOrigins(frontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("traceparent", "tracestate")
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    Console.WriteLine("Applying database migrations...");
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ErpDbContext>();
    await dbContext.Database.MigrateAsync();

    await dbContext.Database.EnsureCreatedAsync();
    Console.WriteLine("Database migrations applied.");

    var seeder = scope.ServiceProvider.GetRequiredService<SeedDatabase>();
    Console.WriteLine("Seeding database (idempotent)...");
    await seeder.SeedAsync();
    Console.WriteLine("Database seed step finished.");
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler();
}

app.UseCors();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("/api/health", () => Results.Ok(new { ok = true }));

app.MapCatalogEndpoints();
app.MapSalesEndpoints();

app.Run();
