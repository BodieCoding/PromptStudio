# GitHub Issues Creation Guide

## Method 1: Using GitHub CLI (Recommended)

### Prerequisites
1. Install GitHub CLI: https://cli.github.com/
2. Authenticate: `gh auth login`
3. Ensure you have write access to the repository

### Execute the Script
```powershell
# Update the repository details
.\create-github-issues.ps1 -RepoOwner "your-username" -RepoName "promptstudio" -Milestone "Q2 2025 Code Quality Initiative" -Assignee "your-username"
```

## Method 2: Using GitHub API with PowerShell

```powershell
# Set your GitHub token and repository
$token = "your_github_personal_access_token"
$repo = "your-username/promptstudio"
$headers = @{
    "Authorization" = "token $token"
    "Accept" = "application/vnd.github.v3+json"
}

# Read the JSON data
$data = Get-Content "github-issues-data.json" | ConvertFrom-Json

# Create milestone
$milestoneBody = @{
    title = $data.milestone.title
    description = $data.milestone.description
    state = $data.milestone.state
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://api.github.com/repos/$repo/milestones" -Method POST -Headers $headers -Body $milestoneBody

# Create meta-issue
$metaIssueBody = @{
    title = $data.meta_issue.title
    body = $data.meta_issue.body
    labels = $data.meta_issue.labels
    assignees = $data.meta_issue.assignees
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://api.github.com/repos/$repo/issues" -Method POST -Headers $headers -Body $metaIssueBody

# Create individual issues
foreach ($issue in $data.issues) {
    $issueBody = @{
        title = $issue.title
        body = $issue.body
        labels = $issue.labels
        assignees = $issue.assignees
    } | ConvertTo-Json
    
    Invoke-RestMethod -Uri "https://api.github.com/repos/$repo/issues" -Method POST -Headers $headers -Body $issueBody
    Start-Sleep -Milliseconds 500  # Rate limiting
}
```

## Method 3: Manual Creation

Use the structured content from `GENERATED_GITHUB_ISSUES.md` to manually create issues in GitHub web interface:

1. Go to your repository on GitHub
2. Click "Issues" tab
3. Click "New issue"
4. Copy title and body from the generated file
5. Add appropriate labels
6. Set milestone and assignee
7. Repeat for each issue

## Verification

After creating issues, verify:
- [ ] Meta-issue created with tracking checklist
- [ ] All 4 P0 critical issues created
- [ ] Labels applied correctly (technical-debt, security, performance, etc.)
- [ ] Milestone assigned
- [ ] Assignees set
- [ ] Issues linked to milestone

## Next Steps

1. **Immediate Action**: Address P0 critical issues
2. **Team Assignment**: Distribute work based on expertise
3. **Progress Tracking**: Update meta-issue as work completes
4. **Re-analysis**: Schedule analysis re-run after Phase 1
