using System.ComponentModel.DataAnnotations;
using PromptStudio.Core.Domain;

namespace PromptStudio.Core.Domain;

/// <summary>
/// Represents an A/B test for comparing different prompts or configurations
/// </summary>
public class ABTest : AuditableEntity
{
    /// <summary>
    /// Unique identifier for the A/B test
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Test name
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Test description
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Test hypothesis
    /// </summary>
    public string? Hypothesis { get; set; }

    /// <summary>
    /// Test configuration in JSON format
    /// </summary>
    [Required]
    public string Configuration { get; set; } = string.Empty;

    /// <summary>
    /// Test status
    /// </summary>
    public ABTestStatus Status { get; set; } = ABTestStatus.Draft;

    /// <summary>
    /// Test start date
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Test end date
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Test results in JSON format
    /// </summary>
    public string? Results { get; set; }

    /// <summary>
    /// Test metadata
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// Entity type being tested
    /// </summary>
    public TestEntityType EntityType { get; set; } = TestEntityType.Prompt;

    /// <summary>
    /// Target entity ID
    /// </summary>
    public Guid? TargetEntityId { get; set; }
}

/// <summary>
/// A/B test status enumeration
/// </summary>
public enum ABTestStatus
{
    Draft = 0,
    Active = 1,
    Paused = 2,
    Completed = 3,
    Cancelled = 4,
    Archived = 5
}

/// <summary>
/// Test entity type enumeration
/// </summary>
public enum TestEntityType
{
    Prompt = 0,
    Template = 1,
    Workflow = 2,
    Model = 3,
    Configuration = 4
}

/// <summary>
/// Represents a variant in an A/B test
/// </summary>
public class ABTestVariant : AuditableEntity
{
    /// <summary>
    /// Unique identifier for the variant
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A/B test ID this variant belongs to
    /// </summary>
    public Guid ABTestId { get; set; }

    /// <summary>
    /// Variant name
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Variant description
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Variant configuration in JSON format
    /// </summary>
    [Required]
    public string Configuration { get; set; } = string.Empty;

    /// <summary>
    /// Traffic allocation percentage (0-100)
    /// </summary>
    public double TrafficAllocation { get; set; } = 50.0;

    /// <summary>
    /// Whether this is the control variant
    /// </summary>
    public bool IsControl { get; set; }

    /// <summary>
    /// Variant metadata
    /// </summary>
    public string? Metadata { get; set; }

    /// <summary>
    /// Navigation property to A/B test
    /// </summary>
    public virtual ABTest? ABTest { get; set; }
}
