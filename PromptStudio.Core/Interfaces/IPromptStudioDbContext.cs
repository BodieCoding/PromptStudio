using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Interfaces;

/// <summary>
/// Interface for the PromptStudio database context
/// </summary>
public interface IPromptStudioDbContext
{
    DbSet<Collection> Collections { get; set; }
    DbSet<PromptTemplate> PromptTemplates { get; set; }
    DbSet<PromptVariable> PromptVariables { get; set; }
    DbSet<PromptExecution> PromptExecutions { get; set; }
    DbSet<VariableCollection> VariableCollections { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}
