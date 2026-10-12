using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using TodoInfo.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InfoDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddHealthChecks()
    .AddDbContextCheck<InfoDbContext>("db", tags: new[] { "ready" });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await DbInitializer.InitializeAsync(app.Services);

app.UseSwagger();
app.UseSwaggerUI();

var api = app.MapGroup("/api");

api.MapGet("/categories", async (InfoDbContext db, CancellationToken ct) =>
    await db.Categories.AsNoTracking().OrderBy(c => c.Id).ToListAsync(ct));

api.MapGet("/priorities", async (InfoDbContext db, CancellationToken ct) =>
    await db.Priorities.AsNoTracking().OrderBy(p => p.Level).ToListAsync(ct));

app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = r => r.Tags.Contains("ready") });

app.Run();
