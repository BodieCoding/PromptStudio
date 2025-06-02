using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromptStudio.Data;
using PromptStudio.Core.Services;
using PromptStudio.Core.Interfaces;

namespace PromptStudio.Tests.Infrastructure;

/// <summary>
/// Startup class for testing that mirrors the main application configuration
/// </summary>
public class TestStartup
{
    public TestStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add MVC, API Controllers, and Razor Pages
        services.AddControllers();
        services.AddRazorPages();
        
        // Configure Entity Framework with in-memory database for tests
        services.AddDbContext<PromptStudioDbContext>(options =>
            options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

        // Register application services
        services.AddScoped<IPromptService, PromptService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Ensure database exists and is up to date
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PromptStudioDbContext>();
            context.Database.EnsureCreated();
        }        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Enable HTTPS redirection and static files
        app.UseHttpsRedirection();
        app.UseStaticFiles();
          // Enable routing and authorization
        app.UseRouting();
        app.UseAuthorization();

        // Configure endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    }
}
