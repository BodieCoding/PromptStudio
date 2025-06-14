import * as vscode from 'vscode';
import { McpService, PromptTemplate } from './mcpService';

export class CopilotIntegration {
    constructor(private promptService: McpService) {}

    initialize(context: vscode.ExtensionContext): void {
        // Register participant for Copilot Chat integration
        const participant = vscode.chat.createChatParticipant('promptstudio', this.handleCopilotRequest.bind(this));
        participant.iconPath = new vscode.ThemeIcon('symbol-misc');
        context.subscriptions.push(participant);
    }

    private async handleCopilotRequest(
        request: vscode.ChatRequest,
        context: vscode.ChatContext,
        stream: vscode.ChatResponseStream,
        token: vscode.CancellationToken
    ): Promise<void> {
        try {
            const input = request.prompt.trim();
            
            // Parse the request
            if (input.startsWith('list') || input.startsWith('show')) {
                await this.handleListPrompts(stream);
            } else if (input.startsWith('execute') || input.startsWith('run')) {
                await this.handleExecutePrompt(input, stream);
            } else if (input.startsWith('create')) {
                await this.handleCreatePrompt(input, stream);
            } else {
                // Default: search for prompts that match the input
                await this.handleSearchPrompts(input, stream);
            }
        } catch (error) {
            stream.markdown(`❌ Error: ${error instanceof Error ? error.message : 'Unknown error'}`);
        }
    }

    private async handleListPrompts(stream: vscode.ChatResponseStream): Promise<void> {
        try {
            const prompts = await this.promptService.getPromptTemplates();
            
            if (prompts.length === 0) {
                stream.markdown('No prompts found. Create your first prompt using the PromptStudio panel.');
                return;
            }

            stream.markdown('## Available Prompts\n\n');
            
            for (const prompt of prompts) {                stream.markdown(`### ${prompt.name}\n`);
                if (prompt.description) {
                    stream.markdown(`${prompt.description}\n`);
                }
                const variables = this.promptService.extractVariables(prompt.content);
                if (variables.length > 0) {
                    stream.markdown(`**Variables**: ${variables.join(', ')}\n`);
                }
                stream.markdown(`*Use: @promptstudio execute "${prompt.name}"*\n\n`);
            }
        } catch (error) {
            stream.markdown(`❌ Failed to list prompts: ${error}`);
        }
    }

    private async handleExecutePrompt(input: string, stream: vscode.ChatResponseStream): Promise<void> {
        // Extract prompt name from input like "execute 'Prompt Name'" or "run Prompt Name"
        const match = input.match(/(?:execute|run)\s+["']?([^"']+)["']?/i);
        if (!match) {
            stream.markdown('❌ Please specify a prompt name. Usage: `execute "Prompt Name"`');
            return;
        }

        const promptName = match[1].trim();
        
        try {
            const prompts = await this.promptService.getPromptTemplates();
            const prompt = prompts.find(p => 
                p.name.toLowerCase() === promptName.toLowerCase() ||
                p.name.toLowerCase().includes(promptName.toLowerCase())
            );

            if (!prompt) {
                stream.markdown(`❌ Prompt "${promptName}" not found. Available prompts:\n`);
                for (const p of prompts) {
                    stream.markdown(`- ${p.name}\n`);
                }
                return;
            }

            await this.executePromptWithCopilot(prompt, stream);
        } catch (error) {
            stream.markdown(`❌ Failed to execute prompt: ${error}`);
        }
    }

    private async handleCreatePrompt(input: string, stream: vscode.ChatResponseStream): Promise<void> {
        stream.markdown('To create a new prompt, use the PromptStudio panel or run the command:');
        stream.markdown('`> PromptStudio: Create New Prompt`');
    }

    private async handleSearchPrompts(input: string, stream: vscode.ChatResponseStream): Promise<void> {
        try {
            const prompts = await this.promptService.getPromptTemplates();
            const searchTerm = input.toLowerCase();
            
            const matchingPrompts = prompts.filter(p => 
                p.name.toLowerCase().includes(searchTerm) ||
                (p.description && p.description.toLowerCase().includes(searchTerm)) ||
                p.content.toLowerCase().includes(searchTerm)
            );

            if (matchingPrompts.length === 0) {
                stream.markdown(`No prompts found matching "${input}". Use \`@promptstudio list\` to see all available prompts.`);
                return;
            }

            stream.markdown(`## Prompts matching "${input}"\n\n`);
            
            for (const prompt of matchingPrompts) {
                stream.markdown(`### ${prompt.name}\n`);
                if (prompt.description) {
                    stream.markdown(`${prompt.description}\n`);
                }
                stream.markdown(`*Use: @promptstudio execute "${prompt.name}"*\n\n`);
            }
        } catch (error) {
            stream.markdown(`❌ Failed to search prompts: ${error}`);
        }
    }    async executePromptWithCopilot(prompt: PromptTemplate, stream?: vscode.ChatResponseStream): Promise<void> {
        try {
            // Collect variable values if the prompt has variables
            const variables: Record<string, string> = {};
            const variableNames = this.promptService.extractVariables(prompt.content);
            
            if (variableNames.length > 0) {
                if (stream) {
                    stream.markdown(`## Executing: ${prompt.name}\n\n`);
                    stream.markdown('This prompt requires the following variables:\n\n');
                    for (const varName of variableNames) {
                        stream.markdown(`- **${varName}**: Variable placeholder\n`);
                    }
                    stream.markdown('\nPlease provide values and run again, or use the PromptStudio panel for interactive execution.');
                    return;
                }                // Interactive mode - collect variables
                for (const varName of variableNames) {
                    const value = await vscode.window.showInputBox({
                        prompt: `Enter value for "${varName}"`,
                        placeHolder: '',
                        validateInput: (value) => {
                            return value.trim().length === 0 ? 'Value is required' : null;
                        }
                    });

                    if (value === undefined) {
                        // User cancelled
                        return;
                    }

                    variables[varName] = value;
                }
            }

            // Resolve the prompt with variables
            const resolvedContent = await this.promptService.resolvePrompt(prompt, variables);

            if (stream) {
                stream.markdown(`## Resolved Prompt\n\n${resolvedContent}\n\n`);
                stream.markdown('You can now copy this content to use with Copilot or any other AI tool.');
            } else {
                // Open the resolved content in a new document
                const doc = await vscode.workspace.openTextDocument({
                    content: resolvedContent,
                    language: 'markdown'
                });
                await vscode.window.showTextDocument(doc);
                
                vscode.window.showInformationMessage(
                    `Prompt "${prompt.name}" executed and opened in new document.`
                );
            }

        } catch (error) {
            const errorMsg = `Failed to execute prompt: ${error}`;
            if (stream) {
                stream.markdown(`❌ ${errorMsg}`);
            } else {
                vscode.window.showErrorMessage(errorMsg);
            }
        }
    }
}
