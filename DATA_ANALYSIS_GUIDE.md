# Data Analysis Prompt Collection Guide

## Overview
This collection contains 8 specialized prompts designed for comprehensive data analysis tasks, each with carefully crafted variables to support different analytical scenarios.

## Prompt Templates

### 1. Statistical Summary Analysis
**Purpose**: Generate comprehensive statistical summaries of datasets
**Key Variables**: `data_type`, `dataset_name`, `key_metrics`, `business_objective`
**Use Cases**: Initial data exploration, executive reporting, data quality assessment

### 2. Trend Analysis & Forecasting
**Purpose**: Analyze trends and create forecasts for time-series data
**Key Variables**: `metric_name`, `time_frame`, `seasonality_pattern`, `forecast_horizon`
**Use Cases**: Business planning, capacity planning, budget forecasting

### 3. Comparative Analysis
**Purpose**: Compare performance between different groups, time periods, or segments
**Key Variables**: `comparison_subjects`, `comparison_dimensions`, `comparison_metrics`
**Use Cases**: Performance benchmarking, competitive analysis, before/after studies

### 4. Data Quality Assessment
**Purpose**: Evaluate data quality across multiple dimensions
**Key Variables**: `dataset_name`, `data_source`, `critical_fields`, `business_rules`
**Use Cases**: Data governance, ETL validation, compliance reporting

### 5. Customer Segmentation Analysis
**Purpose**: Segment customers for targeted marketing and strategy
**Key Variables**: `segmentation_approach`, `segmentation_variables`, `business_objective`
**Use Cases**: Marketing strategy, product development, customer success

### 6. Performance Metrics Dashboard Analysis
**Purpose**: Optimize business dashboards and KPI frameworks
**Key Variables**: `business_unit`, `current_metrics`, `stakeholder_level`
**Use Cases**: Executive reporting, operational monitoring, strategic planning

### 7. A/B Test Results Analysis
**Purpose**: Analyze experiment results with statistical rigor
**Key Variables**: `test_name`, `test_hypothesis`, `primary_metric`, `confidence_level`
**Use Cases**: Product optimization, marketing testing, UX improvements

### 8. Cohort Analysis
**Purpose**: Understand user behavior patterns over time
**Key Variables**: `cohort_type`, `retention_metric`, `user_behavior`
**Use Cases**: Retention analysis, product-market fit, customer lifecycle

## Variable Collections (CSV Files)

### General Data Analysis (`data_analysis_variables.csv`)
Contains 8 scenarios covering:
- E-commerce sales analysis
- Customer churn analysis  
- Website traffic analysis
- Financial performance analysis
- Product usage analysis
- Marketing campaign analysis
- Inventory analysis
- Employee performance analysis

### A/B Testing (`ab_test_variables.csv`)
Contains 6 experiment scenarios:
- Homepage CTA tests
- Pricing page experiments
- Email subject line tests
- Checkout flow optimization
- Onboarding flow tests
- Mobile app push notifications

### Trend Analysis (`trend_analysis_variables.csv`)
Contains 8 forecasting scenarios:
- Monthly recurring revenue
- Website traffic trends
- Customer acquisition cost
- Product feature adoption
- Support ticket volume
- Email campaign performance
- Mobile app downloads
- Sales pipeline velocity

### Customer Segmentation (`customer_segmentation_variables.csv`)
Contains 6 segmentation approaches:
- E-commerce RFM analysis
- SaaS behavioral clustering
- Mobile app engagement segmentation
- Financial services lifecycle segmentation
- Healthcare risk-based segmentation
- Retail value-based segmentation

## How to Use

### Step 1: Import the Collection
1. Open PromptStudio at http://localhost:5000
2. Create a new collection named "Data Analysis Tasks"
3. Import each prompt template with its corresponding variables

### Step 2: Create Variable Collections
1. For each prompt, upload the relevant CSV file to create variable collections
2. Use the "Download Template" feature to get properly formatted CSV headers
3. Customize the CSV data for your specific use cases

### Step 3: Execute Analysis
1. Select the appropriate prompt for your analysis needs
2. Choose a variable collection or enter custom values
3. Execute the prompt to generate comprehensive analysis
4. Export results for further processing or reporting

### Step 4: Batch Processing
1. Use multiple variable sets to run comparative analyses
2. Export batch results to identify patterns across scenarios
3. Use results to inform business decisions and strategy

## Best Practices

### Variable Customization
- Modify CSV files to match your industry and business context
- Add new scenarios relevant to your specific use cases
- Ensure variable values are realistic and contextually appropriate

### Prompt Adaptation
- Customize prompts to align with your organization's terminology
- Add industry-specific requirements or constraints
- Include compliance or regulatory considerations where applicable

### Analysis Workflow
1. Start with Statistical Summary Analysis for initial data exploration
2. Use Data Quality Assessment to validate your data
3. Apply specific analysis types (trend, comparative, cohort) based on objectives
4. Use A/B Test Analysis for experiment validation
5. Create dashboards using Performance Metrics analysis

### Integration with BI Tools
- Export analysis results to integrate with existing BI platforms
- Use prompt outputs as input for automated reporting systems
- Create data dictionaries based on variable definitions

## Advanced Use Cases

### Multi-Stage Analysis
Combine multiple prompts for comprehensive analysis:
1. Data Quality Assessment → Statistical Summary → Trend Analysis
2. Customer Segmentation → Cohort Analysis → Performance Metrics
3. A/B Test Analysis → Comparative Analysis → Dashboard Optimization

### Cross-Functional Collaboration
- Marketing: Customer segmentation + A/B testing + campaign analysis
- Product: Usage analysis + cohort analysis + feature performance
- Finance: Trend forecasting + comparative analysis + performance metrics
- Operations: Data quality + statistical summaries + operational dashboards

### Automated Reporting
- Use batch execution for regular reporting cycles
- Create standardized analysis templates for different stakeholder groups
- Set up monitoring dashboards based on analysis outputs

## Tips for Maximum Value

1. **Contextualize Variables**: Always adapt variables to your specific business context
2. **Validate Assumptions**: Use multiple analysis approaches to validate findings
3. **Document Insights**: Capture key insights and recommendations in structured formats
4. **Iterate and Improve**: Continuously refine prompts based on analysis outcomes
5. **Share Knowledge**: Use successful analyses as templates for similar future projects

This collection provides a comprehensive foundation for data-driven decision making across various business functions and analytical scenarios.
