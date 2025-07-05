namespace PromptStudio.Core.Domain;

/// <summary>
/// Defines data classification levels for enterprise security, compliance, and governance requirements within the organization.
/// 
/// <para><strong>Business Context:</strong></para>
/// DataClassification enables organizations to implement appropriate security controls,
/// access restrictions, and handling procedures based on data sensitivity and business
/// impact. This supports regulatory compliance, risk management, and information security
/// governance while ensuring appropriate protection for different types of organizational data.
/// 
/// <para><strong>Technical Context:</strong></para>
/// The classification system drives access control policies, encryption requirements,
/// audit logging levels, and retention policies throughout the system architecture,
/// ensuring consistent data protection and compliance enforcement.
/// 
/// <para><strong>Value Proposition:</strong></para>
/// - Systematic data protection based on sensitivity and business impact
/// - Regulatory compliance through structured classification and controls
/// - Risk-based security controls and access management policies
/// - Clear handling guidelines for different data sensitivity levels
/// - Automated policy enforcement based on classification levels
/// </summary>
/// <remarks>
/// <para><strong>Classification Guidelines:</strong></para>
/// - Public: No restriction on disclosure, can be shared externally
/// - Internal: Standard business data, organizational access only
/// - Confidential: Sensitive data requiring controlled access
/// - Restricted: Highly sensitive data with minimal access requirements
/// 
/// <para><strong>Security Controls by Classification:</strong></para>
/// - Public: Basic integrity controls, no access restrictions
/// - Internal: Authentication required, basic audit logging
/// - Confidential: Role-based access, enhanced audit logging, encryption
/// - Restricted: Multi-factor authentication, comprehensive audit, encryption at rest/transit
/// 
/// <para><strong>Compliance Considerations:</strong></para>
/// - GDPR: Personal data typically Confidential or Restricted
/// - SOX: Financial data typically Confidential or Restricted
/// - HIPAA: Health information typically Restricted
/// - Industry standards: Follow sector-specific classification requirements
/// 
/// <para><strong>Integration Points:</strong></para>
/// - Access Control: Classification-based permission enforcement
/// - Encryption: Automatic encryption based on classification level
/// - Audit System: Enhanced logging for higher classification levels
/// - Data Loss Prevention: Classification-based monitoring and controls
/// </remarks>
/// <example>
/// <code>
/// // Apply appropriate classification during entity creation
/// var marketingTemplate = new PromptTemplate
/// {
///     Name = "Product Announcement Template",
///     Content = "Public marketing content...",
///     Classification = DataClassification.Public
/// };
/// 
/// var customerDataTemplate = new PromptTemplate
/// {
///     Name = "Customer Analysis Template", 
///     Content = "Template using customer PII...",
///     Classification = DataClassification.Confidential
/// };
/// 
/// var financialReportTemplate = new PromptTemplate
/// {
///     Name = "Financial Reporting Template",
///     Content = "SOX-regulated financial data...",
///     Classification = DataClassification.Restricted
/// };
/// 
/// // Enforce access based on classification
/// if (template.Classification >= DataClassification.Confidential)
/// {
///     await securityService.RequireMultiFactorAuth(user);
///     await auditService.LogSensitiveDataAccess(user, template);
/// }
/// </code>
/// </example>
public enum DataClassification
{
    /// <summary>
    /// Information approved for public disclosure with no access restrictions or security controls.
    /// <value>0 - Data that can be freely shared externally without business impact</value>
    /// </summary>
    /// <remarks>
    /// Examples: Marketing materials, public documentation, press releases.
    /// No encryption or access controls required, basic integrity protection only.
    /// </remarks>
    Public = 0,
    
    /// <summary>
    /// Standard business information accessible to organizational members with basic authentication.
    /// <value>1 - Internal business data requiring organizational access control</value>
    /// </summary>
    /// <remarks>
    /// Examples: Internal procedures, team communications, non-sensitive business data.
    /// Requires authentication and basic audit logging for access tracking.
    /// </remarks>
    Internal = 1,
    
    /// <summary>
    /// Sensitive business information requiring controlled access and enhanced security measures.
    /// <value>2 - Confidential data with role-based access and audit requirements</value>
    /// </summary>
    /// <remarks>
    /// Examples: Customer data, financial information, strategic plans, proprietary content.
    /// Requires role-based access control, encryption, and comprehensive audit logging.
    /// </remarks>
    Confidential = 2,
    
    /// <summary>
    /// Highly sensitive information with minimal access requirements and maximum security controls.
    /// <value>3 - Restricted data requiring exceptional security measures and minimal access</value>
    /// </summary>
    /// <remarks>
    /// Examples: Regulated financial data, personal health information, trade secrets.
    /// Requires multi-factor authentication, encryption at rest/transit, and comprehensive monitoring.
    /// </remarks>
    Restricted = 3
}
