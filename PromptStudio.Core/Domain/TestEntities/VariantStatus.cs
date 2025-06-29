namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the operational status of individual A/B test variants for traffic management and performance tracking.
/// 
/// <para><strong>Business Context:</strong></para>
/// VariantStatus enables dynamic experiment management by controlling which variants receive traffic
/// and participate in data collection. This granular control allows teams to respond to real-time
/// performance data, pause underperforming variants, and focus traffic on promising alternatives
/// without disrupting the overall experiment framework.
/// 
/// <para><strong>Technical Context:</strong></para>
/// Variant status directly controls traffic routing decisions, metrics collection, and statistical
/// analysis inclusion. The status system enables fine-grained experiment control while maintaining
/// data integrity and ensuring valid statistical comparisons across active variants.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Dynamic experiment optimization through real-time variant control
/// - Risk mitigation by isolating problematic variants quickly
/// - Resource optimization by focusing traffic on promising variants
/// - Experiment integrity maintenance through controlled status transitions
/// - Clear winner identification and promotion workflows
/// </summary>
/// <remarks>
/// <para><strong>Status Transition Patterns:</strong></para>
/// Active → Paused → Active (temporary suspension)
/// Active → Stopped (permanent removal from experiment)
/// Active → Winner (declared best performer)
/// Active → Loser (declared poor performer)
/// 
/// <para><strong>Traffic Allocation Rules:</strong></para>
/// - Active: Receives allocated traffic percentage
/// - Paused: No traffic, can be resumed
/// - Stopped: No traffic, permanent exclusion
/// - Winner: May receive increased traffic allocation
/// - Loser: No traffic, marked for analysis only
/// 
/// <para><strong>Analysis Considerations:</strong></para>
/// - Only Active and Winner variants included in ongoing analysis
/// - Paused variants maintain historical data for resumption
/// - Stopped and Loser variants preserved for post-experiment analysis
/// - Winner status may trigger automatic traffic reallocation
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Traffic Router: Status-based routing decisions
/// - Metrics Collector: Status-controlled data collection
/// - Analysis Engine: Status-aware statistical calculations
/// - Alert System: Status change notifications and monitoring
/// </remarks>
/// <example>
/// <code>
/// // Dynamic variant management based on performance
/// foreach (var variant in abTest.Variants)
/// {
///     if (variant.Status == VariantStatus.Active)
///     {
///         // Include in traffic routing and analysis
///         if (variant.ConversionRate < threshold)
///         {
///             variant.Status = VariantStatus.Paused;
///             await NotifyVariantPaused(variant);
///         }
///     }
/// }
/// 
/// // Declare winner and update traffic allocation
/// var winner = variants.OrderByDescending(v => v.ConversionRate).First();
/// winner.Status = VariantStatus.Winner;
/// await ReallocateTraffic(winner);
/// </code>
/// </example>
public enum VariantStatus
{
    /// <summary>
    /// Variant is actively participating in the experiment with full traffic allocation.
    /// <value>0 - Variant receives traffic and participates in data collection</value>
    /// </summary>
    /// <remarks>
    /// Default operational status for variants receiving their allocated traffic
    /// percentage and contributing to experiment metrics and analysis.
    /// </remarks>
    Active = 0,

    /// <summary>
    /// Variant temporarily excluded from traffic routing but can be resumed.
    /// <value>1 - Variant paused but retains data and can be reactivated</value>
    /// </summary>
    /// <remarks>
    /// Temporary suspension that preserves all data and configuration.
    /// Can transition back to Active status to continue participation.
    /// </remarks>
    Paused = 1,

    /// <summary>
    /// Variant permanently removed from experiment with data preserved for analysis.
    /// <value>2 - Variant permanently excluded from ongoing experiment</value>
    /// </summary>
    /// <remarks>
    /// Permanent exclusion from traffic routing and data collection.
    /// Historical data preserved for post-experiment analysis only.
    /// </remarks>
    Stopped = 2,

    /// <summary>
    /// Variant identified as the best performer and potential experiment winner.
    /// <value>3 - Variant declared as winning configuration</value>
    /// </summary>
    /// <remarks>
    /// Designated as the optimal variant based on statistical analysis.
    /// May receive increased traffic allocation or be promoted for deployment.
    /// </remarks>
    Winner = 3,

    /// <summary>
    /// Variant identified as underperforming and excluded from further consideration.
    /// <value>4 - Variant marked as poor performer</value>
    /// </summary>
    /// <remarks>
    /// Statistically determined to be inferior to other variants.
    /// Excluded from traffic but data preserved for learning and analysis.
    /// </remarks>
    Loser = 4
}
