using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using PromptStudio.Core.Interfaces;
using PromptStudio.Data;
using PromptStudio.Core.Services;
// Using fully qualified name for PromptStudioMcpTools to resolve namespace issue
// using PromptStudio.Mcp.Tools;

// Create host builder for MCP server
var builder = Host.CreateApplicationBuilder(args);

// Configure logging to stderr for MCP compatibility
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Configure Entity Framework with SQL Server
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
if (string.IsNullOrEmpty(connectionString))
{
    // Fallback to default SQL Server connection
    connectionString = "Server=localhost,1433;Database=PromptStudio;User Id=sa;Password=Two3RobotDuckTag!;TrustServerCertificate=true;";
}

builder.Services.AddDbContext<PromptStudioDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register application services
builder.Services.AddScoped<IPromptStudioDbContext>(provider => provider.GetRequiredService<PromptStudioDbContext>());
builder.Services.AddScoped<IPromptService, PromptService>();

// Configure native C# MCP server with tools
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<PromptStudio.Mcp.Tools.PromptStudioMcpTools>();

// Build the host
var host = builder.Build();

// Ensure database is created
using (var scope = host.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();
    try
    {
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogWarning(ex, "Database may already exist or migration failed");
    }
}

// Run the MCP server
await host.RunAsync();
