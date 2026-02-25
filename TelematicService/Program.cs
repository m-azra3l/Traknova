using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;
using System.Reflection;
using TelematicService.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Load environment variables from a .env file or host environment
// This is preferred over appsettings.json for sensitive information
Env.Load();

// Add environment variables with a custom prefix to configuration
builder.Configuration.AddEnvironmentVariables(prefix: "TELEMATICS_");

// Retrieve log directory setting from environment variables
var logDir = builder.Configuration.GetValue<string>("LogDirectory") ??
    throw new ArgumentNullException("Directory settings 'LogDirectory' not found.");

// Retrieve MSSQL connection string from environment variables
var connStringMssql = builder.Configuration.GetValue<string>("MssqlDB") ??
    throw new ArgumentNullException("Connection string 'MssqlDB' not found.");

// Configure Serilog logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(logDir, rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Configure EF Core DbContext with SQL Server provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        connStringMssql,
        sqlOptions =>
        {
            // Use query splitting for performance when loading related data
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

            // Enable automatic retries on transient failures
            sqlOptions.EnableRetryOnFailure();

            // Set command timeout to 60 seconds for long-running queries
            sqlOptions.CommandTimeout(60);
        })
// Uncomment for debugging EF Core queries (sensitive data logging)
//.EnableSensitiveDataLogging().LogTo(message => Debug.WriteLine(message), LogLevel.Information)
//.EnableSensitiveDataLogging().LogTo(Console.WriteLine, LogLevel.Error)
//.EnableDetailedErrors().LogTo(message => Debug.WriteLine(message), LogLevel.Information)
, ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
// Configure swagger docmuent generation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("documentations", new OpenApiInfo { Title = "Telematic Service", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Remove server name from response header
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AddServerHeader = false;
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/documentations/swagger.json", "Telematic Service");
    c.RoutePrefix = "documentations";
    c.InjectStylesheet("/css/SwaggerDark.css");

    // Enable the search box
    c.EnableFilter();
    //c.DefaultModelsExpandDepth(-1); // Disable schema models if not needed
    c.DisplayOperationId();
    c.DisplayRequestDuration();
});

// Modern documentation ui option
app.MapScalarApiReference("/docs", (options, httpContext) =>
{
    options.WithOpenApiRoutePattern("/swagger/documentations/swagger.json");
    options.WithTitle("Telematic Service");
    options.WithDarkModeToggle(true);
    options.WithClientButton(true);
    options.WithDefaultOpenAllTags(false);
    options.WithModels(true);
    options
    .WithDotNetFlag(false);
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseDeveloperExceptionPage();
}

// Enforce HTTP Strict Transport Security (HSTS)
app.UseHsts();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
