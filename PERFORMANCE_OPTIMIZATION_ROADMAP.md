# PromptStudio Performance Optimization Roadmap

## PHASE 1: Database Performance (Week 2-3)

### 1. Extract Database Seeding
Create separate `DatabaseSeeder` service to move 1300+ lines out of DbContext

### 2. Implement Caching
```csharp
// Add to Program.cs
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

// In PromptService
private readonly IMemoryCache _cache;

public async Task<IList<Collection>> GetCollectionsAsync()
{
    return await _cache.GetOrCreateAsync("collections", async entry =>
    {
        entry.SlidingExpiration = TimeSpan.FromMinutes(30);
        return await _context.Collections
            .Include(c => c.PromptTemplates)
            .ToListAsync();
    });
}
```

### 3. Optimize Queries
```csharp
// Replace N+1 queries with proper includes
public async Task OnGetAsync() => 
    Collections = await _promptService.GetCollectionsWithPromptsAsync();
```

## PHASE 2: Response Optimization (Week 4)

### 1. Add Response Compression
```csharp
builder.Services.AddResponseCompression();
app.UseResponseCompression();
```

### 2. Implement Static File Caching
```csharp
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.CacheControl = "public,max-age=31536000";
    }
});
```

## Target Metrics
- Load Time: 2-3s → <800ms
- Memory: 50-100MB → <30MB
- DB Queries: 100-200ms → <50ms
