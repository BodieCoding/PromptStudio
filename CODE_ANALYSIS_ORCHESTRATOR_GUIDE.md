# Code Analysis Orchestrator - Usage Guide

## Overview

This reusable prompt system orchestrates the analysis of code quality reports and automatically generates actionable GitHub issues for systematic code improvement. It leverages both **PromptStudio** and **GitHub MCP servers** to create a complete workflow from analysis to implementation tracking.

## How It Works

1. **Input**: Code analysis report (like `PROMPTSTUDIO_CORE_ANALYSIS_README.md`)
2. **Analysis**: AI extracts actionable items and categorizes them
3. **GitHub Integration**: Creates properly structured issues with labels, milestones, and assignments
4. **Tracking**: Generates meta-issues for progress monitoring
5. **Reproducibility**: Designed to be re-run for continuous improvement

## Prerequisites

### Required MCP Servers
1. **PromptStudio MCP Server** - For prompt execution and variable management
2. **GitHub MCP Server** - For creating issues, labels, and project management

### Required Permissions
- GitHub repository write access
- Ability to create issues, labels, and milestones
- Access to assign issues to team members

## Setup Instructions

### 1. Configure PromptStudio
```bash
# Start PromptStudio web application
dotnet run --project PromptStudio/PromptStudio.csproj

# Start PromptStudio MCP server
cd mcp-server
npm start
```

### 2. Configure GitHub MCP Server
Ensure your GitHub MCP server is configured with proper authentication:
```json
{
  "github": {
    "token": "your_github_personal_access_token",
    "default_owner": "your_org",
    "default_repo": "your_repo"
  }
}
```

### 3. Prepare Your Analysis Report
Ensure your code analysis report includes:
- Clear sections for different types of issues
- Specific recommendations with actionable items
- Priority indicators or severity levels
- File references where applicable

## Usage

### Method 1: Using PromptStudio Web Interface

1. **Navigate to PromptStudio**: http://localhost:5131
2. **Go to Collections**: Find "AI Agent Workflows"
3. **Select Prompt**: "Code Analysis Orchestrator"
4. **Fill Variables**:
   - `analysis_file_path`: Path to your analysis report
   - `repo_owner`: GitHub organization/user
   - `repo_name`: Repository name
   - `milestone_name`: Target milestone (optional)
   - `assignee`: Default assignee (optional)
   - `priority_label`: Label prefix for priorities
5. **Execute**: Run the prompt
6. **Review Output**: Check generated GitHub issues

### Method 2: Using CSV Batch Processing

1. **Prepare CSV File**: Use `code_analysis_orchestrator_variables.csv` as template
2. **Upload to PromptStudio**: Create variable collection from CSV
3. **Batch Execute**: Run prompt against all rows

### Method 3: Using MCP Commands

```javascript
// Via PromptStudio MCP Server
await promptStudio.executeTemplate(2004, {
  analysis_file_path: "PROMPTSTUDIO_CORE_ANALYSIS_README.md",
  repo_owner: "myorg",
  repo_name: "promptstudio",
  milestone_name: "Q2 2025 Code Quality",
  assignee: "lead-developer",
  priority_label: "priority"
});
```

## Variable Reference

| Variable | Description | Example | Required |
|----------|-------------|---------|----------|
| `analysis_file_path` | Path to the code analysis report | `"CODE_ANALYSIS_REPORT.md"` | ✅ |
| `repo_owner` | GitHub repository owner | `"microsoft"` | ✅ |
| `repo_name` | GitHub repository name | `"vscode"` | ✅ |
| `milestone_name` | GitHub milestone to assign issues | `"Q2 2025 Code Quality"` | ❌ |
| `assignee` | Default assignee for issues | `"john.doe"` | ❌ |
| `priority_label` | Prefix for priority labels | `"priority"` | ❌ |

## Expected Outputs

### 1. GitHub Issues Created
- **Individual Issues**: One per identified improvement
- **Proper Labeling**: Category and priority labels applied
- **Structured Content**: Problem description, acceptance criteria, implementation approach
- **Linking**: Issues linked to source analysis and related items

### 2. Meta-Issue
- **Tracking Issue**: Overview of the entire improvement initiative
- **Progress Checklist**: Organized by priority level
- **Success Metrics**: Clear definition of completion
- **Process Documentation**: Instructions for team members

### 3. Summary Report
- **Issue Count**: Breakdown by priority and category
- **GitHub Links**: Direct links to all created issues
- **Timeline**: Suggested implementation order
- **Next Steps**: Actions required to proceed

## Issue Categories and Labels

### Priority Levels
- **P0 (Critical)**: `priority:p0` - Security, breaking bugs, data loss
- **P1 (High)**: `priority:p1` - Performance, major technical debt
- **P2 (Medium)**: `priority:p2` - Code quality, refactoring
- **P3 (Low)**: `priority:p3` - Documentation, minor optimizations

### Category Labels
- `bug` - Functional defects
- `enhancement` - New features/improvements
- `technical-debt` - Code quality and maintainability
- `security` - Security-related issues
- `performance` - Performance optimizations
- `documentation` - Documentation improvements
- `testing` - Test coverage and quality
- `dependencies` - Package updates and management

## Best Practices

### 1. Regular Execution
- **Schedule**: Run after major releases or monthly
- **Versioning**: Tag analysis reports with dates
- **Progress Tracking**: Update meta-issues regularly

### 2. Issue Management
- **Prioritization**: Address P0 issues immediately
- **Assignment**: Distribute work based on expertise
- **Review**: Regular issue review and reprioritization

### 3. Continuous Improvement
- **Metrics**: Track improvement over time
- **Process Refinement**: Update prompt based on learnings
- **Tool Evolution**: Enhance analysis tools and reports

## Example Workflow

```bash
# 1. Generate Code Analysis
# (Run your analysis tools to create/update the analysis report)

# 2. Execute Orchestrator Prompt
# Use PromptStudio with these variables:
# - analysis_file_path: "PROMPTSTUDIO_CORE_ANALYSIS_README.md"
# - repo_owner: "myorg" 
# - repo_name: "myproject"
# - milestone_name: "Q2 2025 Code Quality"
# - assignee: "tech-lead"
# - priority_label: "priority"

# 3. Review Generated Issues
# Check GitHub repository for new issues and meta-issue

# 4. Begin Implementation
# Start with P0 issues, then P1, etc.

# 5. Track Progress
# Update meta-issue checklist as work completes

# 6. Re-run Analysis
# Execute again after significant improvements
```

## Troubleshooting

### Common Issues

1. **MCP Server Not Running**
   - Ensure both PromptStudio and GitHub MCP servers are active
   - Check server logs for connection issues

2. **GitHub Authentication**
   - Verify GitHub token has required permissions
   - Check token expiration and scopes

3. **Analysis File Not Found**
   - Ensure analysis file path is correct and accessible
   - Verify file contains structured analysis content

4. **Variables Not Resolved**
   - Check variable names match exactly (case-sensitive)
   - Ensure all required variables are provided

### Support

For issues with this orchestration system:
1. Check PromptStudio logs and MCP server outputs
2. Verify GitHub API rate limits and permissions
3. Review analysis report format and content
4. Test with sample data first

## Advanced Usage

### Custom Labels and Categories
Modify the prompt template to add custom labels or categories specific to your organization.

### Integration with CI/CD
Incorporate this orchestrator into your CI/CD pipeline to automatically create improvement issues after code analysis runs.

### Custom Analysis Reports
Adapt the prompt to work with different analysis tool outputs by modifying the analysis phase instructions.

### Multi-Repository Support
Use CSV batch processing to run analysis across multiple repositories simultaneously.

This orchestration system provides a complete workflow for turning code analysis insights into actionable work items, ensuring continuous improvement and maintaining code quality standards.
