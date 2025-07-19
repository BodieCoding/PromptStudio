namespace PromptStudio.Core.DTOs.Models;

/// <summary>
/// Information about an available model
/// </summary>
public class ModelInfo
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public Dictionary<string, object> Capabilities { get; set; } = new();
}
