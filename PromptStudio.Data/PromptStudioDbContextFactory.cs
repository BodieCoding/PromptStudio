using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PromptStudio.Data;

/// <summary>
/// Design-time DbContext factory for Entity Framework migrations
/// </summary>
public class PromptStudioDbContextFactory : IDesignTimeDbContextFactory<PromptStudioDbContext>
{
    /// <summary>
    /// Creates a new DbContext instance for design-time operations
    /// </summary>
    /// <param name="args">Command line arguments</param>
    /// <returns>A configured DbContext instance</returns>
    public PromptStudioDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PromptStudioDbContext>();
        
        // Use SQL Server for design-time operations (migrations)
        // Default connection for local development
        var connectionString = "Server=localhost,1433;Database=PromptStudio;User Id=sa;Password=Two3RobotDuckTag!;TrustServerCertificate=true;";
        optionsBuilder.UseSqlServer(connectionString);

        return new PromptStudioDbContext(optionsBuilder.Options);
    }
}
