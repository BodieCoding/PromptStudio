namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines the lifecycle status of an A/B testing experiment for proper experiment management and workflow control.
/// 
/// <para><strong>Business Context:</strong></para>
/// ABTestStatus provides clear experiment lifecycle management, enabling teams to track 
/// experiment progress from initial design through completion and analysis. This ensures 
/// proper experiment governance, prevents premature conclusions, and maintains data integrity 
/// throughout the testing process.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The status enum drives workflow automation, access control, and data collection rules
/// within the A/B testing framework. Status transitions trigger appropriate system behaviors
/// such as traffic routing, metric collection, and result analysis.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Clear experiment lifecycle management and governance
/// - Automated workflow control and status-based processing
/// - Data integrity protection during critical experiment phases
/// - Audit trail for experiment progression and decision points
/// - Compliance support for regulated testing environments
/// </summary>
/// <remarks>
/// <para><strong>Status Transitions:</strong></para>
/// Draft → Ready → Running → (Paused ↔ Running) → Completed/Cancelled → Archived
/// 
/// <para><strong>Access Control:</strong></para>
/// - Draft: Full edit access for experiment design
/// - Ready: Configuration locked, awaiting approval/start
/// - Running: Data collection active, configuration immutable
/// - Paused: Temporary suspension, data preserved
/// - Completed: Read-only, analysis and reporting available
/// - Cancelled: Terminated early, partial data analysis
/// - Archived: Historical storage, minimal access
/// 
/// <para><strong>System Behaviors:</strong></para>
/// - Running: Traffic routing active, metrics collection enabled
/// - Paused: Traffic routing disabled, data preserved
/// - Completed: Statistical analysis triggered, results available
/// - Cancelled: Cleanup procedures, partial analysis only
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Traffic Router: Controls variant selection based on status
/// - Analytics Engine: Enables/disables metric collection
/// - Notification System: Status change alerts and updates
/// - Reporting Dashboard: Status-appropriate data visualization
/// </remarks>
/// <example>
/// <code>
/// // Check if test is actively collecting data
/// if (abTest.Status == ABTestStatus.Running)
/// {
///     // Route traffic to variants and collect metrics
///     var selectedVariant = trafficRouter.SelectVariant(abTest);
///     await metricsCollector.RecordExecution(selectedVariant, result);
/// }
/// 
/// // Transition to completed status when target reached
/// if (abTest.CurrentSampleSize >= abTest.TargetSampleSize)
/// {
///     abTest.Status = ABTestStatus.Completed;
///     await statisticalAnalyzer.GenerateResults(abTest);
/// }
/// </code>
/// </example>
public enum ABTestStatus
{
    /// <summary>
    /// Initial status for experiment design and configuration setup.
    /// <value>0 - Experiment is being designed and configured</value>
    /// </summary>
    /// <remarks>
    /// In Draft status, experiments are fully editable and not yet validated.
    /// No traffic routing or data collection occurs in this state.
    /// </remarks>
    Draft = 0,

    /// <summary>
    /// Experiment configuration is complete and validated, ready for execution.
    /// <value>1 - Experiment is validated and ready to start</value>
    /// </summary>
    /// <remarks>
    /// Configuration becomes immutable once moved to Ready status.
    /// Awaiting final approval or scheduled start time.
    /// </remarks>
    Ready = 1,

    /// <summary>
    /// Experiment is actively running with traffic routing and data collection enabled.
    /// <value>2 - Experiment is live and collecting data</value>
    /// </summary>
    /// <remarks>
    /// Primary operational status where variants receive traffic allocation
    /// and performance metrics are actively collected and analyzed.
    /// </remarks>
    Running = 2,

    /// <summary>
    /// Experiment temporarily suspended while preserving collected data and configuration.
    /// <value>3 - Experiment is paused but can be resumed</value>
    /// </summary>
    /// <remarks>
    /// Traffic routing is disabled but data and configuration remain intact.
    /// Can transition back to Running status to continue the experiment.
    /// </remarks>
    Paused = 3,

    /// <summary>
    /// Experiment has reached completion criteria with final results available.
    /// <value>4 - Experiment completed successfully with results</value>
    /// </summary>
    /// <remarks>
    /// Statistical analysis is complete and results are available.
    /// Configuration and data are preserved for historical analysis.
    /// </remarks>
    Completed = 4,

    /// <summary>
    /// Experiment was terminated before completion, with partial data available.
    /// <value>5 - Experiment was cancelled before completion</value>
    /// </summary>
    /// <remarks>
    /// Early termination due to business decisions, technical issues, or other factors.
    /// Partial analysis may be available depending on data collected.
    /// </remarks>
    Cancelled = 5,

    /// <summary>
    /// Experiment moved to long-term storage with limited access for historical reference.
    /// <value>6 - Experiment archived for historical reference</value>
    /// </summary>
    /// <remarks>
    /// Used for long-term storage and compliance requirements.
    /// Typically read-only access with potential for data archival or cleanup.
    /// </remarks>
    Archived = 6
}
