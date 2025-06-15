# Repository Migration Plan: Promptlet → PromptStudio

## Current State
- **Source**: `BodieCoding/Promptlet` (clean layered architecture)
- **Target**: `BodieCoding/promptstudio` (original monolithic)
- **Goal**: Make Promptlet the primary PromptStudio repository

## Migration Steps

### Phase 1: Backup Original Repository
1. ✅ **Manual Step Required**: Rename `BodieCoding/promptstudio` → `BodieCoding/promptstudio-legacy`
   - Go to: https://github.com/BodieCoding/promptstudio/settings
   - Scroll to "Repository name" section
   - Change name to `promptstudio-legacy`
   - This preserves the original with 2 stars and history

### Phase 2: Rename Current Repository
2. ✅ **Manual Step Required**: Rename `BodieCoding/Promptlet` → `BodieCoding/promptstudio`
   - Go to: https://github.com/BodieCoding/Promptlet/settings
   - Change name to `promptstudio`
   - This makes the clean architecture the primary repo

### Phase 3: Update Repository Metadata
3. 🔄 **Automated**: Update description and topics to match original
4. 🔄 **Automated**: Update README to reflect the new primary status
5. 🔄 **Automated**: Archive the legacy repository

### Phase 4: Local Workspace Updates
6. 🔄 **Automated**: Update local git remotes
7. 🔄 **Automated**: Update documentation references

## Why This Approach?
- ✅ Preserves original repository and its 2 stars
- ✅ Maintains commit history for both versions
- ✅ Clean transition with minimal disruption
- ✅ Establishes the better architecture as primary

## Architecture Comparison
### Legacy (promptstudio-legacy)
- Single project structure
- All code in one project
- Simple but less maintainable

### New Primary (promptstudio)
- Layered architecture with 4 projects:
  - `PromptStudio` (Web UI)
  - `PromptStudio.Core` (Business Logic)
  - `PromptStudio.Data` (Data Access)
  - `PromptStudio.Tests` (Testing)
- Better separation of concerns
- More scalable and maintainable
- Modern .NET practices
