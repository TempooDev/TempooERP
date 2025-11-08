using Microsoft.EntityFrameworkCore;
using TempooERP.Infrastructure.Data;
using TempooERP.Infrastructure.Extensions;
using TempooERP.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
var frontendOrigin = builder.Configuration["Frontend:Origin"]
                      ?? "http://localhost:4200";
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSingleton<SeedDatabase>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(frontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
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

    var seeder = scope.ServiceProvider.GetService<SeedDatabase>();
    if (seeder is not null)
    {
        Console.WriteLine("Seeding database...");
        await seeder.SeedAsync();
        Console.WriteLine("Database seeded.");
    }
}

app.UseCors();
app.MapGet("/api/health", () => Results.Ok(new { ok = true }));

app.Run();
