namespace PromptStudio.Core.Domain;

/// <summary>
/// Template size categories for resource allocation
/// </summary>
public enum TemplateSize
{
    /// <summary>
    /// Small templates (&lt; 1KB) - fast execution
    /// </summary>
    Small = 0,
    
    /// <summary>
    /// Medium templates (1KB - 10KB) - standard execution
    /// </summary>
    Medium = 1,
    
    /// <summary>
    /// Large templates (10KB - 100KB) - longer execution
    /// </summary>
    Large = 2,
    
    /// <summary>
    /// Extra large templates (&gt; 100KB) - specialized handling
    /// </summary>
    ExtraLarge = 3
}
