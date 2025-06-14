# VS Code Extension Test Guide

## Testing the PromptStudio VS Code Extension with GitHub Copilot

### Prerequisites
✅ PromptStudio server is running on http://localhost:5131
✅ VS Code extension v1.0.2 is installed
✅ GitHub Copilot is active in VS Code

### Test Steps

1. **Open VS Code**
   ```
   code
   ```

2. **Open GitHub Copilot Chat**
   - Press `Ctrl+Shift+P` (Command Palette)
   - Type: `GitHub Copilot: Open Chat`
   - Or use the chat icon in the activity bar

3. **Test PromptStudio Commands**

   **List all prompts:**
   ```
   @promptstudio list
   ```

   **Search for prompts:**
   ```
   @promptstudio search code review
   ```

   **Execute a specific prompt:**
   ```
   @promptstudio execute 1 language:javascript code:console.log("Hello World");
   ```

### Expected Results

- `@promptstudio list` should show available prompts
- `@promptstudio search` should filter prompts by keyword
- `@promptstudio execute` should run the prompt with variables

### Troubleshooting

If commands don't work:
1. Check that the extension is activated
2. Verify PromptStudio server is running
3. Check VS Code developer console for errors
4. Restart VS Code and try again

### Current API Status
- Server: http://localhost:5131
- Endpoint: /api/prompts/prompts
- Available Prompts: 2 found
  - ID 1: "Code Review" 
  - ID 2: "Single Quoted CSV from list of words"
