# Post-Rename Automation Script

## Repository Metadata Updates
After renaming is complete, run these commands to update metadata:

### Update Description
The new primary repository should have:
**Description**: "PromptStudio - A layered .NET application with MCP server integration for managing AI prompts with collections, variables, execution history, and advanced workflow orchestration."

### Update Topics
Topics should include:
- ai
- mcp
- mcp-server
- prompt-engineering
- prompt-toolkit
- dotnet
- asp-net-core
- entity-framework
- workflow-orchestration
- github-integration

### Archive Legacy Repository
Set `promptstudio-legacy` to archived status to indicate it's no longer actively developed.

## Local Git Remote Updates
```bash
# Update origin remote to point to new repository name
git remote set-url origin https://github.com/BodieCoding/promptstudio.git

# Verify the change
git remote -v
```

## Verification Steps
1. Verify new primary repository is accessible at: https://github.com/BodieCoding/promptstudio
2. Verify legacy repository is archived at: https://github.com/BodieCoding/promptstudio-legacy
3. Verify local git remote points to new URL
4. Test push/pull operations
