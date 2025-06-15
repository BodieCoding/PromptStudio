# Code Review Prompts for PromptStudio

This collection provides comprehensive, reusable code review prompts with variables to help you perform thorough and consistent code reviews across different types of projects and programming languages.

## 📁 Files Included

- `code_review_prompts.json` - Complete prompt collection with 5 specialized review templates
- `code_review_variables.csv` - Sample variables for testing and examples
- `import_code_review_prompts.ps1` - PowerShell script to import prompts into PromptStudio
- `CODE_REVIEW_GUIDE.md` - This documentation file

## 🎯 Available Prompt Templates

### 1. Comprehensive Code Review
**Best for:** General code reviews covering all aspects
**Variables:** 17 customizable parameters
**Focus Areas:**
- Functionality analysis
- Code quality assessment
- Security evaluation
- Performance review
- Testing considerations
- Architecture compliance

### 2. Security-Focused Code Review
**Best for:** Security audits and vulnerability assessments
**Variables:** 16 security-specific parameters
**Focus Areas:**
- OWASP Top 10 vulnerabilities
- Input validation and sanitization
- Authentication and authorization
- Data protection and encryption
- Error handling and logging
- Compliance assessment

### 3. Performance-Focused Code Review
**Best for:** Performance optimization and bottleneck identification
**Variables:** 15 performance-related parameters
**Focus Areas:**
- Algorithm efficiency analysis
- Memory management
- Database optimization
- Concurrency patterns
- Resource utilization
- Scalability considerations

### 4. Code Quality & Maintainability Review
**Best for:** Technical debt assessment and maintainability evaluation
**Variables:** 13 quality-focused parameters
**Focus Areas:**
- SOLID principles compliance
- Design patterns usage
- Code structure and organization
- Documentation quality
- Testing considerations
- Technical debt analysis

### 5. API Design Review
**Best for:** REST API endpoints and web service reviews
**Variables:** 16 API-specific parameters
**Focus Areas:**
- RESTful design principles
- Request/response design
- Security implementation
- Performance considerations
- Documentation completeness
- Integration readiness

## 🚀 Quick Start

### Option 1: Automatic Import (Recommended)
```powershell
# Run the import script
.\import_code_review_prompts.ps1

# Or specify custom PromptStudio URL
.\import_code_review_prompts.ps1 -PromptStudioUrl "http://localhost:5000"
```

### Option 2: Manual Import
1. Start PromptStudio application
2. Navigate to **Collections** → **Import**
3. Select `code_review_prompts.json`
4. Create variable collections using `code_review_variables.csv`

## 💡 Usage Examples

### Example 1: Review a C# Authentication Controller

**Template:** Comprehensive Code Review
**Key Variables:**
```
language: C#
project_type: Web API
module_name: AuthenticationController
review_focus: Security and Performance
security_concerns: OWASP Top 10
```

### Example 2: Security Audit of Payment Processing

**Template:** Security-Focused Code Review
**Key Variables:**
```
language: C#
application_type: Financial Service
compliance_requirements: PCI DSS
data_sensitivity_level: Critical
threat_actors: External attackers, insider threats
```

### Example 3: Performance Review of Data Pipeline

**Template:** Performance-Focused Code Review
**Key Variables:**
```
language: Python
application_type: Data Pipeline
expected_load: 1M records/hour
performance_sla: <10 minutes processing time
resource_constraints: 8GB RAM, 4 CPU cores
```

## 🔧 Customization Tips

### Variable Customization
- **Default Values:** Pre-populate common values for your team/project
- **Descriptions:** Add descriptions to guide reviewers
- **Validation:** Consider adding validation rules for critical variables

### Template Modifications
- **Add Sections:** Include company-specific review criteria
- **Remove Sections:** Skip areas not relevant to your codebase
- **Combine Templates:** Merge multiple templates for comprehensive reviews

### Team-Specific Adaptations
```json
// Example: Add team-specific coding standards
"coding_standards": "Company X C# Style Guide v2.1"

// Example: Include project-specific frameworks
"framework": ".NET 8 with Custom Authentication Framework"

// Example: Add specific compliance requirements
"compliance_requirements": "SOX, GDPR, Company Policy 123"
```

## 📊 Best Practices

### 1. Consistent Variable Sets
Create standard variable collections for different project types:
- **Web Applications:** Standard security and performance variables
- **APIs:** Focus on REST principles and integration points
- **Data Services:** Emphasize performance and data quality
- **Mobile Apps:** Include platform-specific considerations

### 2. Review Workflow Integration
```
1. Select appropriate template based on review type
2. Load project-specific variable collection
3. Customize variables for current review context
4. Execute prompt and analyze results
5. Create action items with priority levels
6. Track resolution and re-review if needed
```

### 3. Variable Collection Management
- **Project Templates:** Create variable collections per project type
- **Team Standards:** Maintain team-wide default values
- **Historical Data:** Save successful variable combinations
- **Continuous Improvement:** Update based on review outcomes

## 🎯 Advanced Usage

### Batch Reviews
Use variable collections to review multiple files or components:
```csv
module_name,code_snippet,specific_concerns
UserController,"[code here]","Authentication middleware integration"
PaymentService,"[code here]","Transaction rollback mechanisms"
DataProcessor,"[code here]","Memory usage with large datasets"
```

### Integration with CI/CD
1. Export prompt results as structured data
2. Integration with code quality gates
3. Automated variable population from repository metadata
4. Link with issue tracking systems

### Custom Scoring Systems
Add scoring variables to track improvement:
```
quality_score_previous: 7/10
performance_baseline: 500ms average
security_risk_level: Medium
technical_debt_hours: 16
```

## 🔍 Troubleshooting

### Common Issues

**"Template not executing properly"**
- Check all required variables are filled
- Verify code_snippet variable contains valid code
- Ensure target_audience is specified

**"Results too generic"**
- Provide more specific context in variables
- Use specific_concerns for targeted analysis
- Consider using a more specialized template

**"Missing framework-specific advice"**
- Update framework variable with specific version
- Add framework-specific concerns to specific_concerns
- Consider creating custom templates for unique frameworks

### Variable Tips
- **Code Snippets:** Include surrounding context for better analysis
- **Business Requirements:** Be specific about functional requirements
- **Constraints:** Include both technical and business constraints
- **Audience:** Tailor language and detail level to the audience

## 📈 Metrics and Improvement

### Track Review Effectiveness
- **Issue Detection Rate:** Issues found vs. missed in production
- **Review Time:** Time spent per review with vs. without prompts
- **Consistency:** Standardization across different reviewers
- **Team Learning:** Knowledge transfer through standardized reviews

### Continuous Improvement
1. **Regular Template Updates:** Based on new threats, patterns, best practices
2. **Variable Refinement:** Add/remove variables based on usage patterns
3. **Team Feedback:** Gather feedback on prompt effectiveness
4. **Industry Standards:** Keep updated with latest coding standards and security practices

---

## 🤝 Contributing

To improve these prompts:
1. Test with various code samples
2. Identify missing review areas
3. Suggest variable improvements
4. Share successful customizations
5. Report issues or gaps

## 📚 Additional Resources

- [PromptStudio Documentation](./README.md)
- [Code Review Best Practices](https://docs.microsoft.com/en-us/azure/devops/repos/git/pull-requests)
- [OWASP Code Review Guide](https://owasp.org/www-pdf-archive/OWASP_Code_Review_Guide_v2.pdf)
- [Microsoft Secure Coding Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/security/secure-coding-guidelines)
