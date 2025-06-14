using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using PromptStudio.Data;
using PromptStudioMcpServer.Tools;
using PromptStudio.Core.Services;
using PromptStudio.Core.Interfaces;

// Create host builder for standalone MCP server
var builder = Host.CreateApplicationBuilder(args);

// Configure logging to stderr for MCP compatibility
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Configure Entity Framework with SQL Server for integration with web app
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"] 
    ?? "Server=localhost,1433;Database=PromptStudio;User Id=sa;Password=Two3RobotDuckTag!;TrustServerCertificate=true;";

builder.Services.AddDbContext<PromptStudioDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register application services
builder.Services.AddScoped<IPromptService, PromptService>();

// Configure native C# MCP server with standalone tools
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<PromptStudioMcpTools>();

// Build and run the standalone MCP server
await builder.Build().RunAsync();
