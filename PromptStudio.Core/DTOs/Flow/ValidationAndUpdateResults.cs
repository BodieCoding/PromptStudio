namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Represents the result of workflow validation operations.
/// Provides comprehensive feedback on workflow structure, dependencies, and potential issues.
/// </summary>
/// <remarks>
/// <para><strong>Validation Scope:</strong></para>
/// <para>Covers structural validation, dependency checking, performance analysis,
/// security assessment, and compliance verification. Used for pre-execution validation
/// and workflow quality assurance processes.</para>
/// </remarks>
public class ValidationResult
{
    /// <summary>
    /// Gets or sets whether the validation passed overall.
    /// </summary>
    /// <value>True if all validation checks passed successfully; otherwise, false.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the overall validation score.
    /// </summary>
    /// <value>Numeric score representing validation quality (0-100).</value>
    public double ValidationScore { get; set; }

    /// <summary>
    /// Gets or sets the validation errors found.
    /// </summary>
    /// <value>Collection of validation errors that prevent execution.</value>
    public List<ValidationError>? Errors { get; set; }

    /// <summary>
    /// Gets or sets informational validation messages.
    /// </summary>
    /// <value>Collection of informational messages about the validation process.</value>
    public List<ValidationInfo>? InfoMessages { get; set; }

    /// <summary>
    /// Gets or sets the validation timestamp.
    /// </summary>
    /// <value>When the validation was performed.</value>
    public DateTime ValidationTimestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the validation duration.
    /// </summary>
    /// <value>Time taken to complete the validation process.</value>
    public TimeSpan ValidationDuration { get; set; }

    /// <summary>
    /// Gets or sets validation categories and their results.
    /// </summary>
    /// <value>Dictionary of validation categories and their specific results.</value>
    public Dictionary<string, ValidationCategoryResult>? CategoryResults { get; set; }

    /// <summary>
    /// Gets or sets recommended actions based on validation results.
    /// </summary>
    /// <value>List of recommended actions to address validation issues.</value>
    public List<string>? RecommendedActions { get; set; }

    /// <summary>
    /// Gets or sets validation metadata.
    /// </summary>
    /// <value>Additional metadata about the validation process.</value>
    public Dictionary<string, object>? ValidationMetadata { get; set; }
}

/// <summary>
/// Represents informational validation messages.
/// </summary>
public class ValidationInfo
{
    /// <summary>
    /// Gets or sets the information code.
    /// </summary>
    public string InfoCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the information message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the information category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets additional information details.
    /// </summary>
    public Dictionary<string, object>? Details { get; set; }
}

/// <summary>
/// Represents validation results for a specific category.
/// </summary>
public class ValidationCategoryResult
{
    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether this category passed validation.
    /// </summary>
    public bool IsPassed { get; set; }

    /// <summary>
    /// Gets or sets the category score.
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    /// Gets or sets the number of checks performed.
    /// </summary>
    public int ChecksPerformed { get; set; }

    /// <summary>
    /// Gets or sets the number of checks that passed.
    /// </summary>
    public int ChecksPassed { get; set; }

    /// <summary>
    /// Gets or sets category-specific details.
    /// </summary>
    public Dictionary<string, object>? CategoryDetails { get; set; }
}

/// <summary>
/// Enumeration of validation severity levels.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// Informational message that doesn't require action.
    /// </summary>
    Info = 0,

    /// <summary>
    /// Warning that suggests improvement but doesn't prevent execution.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Error that prevents execution and must be fixed.
    /// </summary>
    Error = 2,

    /// <summary>
    /// Critical error that indicates serious structural problems.
    /// </summary>
    Critical = 3
}

/// <summary>
/// Represents backup version information for workflow updates.
/// </summary>
public class BackupVersionInfo
{
    /// <summary>
    /// Gets or sets the backup version identifier.
    /// </summary>
    public string BackupVersionId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets when the backup was created.
    /// </summary>
    public DateTime BackupTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the backup location or reference.
    /// </summary>
    public string BackupLocation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the backup size in bytes.
    /// </summary>
    public long BackupSizeBytes { get; set; }

    /// <summary>
    /// Gets or sets backup metadata.
    /// </summary>
    public Dictionary<string, object>? BackupMetadata { get; set; }
}

/// <summary>
/// Represents execution impact analysis for workflow updates.
/// </summary>
public class ExecutionImpactAnalysis
{
    /// <summary>
    /// Gets or sets the number of active executions that may be affected.
    /// </summary>
    public int ActiveExecutionsAffected { get; set; }

    /// <summary>
    /// Gets or sets the estimated impact level.
    /// </summary>
    public ImpactLevel EstimatedImpact { get; set; }

    /// <summary>
    /// Gets or sets impact mitigation strategies.
    /// </summary>
    public List<string>? MitigationStrategies { get; set; }

    /// <summary>
    /// Gets or sets the recommended update timing.
    /// </summary>
    public string? RecommendedTiming { get; set; }
}

/// <summary>
/// Represents information about an execution affected by workflow updates.
/// </summary>
public class AffectedExecution
{
    /// <summary>
    /// Gets or sets the execution identifier.
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Gets or sets the execution status.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the expected impact on this execution.
    /// </summary>
    public ImpactLevel Impact { get; set; }

    /// <summary>
    /// Gets or sets recommended actions for this execution.
    /// </summary>
    public List<string>? RecommendedActions { get; set; }
}

/// <summary>
/// Represents rollback information for workflow updates.
/// </summary>
public class RollbackInfo
{
    /// <summary>
    /// Gets or sets whether rollback is available.
    /// </summary>
    public bool IsRollbackAvailable { get; set; }

    /// <summary>
    /// Gets or sets the rollback version identifier.
    /// </summary>
    public string? RollbackVersionId { get; set; }

    /// <summary>
    /// Gets or sets the estimated rollback time.
    /// </summary>
    public TimeSpan? EstimatedRollbackTime { get; set; }

    /// <summary>
    /// Gets or sets rollback prerequisites.
    /// </summary>
    public List<string>? RollbackPrerequisites { get; set; }

    /// <summary>
    /// Gets or sets rollback risks and considerations.
    /// </summary>
    public List<string>? RollbackRisks { get; set; }
}

/// <summary>
/// Represents performance metrics for workflow updates.
/// </summary>
public class UpdatePerformanceMetrics
{
    /// <summary>
    /// Gets or sets the update processing time.
    /// </summary>
    public TimeSpan UpdateProcessingTime { get; set; }

    /// <summary>
    /// Gets or sets the validation time.
    /// </summary>
    public TimeSpan ValidationTime { get; set; }

    /// <summary>
    /// Gets or sets the backup creation time.
    /// </summary>
    public TimeSpan BackupCreationTime { get; set; }

    /// <summary>
    /// Gets or sets the deployment time.
    /// </summary>
    public TimeSpan DeploymentTime { get; set; }

    /// <summary>
    /// Gets or sets resource usage during update.
    /// </summary>
    public ResourceConsumption? ResourceUsage { get; set; }
}

/// <summary>
/// Represents compliance validation for workflow updates.
/// </summary>
public class ComplianceValidation
{
    /// <summary>
    /// Gets or sets whether the update meets compliance requirements.
    /// </summary>
    public bool IsCompliant { get; set; }

    /// <summary>
    /// Gets or sets the compliance frameworks checked.
    /// </summary>
    public List<string>? ComplianceFrameworks { get; set; }

    /// <summary>
    /// Gets or sets compliance violations found.
    /// </summary>
    public List<string>? Violations { get; set; }

    /// <summary>
    /// Gets or sets compliance recommendations.
    /// </summary>
    public List<string>? Recommendations { get; set; }

    /// <summary>
    /// Gets or sets the compliance officer approval status.
    /// </summary>
    public string? ApprovalStatus { get; set; }
}

/// <summary>
/// Enumeration of impact levels.
/// </summary>
public enum ImpactLevel
{
    /// <summary>
    /// No significant impact expected.
    /// </summary>
    None = 0,

    /// <summary>
    /// Low impact that should be manageable.
    /// </summary>
    Low = 1,

    /// <summary>
    /// Medium impact that requires careful planning.
    /// </summary>
    Medium = 2,

    /// <summary>
    /// High impact that requires extensive planning and coordination.
    /// </summary>
    High = 3,

    /// <summary>
    /// Critical impact that may require system downtime or major changes.
    /// </summary>
    Critical = 4
}
