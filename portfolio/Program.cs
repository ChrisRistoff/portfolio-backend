using FluentMigrator.Runner;
using portfolio.Models;
using portfolio.Repositories;
using portfolio.Seed;
using portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

string connectionStringName = "";

if (env == "Testing")
{
    connectionStringName = "TestConnection";
}

if (env == "Development" || env == "Production")
{
    connectionStringName = "DefaultConnection";
}


Console.WriteLine($"Environment: {env}");
Console.WriteLine($"Connection String: {connectionStringName}");

builder.Services
    .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString(connectionStringName))
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

Task Migrate(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateDown(0);
    runner.MigrateUp();

    return Task.CompletedTask;
}

// register repositories
builder.Services.AddScoped<PersonalInfoRepository>();
builder.Services.AddScoped<ProjectInfoRepository>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();

var app = builder.Build();

if (env == "Testing")
{
    await Migrate(app.Services);
    await SeedTest.Seed(builder.Configuration.GetConnectionString(connectionStringName)!);
}

if (env == "Development" || env == "Production")
{

    await Migrate(app.Services);
    await SeedProd.Seed(builder.Configuration.GetConnectionString(connectionStringName)!);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program {}
