namespace PromptStudio.Core.DTOs.Flow;

/// <summary>
/// Enumeration of execution types for workflow categorization and filtering.
/// Defines the various ways a workflow execution can be initiated, enabling
/// proper categorization, monitoring, and analysis of execution patterns.
/// </summary>
/// <remarks>
/// <para><strong>Usage Context:</strong></para>
/// <para>This enumeration is used throughout the workflow execution system to categorize
/// how executions are initiated. This information is crucial for analytics, billing,
/// security auditing, and operational monitoring. Different execution types may have
/// different performance characteristics, security requirements, and billing models.</para>
/// 
/// <para><strong>Integration Points:</strong></para>
/// <list type="bullet">
/// <item>Workflow execution history tracking and reporting</item>
/// <item>Execution filtering and search operations</item>
/// <item>Performance analytics and optimization</item>
/// <item>Security auditing and compliance monitoring</item>
/// <item>Resource allocation and cost management</item>
/// </list>
/// 
/// <para><strong>Operational Considerations:</strong></para>
/// <para>Manual executions typically require user authentication and may have higher
/// priority. Scheduled executions run automatically and should be monitored for
/// reliability. Event-triggered executions require careful handling of event sources
/// and may need rate limiting. API and webhook executions require proper authentication
/// and authorization validation.</para>
/// </remarks>
public enum ExecutionType
{
    /// <summary>
    /// Manual execution initiated directly by a user through the user interface.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Requires authenticated user session</item>
    /// <item>Typically has real-time visibility requirements</item>
    /// <item>May have higher execution priority</item>
    /// <item>User expects immediate feedback and progress updates</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> Interactive workflow testing, one-off data processing,
    /// troubleshooting, development and debugging scenarios.</para>
    /// </remarks>
    Manual = 0,

    /// <summary>
    /// Scheduled execution triggered automatically by a timer or scheduled event.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Runs automatically without user intervention</item>
    /// <item>Follows predefined schedule patterns (cron, interval-based)</item>
    /// <item>Requires robust error handling and retry mechanisms</item>
    /// <item>Should have monitoring and alerting for failures</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> Regular data processing, automated reports,
    /// maintenance tasks, batch operations, recurring business processes.</para>
    /// </remarks>
    Scheduled = 1,

    /// <summary>
    /// Event-triggered execution initiated by an external event or system notification.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Responds to external system events or state changes</item>
    /// <item>May require event validation and filtering</item>
    /// <item>Execution timing depends on external event frequency</item>
    /// <item>Should handle event bursts and rate limiting</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> File upload processing, database change reactions,
    /// IoT sensor data processing, integration with external systems.</para>
    /// </remarks>
    EventTriggered = 2,

    /// <summary>
    /// API-triggered execution initiated through a REST API call or similar programmatic interface.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Initiated by external applications or services</item>
    /// <item>Requires API authentication and authorization</item>
    /// <item>Should return appropriate HTTP status codes and responses</item>
    /// <item>May need rate limiting and quota management</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> Integration with external applications,
    /// microservices orchestration, third-party system integration, mobile app backends.</para>
    /// </remarks>
    ApiTriggered = 3,

    /// <summary>
    /// Webhook-triggered execution initiated by an incoming webhook from an external service.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Initiated by external services via HTTP callbacks</item>
    /// <item>Requires webhook signature validation for security</item>
    /// <item>Should handle webhook retry attempts from external services</item>
    /// <item>May need duplicate detection and idempotency handling</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> GitHub repository events, payment processing
    /// notifications, CI/CD pipeline triggers, third-party service integrations.</para>
    /// </remarks>
    WebhookTriggered = 4,

    /// <summary>
    /// Batch execution as part of a larger batch operation or bulk processing scenario.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Part of larger bulk processing operations</item>
    /// <item>May have different performance and resource requirements</item>
    /// <item>Should support batch progress tracking and reporting</item>
    /// <item>May need special handling for batch failures and partial completion</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> Bulk data migration, mass email processing,
    /// large dataset transformations, ETL operations, reporting generation.</para>
    /// </remarks>
    Batch = 5,

    /// <summary>
    /// Test execution performed for testing, validation, or quality assurance purposes.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Used for workflow validation and testing scenarios</item>
    /// <item>May use test data or sandbox environments</item>
    /// <item>Should not affect production systems or data</item>
    /// <item>May have different logging and monitoring requirements</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> Workflow development and testing, quality assurance,
    /// performance testing, integration testing, user acceptance testing.</para>
    /// </remarks>
    Test = 6,

    /// <summary>
    /// Debug execution performed for troubleshooting, diagnostics, or development purposes.
    /// </summary>
    /// <remarks>
    /// <para><strong>Characteristics:</strong></para>
    /// <list type="bullet">
    /// <item>Used for troubleshooting and diagnostic purposes</item>
    /// <item>May have enhanced logging and debugging capabilities</item>
    /// <item>Should preserve detailed execution state and context</item>
    /// <item>May run with modified timeouts or resource limits</item>
    /// </list>
    /// <para><strong>Use Cases:</strong> Workflow debugging, issue reproduction,
    /// performance analysis, system diagnostics, development troubleshooting.</para>
    /// </remarks>
    Debug = 7
}
