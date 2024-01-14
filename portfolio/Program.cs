using System.Text;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using portfolio.Auth;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "Open Source Recipes", Version = "v1"});

    // add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



builder.Services.AddControllers();

string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

string connectionStringName = "";

if (env == "Testing")
{
    connectionStringName = "TestConnection";
}

if (env == "Development")
{
    connectionStringName = "DefaultConnection";
}

if (env == "Production")
{
    connectionStringName = "ProductionConnection";
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

Task MigrateProd(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();

    return Task.CompletedTask;
}

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// register repositories
builder.Services.AddScoped<PersonalInfoRepository>();
builder.Services.AddScoped<ProjectInfoRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AdminRepository>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty))
        };
    });
    /*
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
    */

var app = builder.Build();

if (env == "Testing")
{
    await Migrate(app.Services);
    await SeedTest.Seed(builder.Configuration.GetConnectionString(connectionStringName)!, builder.Configuration);
}

if (env == "Development")
{

    await Migrate(app.Services);
    await SeedProd.Seed(builder.Configuration.GetConnectionString(connectionStringName)!);
    // await SeedAdmin.Seed(builder.Configuration.GetConnectionString(connectionStringName)!, builder.Configuration);

    app.UseSwagger();
    app.UseSwaggerUI();
}

if (env == "Production")
{
    await MigrateProd(app.Services);

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program {}
