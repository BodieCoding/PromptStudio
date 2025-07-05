namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents the validation result for a specific node within a flow, including node-specific errors and warnings.
/// </summary>
/// <remarks>
/// <para><strong>Service Integration:</strong></para>
/// <para>Node-level validation result DTO used by flow validation services and development tools for granular error reporting.
/// Essential for providing specific feedback about individual node configurations and dependencies within complex flows.</para>
/// 
/// <para><strong>Data Contract:</strong></para>
/// <para>Node-specific validation data with identity information and categorized issues for targeted problem resolution.
/// Designed for integration with flow editors, validation pipelines, and developer feedback systems.</para>
/// 
/// <para><strong>Usage Patterns:</strong></para>
/// <list type="bullet">
/// <item>Node-specific validation feedback in flow editors</item>
/// <item>Granular error reporting and issue localization</item>
/// <item>Development tool integration and syntax highlighting</item>
/// <item>Automated quality checks and compliance validation</item>
/// </list>
/// 
/// <para><strong>Performance Notes:</strong></para>
/// <para>Lightweight node validation representation designed for collection processing within flow validation results.
/// Error and warning lists should be bounded to prevent excessive feedback that overwhelms development interfaces.</para>
/// </remarks>
public class NodeValidationResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the validated node.
    /// </summary>
    /// <value>A GUID that uniquely identifies the node within the flow.</value>
    public Guid NodeId { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name of the validated node.
    /// </summary>
    /// <value>A string providing a descriptive name for the node for display purposes.</value>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the node passes all validation checks.
    /// </summary>
    /// <value>True if the node is valid; otherwise, false if errors were found.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the collection of node-specific validation errors.
    /// </summary>
    /// <value>A list of error messages specific to this node that must be resolved.</value>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Gets or sets the collection of node-specific validation warnings.
    /// </summary>
    /// <value>A list of warning messages specific to this node suggesting improvements or best practices.</value>
    public List<string> Warnings { get; set; } = new();
}
