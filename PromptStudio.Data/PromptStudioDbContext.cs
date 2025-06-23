using Microsoft.EntityFrameworkCore;
using PromptStudio.Core.Domain;
using PromptStudio.Core.Interfaces;

namespace PromptStudio.Data;

/// <summary>
/// Database context for the PromptStudio application
/// </summary>
public class PromptStudioDbContext : DbContext, IPromptStudioDbContext
{
    /// <summary>
    /// Initializes a new instance of the PromptStudioDbContext
    /// </summary>
    /// <param name="options">The options to be used by the context</param>
    public PromptStudioDbContext(DbContextOptions<PromptStudioDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the collections in the database
    /// </summary>
    public DbSet<Collection> Collections { get; set; }

    /// <summary>
    /// Gets or sets the prompt templates in the database
    /// </summary>
    public DbSet<PromptTemplate> PromptTemplates { get; set; }

    /// <summary>
    /// Gets or sets the prompt variables in the database
    /// </summary>
    public DbSet<PromptVariable> PromptVariables { get; set; }

    /// <summary>
    /// Gets or sets the prompt executions in the database
    /// </summary>
    public DbSet<PromptExecution> PromptExecutions { get; set; }    /// <summary>
    /// Gets or sets the variable collections in the database
    /// </summary>
    public DbSet<VariableCollection> VariableCollections { get; set; }    /// <summary>
    /// Gets or sets the prompt flows in the database
    /// </summary>
    public DbSet<PromptFlow> PromptFlows { get; set; }

    /// <summary>
    /// Gets or sets the flow executions in the database
    /// </summary>
    public DbSet<FlowExecution> FlowExecutions { get; set; }

    /// <summary>
    /// Configures the database model
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Collection configuration
        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => e.Name);
        });

        // PromptTemplate configuration
        modelBuilder.Entity<PromptTemplate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Content).IsRequired();

            entity.HasOne(e => e.Collection)
                  .WithMany(c => c.PromptTemplates)
                  .HasForeignKey(e => e.CollectionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.CollectionId, e.Name });
        });

        // PromptVariable configuration
        modelBuilder.Entity<PromptVariable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Type).HasConversion<string>();

            entity.HasOne(e => e.PromptTemplate)
                  .WithMany(pt => pt.Variables)
                  .HasForeignKey(e => e.PromptTemplateId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => new { e.PromptTemplateId, e.Name }).IsUnique();
        });

        // PromptExecution configuration
        modelBuilder.Entity<PromptExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ResolvedPrompt).IsRequired();
            entity.Property(e => e.AiProvider).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Cost).HasColumnType("decimal(10,4)");

            entity.HasOne(e => e.PromptTemplate)
                  .WithMany(pt => pt.Executions)
                  .HasForeignKey(e => e.PromptTemplateId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.ExecutedAt);
            entity.HasIndex(e => e.PromptTemplateId);
        });

        // VariableCollection configuration
        modelBuilder.Entity<VariableCollection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.VariableSets).IsRequired();

            entity.HasOne(e => e.PromptTemplate)
                  .WithMany(pt => pt.VariableCollections)
                  .HasForeignKey(e => e.PromptTemplateId)
                  .OnDelete(DeleteBehavior.Cascade);            entity.HasIndex(e => e.PromptTemplateId);
            entity.HasIndex(e => e.Name);
        });        // PromptFlow configuration
        modelBuilder.Entity<PromptFlow>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Version).IsRequired().HasMaxLength(20);
            entity.Property(e => e.UserId).HasMaxLength(100);
            entity.Property(e => e.Tags).HasMaxLength(1000);
            entity.Property(e => e.FlowData).IsRequired();

            entity.HasIndex(e => e.Name);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.CreatedAt);
        });

        // FlowExecution configuration
        modelBuilder.Entity<FlowExecution>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InputVariables).IsRequired();
            entity.Property(e => e.OutputResult);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.ErrorMessage).HasMaxLength(2000);

            entity.HasOne(e => e.Flow)
                  .WithMany()
                  .HasForeignKey(e => e.FlowId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.FlowId);
            entity.HasIndex(e => e.CreatedAt);
            entity.HasIndex(e => e.Status);
        });

        // Seed initial data
        SeedData(modelBuilder);
    }    /// <summary>
    /// Seeds the database with initial data
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model</param>
    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed collections
        modelBuilder.Entity<Collection>().HasData(
            new Collection
            {
                Id = 1,
                Name = "Sample Collection",
                Description = "A sample collection to get you started",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Collection
            {
                Id = 1001,
                Name = "AI Agent Workflows",
                Description = "Advanced prompt templates designed for AI agents to automate complex multi-step workflows, code analysis, and problem-solving tasks",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed prompt templates
        modelBuilder.Entity<PromptTemplate>().HasData(
            // Sample Collection Prompt
            new PromptTemplate
            {
                Id = 1,
                Name = "Code Review",
                Description = "Review code for best practices and improvements",
                Content = "Please review the following {{language}} code and provide feedback:\n\n```{{language}}\n{{code}}\n```\n\nFocus on:\n- Code quality\n- Performance\n- Security\n- Best practices",
                CollectionId = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            
            // AI Agent Workflows Prompts
            new PromptTemplate
            {
                Id = 1001,
                Name = "Advanced Code Analysis & Refactoring",
                Description = "Comprehensive code analysis and refactoring recommendations with business context",
                Content = @"# Advanced Code Analysis & Refactoring

## Context
You are analyzing {{code_type}} code from {{project_name}} for {{analysis_purpose}}.

## Code to Analyze
```{{language}}
{{source_code}}
```

## Analysis Requirements
- **Primary Focus**: {{primary_focus}}
- **Secondary Concerns**: {{secondary_concerns}}
- **Performance Requirements**: {{performance_requirements}}
- **Code Standards**: {{coding_standards}}
- **Target Audience**: {{target_audience}}

## Specific Analysis Tasks

### 1. Code Quality Assessment
Evaluate the code for:
- Readability and maintainability
- Performance bottlenecks
- Security vulnerabilities
- Design pattern adherence
- Error handling robustness

### 2. Architecture Review
Analyze:
- Component separation and cohesion
- Dependency management
- Scalability considerations
- Testing coverage gaps
- Documentation completeness

### 3. Refactoring Recommendations
Provide:
- Specific refactoring steps with before/after examples
- Estimated impact on {{business_metrics}}
- Risk assessment for each change
- Implementation priority ranking
- Resource requirements

### 4. Alternative Approaches
Suggest:
- Modern framework alternatives for {{legacy_components}}
- Performance optimization techniques
- Best practices for {{specific_domain}}
- Integration improvements with {{external_systems}}

## Deliverables
1. **Executive Summary**: {{executive_focus}} appropriate insights
2. **Technical Details**: Implementation-ready recommendations
3. **Action Plan**: Prioritized steps with {{timeline_constraints}}
4. **Risk Mitigation**: Strategies for {{deployment_environment}}

## Success Metrics
Measure improvements in:
{{success_metrics}}

## Constraints
- Budget: {{budget_constraints}}
- Timeline: {{timeline_constraints}}
- Team Expertise: {{team_capabilities}}
- Business Requirements: {{business_constraints}}",
                CollectionId = 1001,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            new PromptTemplate
            {
                Id = 1002,
                Name = "AI-Powered Debugging & Problem Solving",
                Description = "Systematic debugging and problem-solving framework for complex technical issues",
                Content = @"# AI-Powered Debugging & Problem Solving Framework

## Issue Context
- **System**: {{system_name}} ({{system_type}})
- **Environment**: {{environment_type}}
- **Issue Description**: {{issue_description}}
- **Severity Level**: {{severity_level}}
- **Business Impact**: {{business_impact}}

## Problem Analysis

### 1. Initial Assessment
**Symptoms Observed:**
{{symptoms_observed}}

**Error Messages/Logs:**
```
{{error_logs}}
```

**Reproduction Steps:**
{{reproduction_steps}}

**Environmental Factors:**
- Operating System: {{operating_system}}
- Runtime Version: {{runtime_version}}
- Dependencies: {{dependencies_list}}
- Configuration: {{configuration_details}}

### 2. Systematic Investigation

#### Data Collection
Gather information about:
- {{data_sources}}
- Performance metrics during {{timeframe_analysis}}
- User behavior patterns: {{user_patterns}}
- System load: {{system_load_info}}

#### Root Cause Analysis
Apply debugging methodology:
1. **Hypothesis Formation**: Based on {{initial_hypothesis}}
2. **Evidence Gathering**: From {{evidence_sources}}
3. **Pattern Recognition**: Looking for {{pattern_indicators}}
4. **Correlation Analysis**: Between {{correlation_factors}}

### 3. Solution Development

#### Immediate Actions
**Quick Fixes** (Timeline: {{immediate_timeline}}):
{{immediate_actions}}

**Mitigation Strategies**:
{{mitigation_strategies}}

#### Long-term Solutions
**Architectural Improvements**:
{{architectural_improvements}}

**Process Enhancements**:
{{process_enhancements}}

**Monitoring & Alerting**:
{{monitoring_setup}}

### 4. Implementation Plan

#### Phase 1: Stabilization
- Priority: {{phase1_priority}}
- Resources: {{phase1_resources}}
- Timeline: {{phase1_timeline}}
- Success Criteria: {{phase1_success}}

#### Phase 2: Optimization
- Scope: {{phase2_scope}}
- Dependencies: {{phase2_dependencies}}
- Risk Assessment: {{phase2_risks}}

#### Phase 3: Prevention
- Automation: {{automation_requirements}}
- Documentation: {{documentation_needs}}
- Training: {{training_requirements}}

### 5. Validation & Testing

**Test Strategy**:
{{test_strategy}}

**Validation Criteria**:
{{validation_criteria}}

**Rollback Plan**:
{{rollback_plan}}

## Monitoring & Follow-up
- **Key Metrics**: {{success_metrics}}
- **Alert Thresholds**: {{alert_thresholds}}
- **Review Schedule**: {{review_schedule}}
- **Stakeholder Communication**: {{communication_plan}}

## Learning & Documentation
**Post-Mortem Analysis**:
{{postmortem_requirements}}

**Knowledge Base Updates**:
{{knowledge_base_updates}}

**Team Training Needs**:
{{training_needs}}",
                CollectionId = 1001,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            new PromptTemplate
            {
                Id = 1003,
                Name = "Multi-Modal Creative Content Generator",
                Description = "Comprehensive creative content generation framework for multi-platform campaigns",
                Content = @"# Multi-Modal Creative Content Generator

## Campaign Overview
- **Brand**: {{brand_name}}
- **Campaign Name**: {{campaign_name}}
- **Campaign Objective**: {{campaign_objective}}
- **Target Audience**: {{target_audience}}
- **Campaign Duration**: {{campaign_duration}}
- **Budget**: {{campaign_budget}}

## Brand Guidelines
**Brand Voice & Tone:**
{{brand_voice}}

**Visual Identity:**
{{visual_identity}}

**Key Messages:**
{{key_messages}}

**Brand Values:**
{{brand_values}}

## Content Strategy

### 1. Platform-Specific Content

#### Social Media Platforms
**Instagram**:
- Content Type: {{instagram_content_type}}
- Post Frequency: {{instagram_frequency}}
- Hashtag Strategy: {{instagram_hashtags}}
- Visual Style: {{instagram_visual_style}}

**LinkedIn**:
- Content Focus: {{linkedin_focus}}
- Tone: {{linkedin_tone}}
- Format Preferences: {{linkedin_formats}}

**Twitter/X**:
- Content Style: {{twitter_style}}
- Engagement Strategy: {{twitter_engagement}}
- Trending Topics: {{twitter_trends}}

**TikTok**:
- Content Themes: {{tiktok_themes}}
- Music/Audio: {{tiktok_audio}}
- Trends Integration: {{tiktok_trends}}

**YouTube**:
- Video Types: {{youtube_types}}
- SEO Strategy: {{youtube_seo}}
- Thumbnail Strategy: {{youtube_thumbnails}}

#### Traditional Media
**Email Marketing**:
- Subject Line Strategy: {{email_subjects}}
- Content Structure: {{email_structure}}
- CTA Strategy: {{email_cta}}

**Blog Content**:
- Topics: {{blog_topics}}
- SEO Keywords: {{seo_keywords}}
- Content Calendar: {{content_calendar}}

**Print Materials**:
- Format Requirements: {{print_formats}}
- Distribution Channels: {{print_distribution}}

### 2. Content Creation Framework

#### Text Content
**Headlines & Copy**:
Generate compelling headlines for {{headline_context}} that:
- Capture attention within {{attention_timeframe}}
- Convey {{key_benefits}}
- Include power words: {{power_words}}
- Optimize for {{optimization_goals}}

**Body Content**:
Create engaging content that:
- Addresses {{audience_pain_points}}
- Provides value through {{value_proposition}}
- Includes {{social_proof_elements}}
- Drives action toward {{desired_actions}}

#### Visual Content
**Image Concepts**:
- Style: {{image_style}}
- Color Palette: {{color_palette}}
- Composition: {{composition_style}}
- Subject Matter: {{image_subjects}}

**Video Concepts**:
- Format: {{video_format}}
- Duration: {{video_duration}}
- Narrative Structure: {{video_narrative}}
- Visual Effects: {{video_effects}}

#### Audio Content
**Podcast/Audio**:
- Tone: {{audio_tone}}
- Background Music: {{background_music}}
- Voice Style: {{voice_style}}
- Sound Effects: {{sound_effects}}

### 3. Personalization & Targeting

#### Audience Segmentation
**Primary Segments**:
{{audience_segments}}

**Personalization Variables**:
{{personalization_vars}}

**Behavioral Triggers**:
{{behavioral_triggers}}

#### Dynamic Content
**A/B Testing Framework**:
Test variations of:
- {{ab_test_elements}}
- Metrics to track: {{ab_test_metrics}}
- Statistical significance: {{significance_threshold}}

### 4. Content Calendar & Scheduling

#### Content Planning
**Weekly Themes**:
{{weekly_themes}}

**Seasonal Considerations**:
{{seasonal_factors}}

**Event-Based Content**:
{{event_content}}

#### Publishing Schedule
**Optimal Timing**:
{{posting_schedule}}

**Frequency by Platform**:
{{platform_frequency}}

**Cross-Platform Coordination**:
{{cross_platform_strategy}}

### 5. Performance Optimization

#### Success Metrics
**Engagement Metrics**:
{{engagement_metrics}}

**Conversion Metrics**:
{{conversion_metrics}}

**Brand Awareness Metrics**:
{{awareness_metrics}}

#### Optimization Strategy
**Content Performance Analysis**:
{{performance_analysis}}

**Iterative Improvements**:
{{improvement_strategy}}

**Trend Integration**:
{{trend_integration}}

### 6. Compliance & Guidelines

#### Legal Considerations
**Copyright Requirements**:
{{copyright_requirements}}

**Disclosure Requirements**:
{{disclosure_requirements}}

**Platform Guidelines**:
{{platform_guidelines}}

#### Quality Assurance
**Review Process**:
{{review_process}}

**Approval Workflow**:
{{approval_workflow}}

**Brand Consistency Check**:
{{consistency_check}}

## Deliverables
1. **Content Assets**: {{deliverable_assets}}
2. **Style Guide**: {{style_guide_elements}}
3. **Content Calendar**: {{calendar_timeframe}}
4. **Performance Dashboard**: {{dashboard_metrics}}
5. **Optimization Recommendations**: {{optimization_recommendations}}",
                CollectionId = 1001,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            new PromptTemplate
            {
                Id = 1004,
                Name = "AI Agent Workflow Orchestration",
                Description = "Advanced framework for orchestrating complex AI agent workflows with error handling, monitoring, and optimization",
                Content = @"# AI Agent Workflow Orchestration Framework

## Workflow Definition
- **Workflow Name**: {{workflow_name}}
- **Business Objective**: {{business_objective}}
- **Success Criteria**: {{success_criteria}}
- **SLA Requirements**: {{sla_requirements}}
- **Compliance Requirements**: {{compliance_requirements}}

## Agent Architecture

### 1. Agent Configuration
**Primary Agents:**
{{primary_agents}}

**Supporting Agents:**
{{supporting_agents}}

**Agent Capabilities:**
{{agent_capabilities}}

**Resource Requirements:**
{{resource_requirements}}

### 2. Communication Protocols
**Inter-Agent Communication:**
- Protocol: {{communication_protocol}}
- Message Format: {{message_format}}
- Authentication: {{auth_mechanism}}
- Rate Limiting: {{rate_limits}}

**External Integrations:**
{{external_integrations}}

## Workflow Design

### 3. Process Flow Definition

#### Step 1: Initialization
**Trigger Conditions:**
{{trigger_conditions}}

**Input Validation:**
{{input_validation}}

**Resource Allocation:**
{{resource_allocation}}

**Logging Setup:**
{{logging_setup}}

#### Step 2: Task Distribution
**Task Assignment Strategy:**
{{task_assignment}}

**Load Balancing:**
{{load_balancing}}

**Priority Handling:**
{{priority_handling}}

**Queue Management:**
{{queue_management}}

#### Step 3: Execution Monitoring
**Real-time Monitoring:**
{{monitoring_setup}}

**Performance Metrics:**
{{performance_metrics}}

**Health Checks:**
{{health_checks}}

**Alert Conditions:**
{{alert_conditions}}

#### Step 4: Error Handling
**Error Classification:**
{{error_classification}}

**Recovery Strategies:**
{{recovery_strategies}}

**Escalation Procedures:**
{{escalation_procedures}}

**Fallback Mechanisms:**
{{fallback_mechanisms}}

#### Step 5: Completion & Cleanup
**Success Validation:**
{{success_validation}}

**Data Persistence:**
{{data_persistence}}

**Resource Cleanup:**
{{resource_cleanup}}

**Notification System:**
{{notification_system}}

### 4. Advanced Features

#### Dynamic Scaling
**Auto-scaling Triggers:**
{{scaling_triggers}}

**Resource Provisioning:**
{{resource_provisioning}}

**Performance Optimization:**
{{performance_optimization}}

**Cost Management:**
{{cost_management}}

#### Workflow Branching
**Conditional Logic:**
{{conditional_logic}}

**Parallel Processing:**
{{parallel_processing}}

**Synchronization Points:**
{{sync_points}}

**Merge Strategies:**
{{merge_strategies}}

#### State Management
**State Persistence:**
{{state_persistence}}

**Checkpoint Strategy:**
{{checkpoint_strategy}}

**Recovery Points:**
{{recovery_points}}

**State Validation:**
{{state_validation}}

### 5. Quality Assurance

#### Testing Framework
**Unit Testing:**
{{unit_testing}}

**Integration Testing:**
{{integration_testing}}

**Performance Testing:**
{{performance_testing}}

**Chaos Engineering:**
{{chaos_engineering}}

#### Validation Pipeline
**Input Validation:**
{{input_validation_rules}}

**Output Verification:**
{{output_verification}}

**Business Rule Validation:**
{{business_rule_validation}}

**Compliance Checking:**
{{compliance_checking}}

### 6. Security & Governance

#### Security Framework
**Authentication & Authorization:**
{{auth_framework}}

**Data Encryption:**
{{encryption_standards}}

**Audit Logging:**
{{audit_logging}}

**Access Control:**
{{access_control}}

#### Governance
**Approval Workflows:**
{{approval_workflows}}

**Change Management:**
{{change_management}}

**Version Control:**
{{version_control}}

**Documentation Standards:**
{{documentation_standards}}

### 7. Monitoring & Analytics

#### Performance Dashboard
**Key Metrics:**
{{dashboard_metrics}}

**Real-time Indicators:**
{{realtime_indicators}}

**Historical Analysis:**
{{historical_analysis}}

**Predictive Analytics:**
{{predictive_analytics}}

#### Alerting System
**Alert Types:**
{{alert_types}}

**Escalation Matrix:**
{{escalation_matrix}}

**Response Procedures:**
{{response_procedures}}

**Communication Channels:**
{{communication_channels}}

### 8. Optimization & Tuning

#### Performance Optimization
**Bottleneck Identification:**
{{bottleneck_identification}}

**Resource Optimization:**
{{resource_optimization}}

**Algorithm Tuning:**
{{algorithm_tuning}}

**Caching Strategy:**
{{caching_strategy}}

#### Continuous Improvement
**Feedback Loops:**
{{feedback_loops}}

**Learning Mechanisms:**
{{learning_mechanisms}}

**Adaptation Strategies:**
{{adaptation_strategies}}

**Version Updates:**
{{version_updates}}

### 9. Deployment & Operations

#### Deployment Strategy
**Environment Management:**
{{environment_management}}

**Blue-Green Deployment:**
{{bluegreen_strategy}}

**Rollback Procedures:**
{{rollback_procedures}}

**Migration Planning:**
{{migration_planning}}

#### Operational Procedures
**Daily Operations:**
{{daily_operations}}

**Maintenance Windows:**
{{maintenance_windows}}

**Backup & Recovery:**
{{backup_recovery}}

**Disaster Recovery:**
{{disaster_recovery}}

### 10. Documentation & Training

#### Documentation Requirements
**Technical Documentation:**
{{technical_docs}}

**User Guides:**
{{user_guides}}

**Operational Runbooks:**
{{operational_runbooks}}

**API Documentation:**
{{api_documentation}}

#### Training Program
**Operator Training:**
{{operator_training}}

**Developer Training:**
{{developer_training}}

**Business User Training:**
{{business_training}}

**Certification Requirements:**
{{certification_requirements}}

## Implementation Timeline
**Phase 1**: {{phase1_scope}} ({{phase1_timeline}})
**Phase 2**: {{phase2_scope}} ({{phase2_timeline}})
**Phase 3**: {{phase3_scope}} ({{phase3_timeline}})

## Success Metrics & KPIs
**Business Metrics:**
{{business_metrics}}

**Technical Metrics:**
{{technical_metrics}}

**User Experience Metrics:**
{{ux_metrics}}

**Cost Efficiency Metrics:**
{{cost_metrics}}",
                CollectionId = 1001,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },

            new PromptTemplate
            {
                Id = 1005,
                Name = "Intelligent Test Generation & QA Automation",
                Description = "Comprehensive testing strategy development with CI/CD integration and quality metrics",
                Content = @"# Intelligent Test Generation & QA Automation Framework

## Project Context
- **Application**: {{application_name}}
- **Technology Stack**: {{technology_stack}}
- **Testing Scope**: {{testing_scope}}
- **Quality Standards**: {{quality_standards}}
- **Regulatory Requirements**: {{regulatory_requirements}}

## Testing Strategy

### 1. Test Planning & Analysis

#### Requirements Analysis
**Functional Requirements:**
{{functional_requirements}}

**Non-Functional Requirements:**
{{nonfunctional_requirements}}

**Business Rules:**
{{business_rules}}

**User Stories/Use Cases:**
{{user_stories}}

#### Risk Assessment
**High-Risk Areas:**
{{high_risk_areas}}

**Critical Paths:**
{{critical_paths}}

**Security Vulnerabilities:**
{{security_risks}}

**Performance Bottlenecks:**
{{performance_risks}}

### 2. Test Design Framework

#### Test Categories
**Unit Testing:**
- Coverage Target: {{unit_coverage_target}}
- Frameworks: {{unit_frameworks}}
- Mock Strategy: {{mock_strategy}}
- Test Data: {{unit_test_data}}

**Integration Testing:**
- Integration Points: {{integration_points}}
- Test Environment: {{integration_environment}}
- Data Dependencies: {{integration_dependencies}}
- External Services: {{external_services}}

**System Testing:**
- Test Scenarios: {{system_scenarios}}
- Environment Requirements: {{system_environment}}
- Performance Criteria: {{performance_criteria}}
- Security Testing: {{security_testing}}

**User Acceptance Testing:**
- Acceptance Criteria: {{acceptance_criteria}}
- User Personas: {{user_personas}}
- Business Scenarios: {{business_scenarios}}
- Sign-off Requirements: {{signoff_requirements}}

#### Test Data Management
**Data Generation Strategy:**
{{data_generation}}

**Data Privacy Compliance:**
{{data_privacy}}

**Test Data Refresh:**
{{data_refresh}}

**Data Masking Requirements:**
{{data_masking}}

### 3. Automated Testing Implementation

#### Test Automation Architecture
**Framework Selection:**
{{automation_framework}}

**Tool Stack:**
{{automation_tools}}

**Infrastructure Requirements:**
{{automation_infrastructure}}

**Maintenance Strategy:**
{{automation_maintenance}}

#### Test Script Generation
**Page Object Patterns:**
{{page_objects}}

**Test Case Patterns:**
{{test_patterns}}

**Assertion Strategies:**
{{assertion_strategies}}

**Error Handling:**
{{error_handling}}

#### API Testing Automation
**Endpoint Coverage:**
{{api_endpoints}}

**Authentication Testing:**
{{api_auth}}

**Data Validation:**
{{api_validation}}

**Performance Testing:**
{{api_performance}}

### 4. Performance Testing Strategy

#### Performance Requirements
**Response Time Targets:**
{{response_targets}}

**Throughput Requirements:**
{{throughput_requirements}}

**Scalability Targets:**
{{scalability_targets}}

**Resource Utilization:**
{{resource_utilization}}

#### Load Testing Design
**Test Scenarios:**
{{load_scenarios}}

**Load Patterns:**
{{load_patterns}}

**Ramp-up Strategy:**
{{rampup_strategy}}

**Break-point Testing:**
{{breakpoint_testing}}

#### Performance Monitoring
**Monitoring Tools:**
{{performance_tools}}

**Key Metrics:**
{{performance_metrics}}

**Alert Thresholds:**
{{performance_alerts}}

**Reporting Strategy:**
{{performance_reporting}}

### 5. Security Testing Framework

#### Security Test Categories
**Authentication Testing:**
{{auth_testing}}

**Authorization Testing:**
{{authz_testing}}

**Input Validation:**
{{input_validation}}

**Session Management:**
{{session_testing}}

#### Vulnerability Assessment
**OWASP Top 10:**
{{owasp_testing}}

**Penetration Testing:**
{{pentest_scope}}

**Code Security Review:**
{{code_security}}

**Infrastructure Security:**
{{infra_security}}

#### Compliance Testing
**Regulatory Requirements:**
{{compliance_requirements}}

**Data Protection:**
{{data_protection}}

**Audit Requirements:**
{{audit_requirements}}

**Certification Needs:**
{{certification_needs}}

### 6. CI/CD Integration

#### Pipeline Integration
**Build Integration:**
{{build_integration}}

**Test Execution:**
{{test_execution}}

**Deployment Gates:**
{{deployment_gates}}

**Rollback Triggers:**
{{rollback_triggers}}

#### Quality Gates
**Code Coverage Thresholds:**
{{coverage_thresholds}}

**Test Pass Rates:**
{{pass_rates}}

**Performance Benchmarks:**
{{performance_benchmarks}}

**Security Scan Results:**
{{security_scans}}

#### Reporting & Notifications
**Test Reports:**
{{test_reports}}

**Failure Notifications:**
{{failure_notifications}}

**Quality Dashboards:**
{{quality_dashboards}}

**Stakeholder Communication:**
{{stakeholder_communication}}

### 7. Test Environment Management

#### Environment Configuration
**Environment Types:**
{{environment_types}}

**Configuration Management:**
{{config_management}}

**Environment Provisioning:**
{{environment_provisioning}}

**Data Seeding:**
{{data_seeding}}

#### Environment Monitoring
**Health Checks:**
{{environment_health}}

**Performance Monitoring:**
{{environment_performance}}

**Resource Utilization:**
{{resource_monitoring}}

**Cost Optimization:**
{{cost_optimization}}

### 8. Defect Management

#### Bug Tracking
**Defect Classification:**
{{defect_classification}}

**Priority Matrix:**
{{priority_matrix}}

**Severity Levels:**
{{severity_levels}}

**Escalation Procedures:**
{{escalation_procedures}}

#### Root Cause Analysis
**Analysis Framework:**
{{rca_framework}}

**Pattern Identification:**
{{pattern_identification}}

**Prevention Strategies:**
{{prevention_strategies}}

**Process Improvements:**
{{process_improvements}}

### 9. Quality Metrics & Reporting

#### Test Metrics
**Coverage Metrics:**
{{coverage_metrics}}

**Quality Metrics:**
{{quality_metrics}}

**Efficiency Metrics:**
{{efficiency_metrics}}

**Trend Analysis:**
{{trend_analysis}}

#### Business Metrics
**Customer Impact:**
{{customer_impact}}

**Business Value:**
{{business_value}}

**Cost of Quality:**
{{cost_quality}}

**ROI Analysis:**
{{roi_analysis}}

#### Executive Reporting
**Dashboard Design:**
{{executive_dashboard}}

**KPI Tracking:**
{{kpi_tracking}}

**Risk Assessment:**
{{risk_reporting}}

**Recommendation Engine:**
{{recommendation_engine}}

### 10. Continuous Improvement

#### Test Optimization
**Test Suite Optimization:**
{{test_optimization}}

**Execution Time Reduction:**
{{execution_optimization}}

**Maintenance Reduction:**
{{maintenance_optimization}}

**Reliability Improvement:**
{{reliability_improvement}}

#### Process Enhancement
**Methodology Updates:**
{{methodology_updates}}

**Tool Evaluation:**
{{tool_evaluation}}

**Skill Development:**
{{skill_development}}

**Best Practice Adoption:**
{{best_practices}}

#### Innovation Integration
**AI/ML Integration:**
{{ai_integration}}

**Predictive Analytics:**
{{predictive_analytics}}

**Intelligent Test Generation:**
{{intelligent_generation}}

**Autonomous Testing:**
{{autonomous_testing}}

## Implementation Roadmap
**Phase 1**: Foundation Setup ({{phase1_timeline}})
**Phase 2**: Automation Implementation ({{phase2_timeline}})
**Phase 3**: Advanced Features ({{phase3_timeline}})
**Phase 4**: Optimization & Innovation ({{phase4_timeline}})

## Success Criteria & ROI
**Quality Improvements:**
{{quality_improvements}}

**Cost Reductions:**
{{cost_reductions}}

**Time Savings:**
{{time_savings}}

**Risk Mitigation:**
{{risk_mitigation}}",
                CollectionId = 1001,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed sample variables
        modelBuilder.Entity<PromptVariable>().HasData(
            new PromptVariable
            {
                Id = 1,
                Name = "language",
                Description = "Programming language of the code",
                DefaultValue = "javascript",
                Type = VariableType.Text,
                PromptTemplateId = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PromptVariable
            {
                Id = 2,
                Name = "code",
                Description = "The code to review",
                DefaultValue = "// Paste your code here",
                Type = VariableType.LargeText,
                PromptTemplateId = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
