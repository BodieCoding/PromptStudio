import * as vscode from 'vscode';
import { PromptsProvider } from './promptsProvider';
import { McpService } from './mcpService';
import { PromptWebviewProvider } from './promptWebviewProvider';
import { CopilotIntegration } from './copilotIntegration';

export function activate(context: vscode.ExtensionContext) {
    console.log('PromptStudio extension is now active!');    
    
    // Initialize native MCP service
    const mcpService = new McpService();
    const promptsProvider = new PromptsProvider(mcpService);
    const webviewProvider = new PromptWebviewProvider(context.extensionUri, mcpService);
    const copilotIntegration = new CopilotIntegration(mcpService);

    // Initialize Copilot integration
    copilotIntegration.initialize(context);

    // Set context for when extension is enabled
    vscode.commands.executeCommand('setContext', 'promptstudio.enabled', true);

    // Register tree data provider
    vscode.window.createTreeView('promptstudio.promptsView', {
        treeDataProvider: promptsProvider,
        showCollapseAll: true
    });

    // Register webview provider
    context.subscriptions.push(
        vscode.window.registerWebviewViewProvider('promptstudio.webview', webviewProvider)
    );

    // Register commands
    const commands = [
        vscode.commands.registerCommand('promptstudio.openWebview', () => {
            webviewProvider.show();
        }),

        vscode.commands.registerCommand('promptstudio.createPrompt', async () => {
            await promptsProvider.createNewPrompt();
        }),

        vscode.commands.registerCommand('promptstudio.executePrompt', async (item) => {
            if (item && item.prompt) {
                await copilotIntegration.executePromptWithCopilot(item.prompt);
            }
        }),

        vscode.commands.registerCommand('promptstudio.refreshPrompts', () => {
            promptsProvider.refresh();
        })
    ];

        context.subscriptions.push(...commands);    // Store MCP service for cleanup
    context.subscriptions.push({
        dispose: () => mcpService.dispose()
    });

    // Auto-refresh prompts if enabled
    const config = vscode.workspace.getConfiguration('promptstudio');
    if (config.get('autoRefresh', true)) {
        // Refresh every 30 seconds
        setInterval(() => {
            promptsProvider.refresh();
        }, 30000);
    }
}

export function deactivate() {
    console.log('PromptStudio extension is now deactivated');
}
