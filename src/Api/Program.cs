using TempooERP.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var frontendOrigin = builder.Configuration["Frontend:Origin"]
                      ?? "http://localhost:4200";
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(frontendOrigin)
              .AllowAnyHeader()
              .AllowAnyMethod()
    );
});

var app = builder.Build();

app.UseCors();
app.MapGet("/api/health", () => Results.Ok(new { ok = true }));

app.Run();
