namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the classification of version changes following enterprise semantic versioning principles for AI template management.
/// 
/// <para><strong>Business Context:</strong></para>
/// Version change types enable systematic impact assessment, deployment planning, and backwards compatibility
/// management for AI templates and workflows. This classification supports release management processes,
/// risk assessment, and communication strategies for template evolution across enterprise environments.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Change types correspond to semantic versioning standards (MAJOR.MINOR.PATCH) with extensions for
/// AI-specific scenarios like experimental features and hotfixes. Each type has specific deployment,
/// testing, and notification requirements that ensure controlled and safe template evolution.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Predictable impact assessment for template changes
/// - Systematic deployment and rollback planning
/// - Clear communication of change significance
/// - Risk-appropriate approval and testing workflows
/// </summary>
/// <remarks>
/// <para><strong>Version Strategy:</strong></para>
/// - Major: Breaking changes requiring careful migration planning
/// - Minor: New capabilities with backward compatibility assurance
/// - Patch: Safe improvements with minimal risk assessment
/// - Hotfix: Emergency fixes with expedited deployment
/// - Experimental: Innovation testing with isolated deployment
/// 
/// <para><strong>Deployment Considerations:</strong></para>
/// Different change types require different approval workflows, testing levels,
/// and deployment strategies to balance innovation with operational stability.
/// 
/// <para><strong>Impact Assessment:</strong></para>
/// Change type determines the scope of impact analysis, dependency checking,
/// and migration planning required for safe template evolution.
/// </remarks>
/// <example>
/// Changing a template's API parameters would be Major, adding optional features would be Minor,
/// and fixing a formatting bug would be Patch.
/// </example>
public enum VersionChangeType
{
    /// <summary>
    /// Breaking changes that require migration and careful deployment planning.
    /// Includes API changes, removed features, altered behavior, or incompatible modifications
    /// that may impact existing workflows and require coordinated updates.
    /// </summary>
    Major = 0,
    
    /// <summary>
    /// New features and enhancements that maintain backward compatibility.
    /// Includes additional capabilities, optional parameters, performance improvements,
    /// and extensions that don't break existing functionality or workflows.
    /// </summary>
    Minor = 1,
    
    /// <summary>
    /// Bug fixes, minor improvements, and maintenance updates.
    /// Includes error corrections, small optimizations, documentation updates,
    /// and low-risk improvements that don't change core functionality.
    /// </summary>
    Patch = 2,
    
    /// <summary>
    /// Critical bug fixes requiring immediate deployment outside normal release cycles.
    /// Includes security fixes, urgent defect resolution, and emergency corrections
    /// that address production issues with minimal testing and approval overhead.
    /// </summary>
    Hotfix = 3,
    
    /// <summary>
    /// Experimental features for testing and validation in controlled environments.
    /// Includes prototype capabilities, beta features, and innovation experiments
    /// that require isolated deployment and careful evaluation before general availability.
    /// </summary>
    Experimental = 4
}
