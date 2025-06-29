namespace PromptStudio.Core.Domain;

/// <summary>
/// Data classification levels for enterprise security and compliance
/// </summary>
public enum DataClassification
{
    /// <summary>
    /// Public data - can be shared externally
    /// </summary>
    Public = 0,
    
    /// <summary>
    /// Internal data - accessible within organization
    /// </summary>
    Internal = 1,
    
    /// <summary>
    /// Confidential data - restricted access within organization
    /// </summary>
    Confidential = 2,
    
    /// <summary>
    /// Restricted data - highly sensitive, minimal access
    /// </summary>
    Restricted = 3
}
