using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromptStudio.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using System;
using System.Reflection;

namespace PromptStudio.Tests.Infrastructure;

/// <summary>
/// Custom WebApplicationFactory for integration tests
/// </summary>
public class PromptStudioWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddDebug();
        });        builder.ConfigureServices(services =>
        {            // The main Program.cs now handles database provider selection based on environment
            // Since we're using "Testing" environment, it will automatically use InMemory database
            
            // Explicitly scan for controllers from the Web/API project
            // Find the assembly containing your controllers using a known type
            // Option 1: Using the Program class as a reference to find the main assembly
            var mainAssembly = typeof(Program).Assembly;
            
            // Option 2: If your controllers are in a different assembly, use a controller type
            // var mainAssembly = typeof(YourControllerClassName).Assembly;
            
            // Option 3: Load assembly by name if necessary
            // var mainAssembly = Assembly.Load("PromptStudio");
            
            services.AddControllers()
                .AddApplicationPart(mainAssembly);
                
            // Print registered routes to aid debugging
            services.AddSingleton<IStartupFilter, RoutePrinterStartupFilter>();
        });
    }
}

// Helper class to print routes during startup
public class RoutePrinterStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            // Use middleware to print routes
            app.Use(async (context, nextMiddleware) =>
            {
                var endpointFeature = context.Features.Get<IEndpointFeature>();
                var endpoint = endpointFeature?.Endpoint;
                if (endpoint != null)
                {
                    var routePattern = (endpoint as RouteEndpoint)?.RoutePattern?.RawText;
                    Console.WriteLine($"Matched endpoint: {endpoint.DisplayName}, Route: {routePattern}");
                }
                await nextMiddleware();
            });
            
            next(app);
        };
    }
}
