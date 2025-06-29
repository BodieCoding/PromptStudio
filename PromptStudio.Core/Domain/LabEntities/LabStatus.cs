// Enhanced PromptLab with Guid identifier and enterprise-scale considerations
namespace PromptStudio.Core.Domain;

/// <summary>
/// Lab lifecycle status
/// </summary>
public enum LabStatus
{
    Active = 0,
    Archived = 1,
    Deleted = 2,
    Suspended = 3  // For enterprise compliance scenarios
}
