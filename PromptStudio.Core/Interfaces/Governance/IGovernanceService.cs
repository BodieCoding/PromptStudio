using PromptStudio.Core.Domain.GovernanceEntities;
using PromptStudio.Core.DTOs.Common;

namespace PromptStudio.Core.Interfaces.Governance;

/// <summary>
/// Service interface for comprehensive governance, audit, and compliance management in enterprise LLMOps environments.
/// 
/// <para><strong>Service Architecture:</strong></para>
/// <para>Core governance service operating in the business service layer, responsible for audit trail management,
/// compliance monitoring, data classification, regulatory reporting, and governance policy enforcement.
/// Integrates with all domain services to provide comprehensive oversight and control for prompt engineering
/// operations, ensuring regulatory compliance and organizational governance standards.</para>
/// 
/// <para><strong>Implementation Contract:</strong></para>
/// <para>Implementations must maintain immutable audit trails, support real-time compliance monitoring,
/// enforce data classification policies, and provide comprehensive reporting capabilities for regulatory
/// requirements. All governance operations must be tamper-proof and support long-term retention policies.</para>
/// </summary>
/// <remarks>
/// <para><strong>Implementation Guidelines:</strong></para>
/// <list type="bullet">
/// <item><description>Implement immutable audit logs with cryptographic integrity protection</description></item>
/// <item><description>Support real-time compliance monitoring and violation detection</description></item>
/// <item><description>Maintain comprehensive data lineage and change tracking</description></item>
/// <item><description>Enforce retention policies and legal hold requirements</description></item>
/// <item><description>Provide automated compliance reporting and alerting capabilities</description></item>
/// </list>
/// 
/// <para><strong>Integration Patterns:</strong></para>
/// <list type="bullet">
/// <item><description>Integrates with all domain services for comprehensive audit coverage</description></item>
/// <item><description>Coordinates with security services for access control auditing</description></item>
/// <item><description>Connects to external compliance systems for regulatory reporting</description></item>
/// <item><description>Utilizes event sourcing patterns for complete change tracking</description></item>
/// <item><description>Implements CQRS for optimized read/write audit operations</description></item>
/// </list>
/// 
/// <para><strong>Compliance Standards:</strong></para>
/// <list type="bullet">
/// <item><description>SOX compliance for financial controls and audit trails</description></item>
/// <item><description>GDPR compliance for data protection and privacy requirements</description></item>
/// <item><description>SOC 2 compliance for security and availability controls</description></item>
/// <item><description>Industry-specific regulations (HIPAA, PCI-DSS, etc.)</description></item>
/// </list>
/// </remarks>
public interface IGovernanceService
{
    #region Audit Trail Management

    /// <summary>
    /// Records an audit event with comprehensive context and metadata for compliance tracking.
    /// Creates immutable audit trail entries with cryptographic integrity protection.
    /// </summary>
    /// <param name="entityType">Type of entity being audited (Lab, Library, Template, etc.)</param>
    /// <param name="entityId">Unique identifier of the audited entity</param>
    /// <param name="action">Specific action performed on the entity</param>
    /// <param name="userId">User identifier who performed the action</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="beforeState">Entity state before the change (for update operations)</param>
    /// <param name="afterState">Entity state after the change (for create/update operations)</param>
    /// <param name="additionalContext">Additional context and metadata for the audit event</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Audit entry with unique identifier and integrity hash</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
    /// <exception cref="AuditException">Thrown when audit recording fails</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>All audit entries are immutable once created</description></item>
    /// <item><description>Audit entries include cryptographic integrity protection</description></item>
    /// <item><description>Sensitive data is masked or encrypted in audit trails</description></item>
    /// <item><description>Audit entries support long-term retention and legal hold requirements</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements append-only audit storage for tamper resistance</description></item>
    /// <item><description>Uses event sourcing patterns for complete change tracking</description></item>
    /// <item><description>Supports real-time audit event streaming for monitoring</description></item>
    /// <item><description>Maintains separate audit database for security isolation</description></item>
    /// </list>
    /// 
    /// <para><strong>Usage Examples:</strong></para>
    /// <code>
    /// // Record a prompt template update audit event
    /// var auditEntry = await governanceService.RecordAuditEventAsync(
    ///     "PromptTemplate",
    ///     templateId,
    ///     "Update",
    ///     userId,
    ///     organizationId,
    ///     originalTemplate,
    ///     updatedTemplate,
    ///     new { ipAddress = "192.168.1.100", userAgent = "..." },
    ///     cancellationToken
    /// );
    /// </code>
    /// </remarks>
    Task<AuditEntry> RecordAuditEventAsync(
        string entityType,
        Guid entityId,
        string action,
        string userId,
        Guid organizationId,
        object? beforeState = null,
        object? afterState = null,
        object? additionalContext = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves audit trail for a specific entity with comprehensive filtering and pagination.
    /// Supports compliance reporting and investigation workflows.
    /// </summary>
    /// <param name="entityType">Type of entity to retrieve audit trail for</param>
    /// <param name="entityId">Unique identifier of the entity</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="dateRange">Optional date range filter for audit entries</param>
    /// <param name="actions">Optional filter by specific actions</param>
    /// <param name="userId">Optional filter by specific user</param>
    /// <param name="pageRequest">Pagination parameters for large audit trails</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Paginated audit trail with comprehensive event details</returns>
    /// <exception cref="ArgumentException">Thrown when entity parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks audit access permissions</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Audit access requires appropriate governance permissions</description></item>
    /// <item><description>Sensitive audit data may be masked based on user permissions</description></item>
    /// <item><description>Audit trails include complete change history and metadata</description></item>
    /// <item><description>Results are ordered chronologically for investigation workflows</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements efficient querying for large audit datasets</description></item>
    /// <item><description>Supports real-time audit trail updates and notifications</description></item>
    /// <item><description>Provides audit trail integrity verification capabilities</description></item>
    /// <item><description>Optimizes for compliance reporting and investigation scenarios</description></item>
    /// </list>
    /// </remarks>
    Task<PagedResult<AuditEntry>> GetAuditTrailAsync(
        string entityType,
        Guid entityId,
        Guid organizationId,
        DateRange? dateRange = null,
        IEnumerable<string>? actions = null,
        string? userId = null,
        PageRequest? pageRequest = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Performs comprehensive audit trail integrity verification for compliance assurance.
    /// Validates cryptographic hashes and detects potential tampering attempts.
    /// </summary>
    /// <param name="entityType">Type of entity to verify audit integrity for</param>
    /// <param name="entityId">Unique identifier of the entity</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Integrity verification result with detailed analysis</returns>
    /// <exception cref="ArgumentException">Thrown when entity parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks audit verification permissions</exception>
    /// <remarks>
    /// <para><strong>Verification Process:</strong></para>
    /// <list type="bullet">
    /// <item><description>Validates cryptographic integrity hashes for all audit entries</description></item>
    /// <item><description>Verifies chronological ordering and sequence integrity</description></item>
    /// <item><description>Detects missing or corrupted audit entries</description></item>
    /// <item><description>Validates audit entry format and required field completeness</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements blockchain-style integrity verification patterns</description></item>
    /// <item><description>Supports forensic analysis and evidence preservation</description></item>
    /// <item><description>Provides detailed verification reports for compliance auditors</description></item>
    /// <item><description>Maintains verification audit trail for investigation purposes</description></item>
    /// </list>
    /// </remarks>
    Task<AuditIntegrityVerificationResult> VerifyAuditIntegrityAsync(
        string entityType,
        Guid entityId,
        Guid organizationId,
        CancellationToken cancellationToken = default);

    #endregion

    #region Data Classification and Compliance

    /// <summary>
    /// Classifies data sensitivity and applies appropriate governance controls.
    /// Supports automated classification and manual override capabilities.
    /// </summary>
    /// <param name="entityType">Type of entity being classified</param>
    /// <param name="entityId">Unique identifier of the entity</param>
    /// <param name="content">Content to analyze for classification</param>
    /// <param name="organizationId">Organization context for classification policies</param>
    /// <param name="manualClassification">Optional manual classification override</param>
    /// <param name="classifiedBy">User identifier performing the classification</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Data classification result with applied controls and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are invalid</exception>
    /// <exception cref="ClassificationException">Thrown when data classification fails</exception>
    /// <remarks>
    /// <para><strong>Classification Levels:</strong></para>
    /// <list type="bullet">
    /// <item><description>Public: No restrictions, freely shareable</description></item>
    /// <item><description>Internal: Organization-wide access with basic controls</description></item>
    /// <item><description>Confidential: Restricted access with enhanced security</description></item>
    /// <item><description>Restricted: Highly sensitive with strict access controls</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements machine learning for automated content classification</description></item>
    /// <item><description>Supports regular expression and keyword-based classification rules</description></item>
    /// <item><description>Provides classification confidence scoring and review workflows</description></item>
    /// <item><description>Maintains classification audit trail and change history</description></item>
    /// </list>
    /// </remarks>
    Task<DataClassificationResult> ClassifyDataAsync(
        string entityType,
        Guid entityId,
        string content,
        Guid organizationId,
        DataClassification? manualClassification = null,
        string? classifiedBy = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves current data classification for an entity with applied governance controls.
    /// Includes classification history and control enforcement status.
    /// </summary>
    /// <param name="entityType">Type of entity to retrieve classification for</param>
    /// <param name="entityId">Unique identifier of the entity</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Current data classification with governance controls and history</returns>
    /// <exception cref="ArgumentException">Thrown when entity parameters are invalid</exception>
    /// <exception cref="EntityNotFoundException">Thrown when entity is not found</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Classifications are inherited hierarchically (Lab → Library → Template)</description></item>
    /// <item><description>Manual classifications override automated classifications</description></item>
    /// <item><description>Classification changes trigger governance control updates</description></item>
    /// <item><description>Historical classifications are preserved for audit purposes</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements caching for frequently accessed classifications</description></item>
    /// <item><description>Supports real-time classification updates and notifications</description></item>
    /// <item><description>Provides classification confidence and review status information</description></item>
    /// <item><description>Maintains complete classification change audit trail</description></item>
    /// </list>
    /// </remarks>
    Task<EntityDataClassification> GetDataClassificationAsync(
        string entityType,
        Guid entityId,
        Guid organizationId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates data classification for an entity with comprehensive audit tracking.
    /// Enforces classification policies and triggers governance control updates.
    /// </summary>
    /// <param name="entityType">Type of entity to update classification for</param>
    /// <param name="entityId">Unique identifier of the entity</param>
    /// <param name="newClassification">New data classification level</param>
    /// <param name="organizationId">Organization context for tenant isolation</param>
    /// <param name="updatedBy">User identifier performing the classification update</param>
    /// <param name="reason">Business justification for classification change</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Classification update result with applied controls and audit information</returns>
    /// <exception cref="ArgumentException">Thrown when parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks classification management permissions</exception>
    /// <exception cref="ClassificationException">Thrown when classification update violates policies</exception>
    /// <remarks>
    /// <para><strong>Business Rules:</strong></para>
    /// <list type="bullet">
    /// <item><description>Classification updates require appropriate governance permissions</description></item>
    /// <item><description>Classification downgrades may require approval workflows</description></item>
    /// <item><description>Classification changes trigger automatic governance control updates</description></item>
    /// <item><description>All classification changes create comprehensive audit trails</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements approval workflows for sensitive classification changes</description></item>
    /// <item><description>Supports notification systems for classification updates</description></item>
    /// <item><description>Maintains detailed audit logs for compliance requirements</description></item>
    /// <item><description>Validates against organizational classification policies</description></item>
    /// </list>
    /// </remarks>
    Task<ClassificationUpdateResult> UpdateDataClassificationAsync(
        string entityType,
        Guid entityId,
        DataClassification newClassification,
        Guid organizationId,
        string updatedBy,
        string reason,
        CancellationToken cancellationToken = default);

    #endregion

    #region Compliance Monitoring and Reporting

    /// <summary>
    /// Performs comprehensive compliance assessment for an organization against regulatory frameworks.
    /// Evaluates current state against compliance requirements and identifies violations.
    /// </summary>
    /// <param name="organizationId">Organization to assess compliance for</param>
    /// <param name="complianceFramework">Specific compliance framework to assess against</param>
    /// <param name="assessmentScope">Scope of assessment (all resources or specific subset)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive compliance assessment with findings and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when organization or framework parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks compliance assessment permissions</exception>
    /// <remarks>
    /// <para><strong>Compliance Frameworks:</strong></para>
    /// <list type="bullet">
    /// <item><description>SOX: Sarbanes-Oxley financial controls and audit requirements</description></item>
    /// <item><description>GDPR: General Data Protection Regulation privacy requirements</description></item>
    /// <item><description>SOC2: Service Organization Control 2 security and availability</description></item>
    /// <item><description>HIPAA: Health Insurance Portability and Accountability Act</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements automated compliance rule evaluation engines</description></item>
    /// <item><description>Supports custom compliance frameworks and requirements</description></item>
    /// <item><description>Provides risk scoring and prioritization for violations</description></item>
    /// <item><description>Maintains compliance assessment history and trend analysis</description></item>
    /// </list>
    /// </remarks>
    Task<ComplianceAssessmentResult> AssessComplianceAsync(
        Guid organizationId,
        ComplianceFramework complianceFramework,
        AssessmentScope? assessmentScope = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates comprehensive compliance report for regulatory submission and audit purposes.
    /// Includes executive summary, detailed findings, and remediation recommendations.
    /// </summary>
    /// <param name="organizationId">Organization to generate report for</param>
    /// <param name="complianceFramework">Compliance framework for report scope</param>
    /// <param name="reportPeriod">Time period covered by the compliance report</param>
    /// <param name="reportFormat">Desired output format for the compliance report</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Generated compliance report with comprehensive findings and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when report parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks compliance reporting permissions</exception>
    /// <remarks>
    /// <para><strong>Report Sections:</strong></para>
    /// <list type="bullet">
    /// <item><description>Executive summary with compliance posture overview</description></item>
    /// <item><description>Detailed compliance findings and violation analysis</description></item>
    /// <item><description>Risk assessment and impact analysis</description></item>
    /// <item><description>Remediation recommendations and action plans</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements templated reporting for consistent compliance documentation</description></item>
    /// <item><description>Supports multiple output formats (PDF, Word, Excel, JSON)</description></item>
    /// <item><description>Provides automated report scheduling and distribution</description></item>
    /// <item><description>Maintains report generation audit trail for compliance validation</description></item>
    /// </list>
    /// </remarks>
    Task<ComplianceReport> GenerateComplianceReportAsync(
        Guid organizationId,
        ComplianceFramework complianceFramework,
        DateRange reportPeriod,
        ReportFormat reportFormat = ReportFormat.PDF,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Monitors real-time compliance status and triggers alerts for violations.
    /// Provides continuous compliance monitoring and automated incident response.
    /// </summary>
    /// <param name="organizationId">Organization to monitor compliance for</param>
    /// <param name="monitoringRules">Specific compliance rules to monitor</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Real-time compliance monitoring status with active violations and alerts</returns>
    /// <exception cref="ArgumentException">Thrown when organization or monitoring parameters are invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when user lacks compliance monitoring permissions</exception>
    /// <remarks>
    /// <para><strong>Monitoring Capabilities:</strong></para>
    /// <list type="bullet">
    /// <item><description>Real-time violation detection and alerting</description></item>
    /// <item><description>Automated incident response and escalation workflows</description></item>
    /// <item><description>Compliance trend analysis and predictive monitoring</description></item>
    /// <item><description>Integration with external compliance management systems</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Implements event-driven compliance monitoring architecture</description></item>
    /// <item><description>Supports configurable monitoring rules and thresholds</description></item>
    /// <item><description>Provides real-time compliance dashboards and alerting</description></item>
    /// <item><description>Maintains comprehensive monitoring audit trail</description></item>
    /// </list>
    /// </remarks>
    Task<ComplianceMonitoringStatus> MonitorComplianceAsync(
        Guid organizationId,
        IEnumerable<ComplianceMonitoringRule>? monitoringRules = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region Governance Analytics

    /// <summary>
    /// Retrieves comprehensive governance analytics for organizational governance improvement.
    /// Provides insights into governance effectiveness, compliance trends, and optimization opportunities.
    /// </summary>
    /// <param name="organizationId">Organization context for analytics scope</param>
    /// <param name="dateRange">Time range for analytics data collection</param>
    /// <param name="analyticsScope">Scope of analytics (audit, compliance, classification, etc.)</param>
    /// <param name="cancellationToken">Cancellation token for async operation control</param>
    /// <returns>Comprehensive governance analytics with insights and recommendations</returns>
    /// <exception cref="ArgumentException">Thrown when organization ID or date range is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when requestor lacks governance analytics access</exception>
    /// <remarks>
    /// <para><strong>Analytics Categories:</strong></para>
    /// <list type="bullet">
    /// <item><description>Audit coverage and effectiveness analysis</description></item>
    /// <item><description>Compliance posture and trend analysis</description></item>
    /// <item><description>Data classification distribution and effectiveness</description></item>
    /// <item><description>Governance policy adherence and violation patterns</description></item>
    /// </list>
    /// 
    /// <para><strong>Implementation Notes:</strong></para>
    /// <list type="bullet">
    /// <item><description>Aggregates data from audit, compliance, and classification systems</description></item>
    /// <item><description>Implements machine learning for governance pattern analysis</description></item>
    /// <item><description>Provides predictive analytics for compliance risk assessment</description></item>
    /// <item><description>Supports governance dashboards and executive reporting</description></item>
    /// </list>
    /// </remarks>
    Task<GovernanceAnalytics> GetGovernanceAnalyticsAsync(
        Guid organizationId,
        DateRange dateRange,
        GovernanceAnalyticsScope? analyticsScope = null,
        CancellationToken cancellationToken = default);

    #endregion
}

/// <summary>
/// Represents an immutable audit entry with comprehensive context and integrity protection.
/// </summary>
public record AuditEntry(
    Guid Id,
    string EntityType,
    Guid EntityId,
    string Action,
    string UserId,
    Guid OrganizationId,
    DateTimeOffset Timestamp,
    object? BeforeState,
    object? AfterState,
    object? AdditionalContext,
    string IntegrityHash,
    string SequenceNumber);

/// <summary>
/// Result of audit trail integrity verification with detailed analysis.
/// </summary>
public record AuditIntegrityVerificationResult(
    bool IsValid,
    int TotalEntries,
    int VerifiedEntries,
    int CorruptedEntries,
    IEnumerable<AuditIntegrityViolation> Violations,
    DateTimeOffset VerificationTimestamp,
    string VerificationId);

/// <summary>
/// Represents an audit integrity violation requiring investigation.
/// </summary>
public record AuditIntegrityViolation(
    Guid AuditEntryId,
    string ViolationType,
    string Description,
    string Severity,
    string RecommendedAction);

/// <summary>
/// Result of data classification operation with applied controls.
/// </summary>
public record DataClassificationResult(
    DataClassification Classification,
    double ConfidenceScore,
    IEnumerable<string> DetectedPatterns,
    IEnumerable<GovernanceControl> AppliedControls,
    string ClassificationSource,
    DateTimeOffset ClassifiedAt);

/// <summary>
/// Current data classification for an entity with governance controls and history.
/// </summary>
public record EntityDataClassification(
    string EntityType,
    Guid EntityId,
    DataClassification CurrentClassification,
    IEnumerable<GovernanceControl> ActiveControls,
    IEnumerable<ClassificationHistoryEntry> ClassificationHistory,
    DateTimeOffset LastUpdated,
    string LastUpdatedBy);

/// <summary>
/// Historical data classification entry for audit and change tracking.
/// </summary>
public record ClassificationHistoryEntry(
    DataClassification Classification,
    DateTimeOffset ClassifiedAt,
    string ClassifiedBy,
    string Source,
    string? Reason);

/// <summary>
/// Result of data classification update operation with audit information.
/// </summary>
public record ClassificationUpdateResult(
    bool Success,
    string? ErrorMessage,
    DataClassification NewClassification,
    IEnumerable<GovernanceControl> UpdatedControls,
    string AuditId,
    DateTimeOffset UpdatedAt);

/// <summary>
/// Governance control applied based on data classification.
/// </summary>
public record GovernanceControl(
    string ControlType,
    string Description,
    DataClassification RequiredClassification,
    bool IsActive,
    string? Configuration);

/// <summary>
/// Result of comprehensive compliance assessment with findings and recommendations.
/// </summary>
public record ComplianceAssessmentResult(
    ComplianceFramework Framework,
    Guid OrganizationId,
    DateTimeOffset AssessmentDate,
    ComplianceStatus OverallStatus,
    double ComplianceScore,
    IEnumerable<ComplianceFinding> Findings,
    IEnumerable<ComplianceRecommendation> Recommendations,
    AssessmentMetadata Metadata);

/// <summary>
/// Individual compliance finding with severity and remediation guidance.
/// </summary>
public record ComplianceFinding(
    string RuleId,
    string RuleName,
    ComplianceSeverity Severity,
    string Description,
    IEnumerable<string> AffectedResources,
    string RecommendedAction,
    DateTimeOffset IdentifiedAt);

/// <summary>
/// Compliance recommendation for improving organizational compliance posture.
/// </summary>
public record ComplianceRecommendation(
    string Type,
    string Title,
    string Description,
    string Impact,
    string Priority,
    IEnumerable<string> ActionItems,
    string ExpectedOutcome);

/// <summary>
/// Comprehensive compliance report for regulatory submission and audit purposes.
/// </summary>
public record ComplianceReport(
    ComplianceFramework Framework,
    Guid OrganizationId,
    DateRange ReportPeriod,
    ReportFormat Format,
    ComplianceExecutiveSummary ExecutiveSummary,
    IEnumerable<ComplianceFinding> DetailedFindings,
    RiskAssessment RiskAssessment,
    IEnumerable<ComplianceRecommendation> RemediationPlan,
    byte[] ReportContent,
    DateTimeOffset GeneratedAt,
    string GeneratedBy);

/// <summary>
/// Executive summary for compliance report with high-level compliance posture.
/// </summary>
public record ComplianceExecutiveSummary(
    ComplianceStatus OverallStatus,
    double ComplianceScore,
    int TotalViolations,
    int HighSeverityViolations,
    int MediumSeverityViolations,
    int LowSeverityViolations,
    string KeyFindings,
    string RecommendedActions);

/// <summary>
/// Risk assessment for compliance violations and organizational impact.
/// </summary>
public record RiskAssessment(
    string OverallRiskLevel,
    IEnumerable<RiskFactor> RiskFactors,
    IEnumerable<RiskMitigationStrategy> MitigationStrategies,
    double RiskScore);

/// <summary>
/// Individual risk factor contributing to overall compliance risk.
/// </summary>
public record RiskFactor(
    string Name,
    string Description,
    string RiskLevel,
    double Impact,
    double Likelihood,
    string MitigationStatus);

/// <summary>
/// Risk mitigation strategy for addressing compliance risks.
/// </summary>
public record RiskMitigationStrategy(
    string Name,
    string Description,
    string Priority,
    IEnumerable<string> ActionItems,
    string ExpectedOutcome,
    DateTimeOffset TargetCompletionDate);

/// <summary>
/// Real-time compliance monitoring status with active violations and alerts.
/// </summary>
public record ComplianceMonitoringStatus(
    Guid OrganizationId,
    DateTimeOffset MonitoringTimestamp,
    ComplianceStatus CurrentStatus,
    IEnumerable<ActiveComplianceViolation> ActiveViolations,
    IEnumerable<ComplianceAlert> ActiveAlerts,
    ComplianceTrendAnalysis TrendAnalysis);

/// <summary>
/// Active compliance violation requiring immediate attention.
/// </summary>
public record ActiveComplianceViolation(
    string ViolationId,
    string RuleId,
    string RuleName,
    ComplianceSeverity Severity,
    string Description,
    IEnumerable<string> AffectedResources,
    DateTimeOffset DetectedAt,
    string Status,
    string? AssignedTo);

/// <summary>
/// Compliance alert for automated notification and escalation.
/// </summary>
public record ComplianceAlert(
    string AlertId,
    string AlertType,
    string Title,
    string Description,
    string Severity,
    DateTimeOffset TriggeredAt,
    string Status,
    IEnumerable<string> Recipients);

/// <summary>
/// Compliance trend analysis for predictive monitoring and risk assessment.
/// </summary>
public record ComplianceTrendAnalysis(
    double ComplianceScoreTrend,
    int ViolationCountTrend,
    IEnumerable<ComplianceTrendItem> TrendItems,
    IEnumerable<string> RiskIndicators,
    string TrendSummary);

/// <summary>
/// Individual compliance trend item for trend analysis.
/// </summary>
public record ComplianceTrendItem(
    DateTimeOffset Date,
    double ComplianceScore,
    int ViolationCount,
    ComplianceStatus Status);

/// <summary>
/// Compliance monitoring rule for automated violation detection.
/// </summary>
public record ComplianceMonitoringRule(
    string RuleId,
    string RuleName,
    ComplianceFramework Framework,
    string Description,
    string MonitoringExpression,
    ComplianceSeverity Severity,
    bool IsActive,
    IEnumerable<string> AlertRecipients);

/// <summary>
/// Comprehensive governance analytics for organizational governance improvement.
/// </summary>
public record GovernanceAnalytics(
    Guid OrganizationId,
    DateRange DateRange,
    AuditCoverageAnalytics AuditCoverage,
    CompliancePostureAnalytics CompliancePosture,
    DataClassificationAnalytics DataClassification,
    GovernancePolicyAnalytics PolicyAdherence,
    IEnumerable<GovernanceRecommendation> Recommendations,
    DateTimeOffset GeneratedAt);

/// <summary>
/// Analytics for audit coverage and effectiveness assessment.
/// </summary>
public record AuditCoverageAnalytics(
    int TotalAuditableEntities,
    int AuditedEntities,
    double AuditCoveragePercentage,
    double AverageAuditFrequency,
    IEnumerable<AuditGap> IdentifiedGaps,
    IEnumerable<AuditEffectivenessMetric> EffectivenessMetrics);

/// <summary>
/// Analytics for organizational compliance posture and trends.
/// </summary>
public record CompliancePostureAnalytics(
    double OverallComplianceScore,
    ComplianceStatus OverallStatus,
    IEnumerable<FrameworkComplianceScore> FrameworkScores,
    IEnumerable<ComplianceTrendItem> ComplianceTrends,
    IEnumerable<ComplianceViolationPattern> ViolationPatterns);

/// <summary>
/// Analytics for data classification distribution and effectiveness.
/// </summary>
public record DataClassificationAnalytics(
    IEnumerable<ClassificationDistribution> ClassificationDistribution,
    double AutoClassificationAccuracy,
    int ManualClassificationCount,
    IEnumerable<ClassificationEffectivenessMetric> EffectivenessMetrics,
    IEnumerable<DataClassificationGap> IdentifiedGaps);

/// <summary>
/// Analytics for governance policy adherence and violation patterns.
/// </summary>
public record GovernancePolicyAnalytics(
    double OverallPolicyAdherence,
    IEnumerable<PolicyAdherenceMetric> PolicyMetrics,
    IEnumerable<PolicyViolationPattern> ViolationPatterns,
    IEnumerable<PolicyEffectivenessMetric> PolicyEffectiveness);

/// <summary>
/// Governance recommendation for organizational improvement.
/// </summary>
public record GovernanceRecommendation(
    string Type,
    string Title,
    string Description,
    string Impact,
    string Priority,
    IEnumerable<string> ActionItems,
    string ExpectedBenefit);

/// <summary>
/// Enumeration of supported compliance frameworks.
/// </summary>
public enum ComplianceFramework
{
    SOX,
    GDPR,
    SOC2,
    HIPAA,
    PCIDSS,
    ISO27001,
    NIST,
    Custom
}

/// <summary>
/// Enumeration of compliance status levels.
/// </summary>
public enum ComplianceStatus
{
    Compliant,
    NonCompliant,
    PartiallyCompliant,
    Unknown,
    InProgress
}

/// <summary>
/// Enumeration of compliance violation severity levels.
/// </summary>
public enum ComplianceSeverity
{
    Low,
    Medium,
    High,
    Critical
}

/// <summary>
/// Enumeration of compliance report formats.
/// </summary>
public enum ReportFormat
{
    PDF,
    Word,
    Excel,
    JSON,
    HTML
}

/// <summary>
/// Enumeration of governance analytics scope options.
/// </summary>
public enum GovernanceAnalyticsScope
{
    All,
    Audit,
    Compliance,
    DataClassification,
    PolicyAdherence
}
