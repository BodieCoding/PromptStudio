namespace PromptStudio.Core.Domain;

/// <summary>
/// Types of version changes
/// </summary>
public enum VersionChangeType
{
    /// <summary>
    /// Major version - breaking changes or significant new features
    /// </summary>
    Major = 0,
    
    /// <summary>
    /// Minor version - new features, backward compatible
    /// </summary>
    Minor = 1,
    
    /// <summary>
    /// Patch version - bug fixes, minor improvements
    /// </summary>
    Patch = 2,
    
    /// <summary>
    /// Hotfix - critical bug fixes
    /// </summary>
    Hotfix = 3,
    
    /// <summary>
    /// Experimental - testing new features
    /// </summary>
    Experimental = 4
}
