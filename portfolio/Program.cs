using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

string connectionStringName = "";

if (env == "Testing")
{
    connectionStringName = "Test";
}

if (env == "Development" || env == "Production")
{
    connectionStringName = "Default";
}

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString(connectionStringName))
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

static void Migrate(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateDown(0);
    runner.MigrateUp();
}

if (env == "Testing")
{
    Migrate(app.Services);
    // SEED TEST DATABASE
}

if (env == "Development" || env == "Production")
{
    Migrate(app.Services);

    app.UseSwagger();
    app.UseSwaggerUI();
    // SEED DATABASE
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.Run();
