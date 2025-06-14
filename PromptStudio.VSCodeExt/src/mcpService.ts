import * as child_process from 'child_process';
import * as vscode from 'vscode';

// Native MCP types - direct from MCP protocol
export interface PromptTemplate {
    id: number;
    name: string;
    content: string;
    description?: string;
    collectionId: number;
}

export interface PromptCollection {
    id: number;
    name: string;
    description?: string;
}

export interface ExecutionResult {
    id: number;
    promptId: number;
    variables: string;
    result: string;
    executedAt: string;
}

interface McpRequest {
    jsonrpc: string;
    method: string;
    params?: any;
    id: number;
}

interface McpResponse {
    jsonrpc: string;
    result?: any;
    error?: any;
    id: number;
}

export class McpService {
    private serverProcess: child_process.ChildProcess | null = null;
    private isConnected = false;
    private requestId = 1;
    private pendingRequests = new Map<number, { resolve: Function; reject: Function }>();
    private buffer = '';
    private serverPath: string;

    constructor() {
        const config = vscode.workspace.getConfiguration('promptstudio');
        this.serverPath = config.get('mcpServerPath') || 'PromptStudioMcpServer.exe';
    }

    private async connect(): Promise<void> {
        if (this.isConnected) return;

        return new Promise((resolve, reject) => {
            console.log(`Starting MCP server: ${this.serverPath}`);
            
            this.serverProcess = child_process.spawn(this.serverPath, ['stdio'], {
                stdio: ['pipe', 'pipe', 'pipe']
            });

            this.serverProcess.stdout?.on('data', (data) => {
                this.buffer += data.toString();
                this.processBuffer();
            });

            this.serverProcess.stderr?.on('data', (data) => {
                console.error('MCP Server Error:', data.toString());
            });

            this.serverProcess.on('error', (error) => {
                console.error('Failed to start MCP server:', error);
                reject(error);
            });

            this.serverProcess.on('exit', (code) => {
                console.log('MCP server exited with code:', code);
                this.isConnected = false;
                this.serverProcess = null;
            });

            // Initialize MCP connection
            this.sendRequest('initialize', {
                protocolVersion: '2024-11-05',
                capabilities: {},
                clientInfo: { name: 'PromptStudio-VSCode', version: '1.0.0' }
            }).then(() => {
                this.isConnected = true;
                console.log('MCP server connected successfully');
                resolve();
            }).catch(reject);
        });
    }

    private processBuffer(): void {
        const lines = this.buffer.split('\n');
        this.buffer = lines.pop() || '';

        for (const line of lines) {
            if (line.trim()) {
                try {
                    const response: McpResponse = JSON.parse(line);
                    this.handleResponse(response);
                } catch (error) {
                    console.error('Failed to parse MCP response:', error, line);
                }
            }
        }
    }

    private handleResponse(response: McpResponse): void {
        const pending = this.pendingRequests.get(response.id);
        if (pending) {
            this.pendingRequests.delete(response.id);
            if (response.error) {
                pending.reject(new Error(response.error.message || 'MCP Error'));
            } else {
                pending.resolve(response.result);
            }
        }
    }

    private async sendRequest(method: string, params?: any): Promise<any> {
        if (!this.isConnected && method !== 'initialize') {
            await this.connect();
        }

        return new Promise((resolve, reject) => {
            const id = this.requestId++;
            const request: McpRequest = {
                jsonrpc: '2.0',
                method,
                params,
                id
            };

            this.pendingRequests.set(id, { resolve, reject });

            const message = JSON.stringify(request) + '\n';
            this.serverProcess?.stdin?.write(message);

            // Timeout after 30 seconds
            setTimeout(() => {
                if (this.pendingRequests.has(id)) {
                    this.pendingRequests.delete(id);
                    reject(new Error('Request timeout'));
                }
            }, 30000);
        });
    }

    private extractToolResult(result: any): any {
        // Extract the actual data from MCP tool call response
        return result.content?.[0]?.text ? JSON.parse(result.content[0].text) : result;
    }

    // Core MCP operations
    async getPromptTemplates(collectionId?: number): Promise<PromptTemplate[]> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_GetPromptTemplates',
                arguments: collectionId ? { collectionId } : {}
            });
            return this.extractToolResult(result) || [];
        } catch (error) {
            console.error('Failed to get prompt templates:', error);
            return [];
        }
    }

    async getCollections(): Promise<PromptCollection[]> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_GetCollections',
                arguments: {}
            });
            return this.extractToolResult(result) || [];
        } catch (error) {
            console.error('Failed to get collections:', error);
            return [];
        }
    }

    async getPromptTemplate(id: number): Promise<PromptTemplate | null> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_GetPromptTemplate',
                arguments: { id }
            });
            return this.extractToolResult(result);
        } catch (error) {
            console.error('Failed to get prompt template:', error);
            return null;
        }
    }

    async createPromptTemplate(name: string, content: string, collectionId: number, description?: string): Promise<PromptTemplate | null> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_CreatePromptTemplate',
                arguments: {
                    name,
                    content,
                    collectionId,
                    description
                }
            });
            return this.extractToolResult(result);
        } catch (error) {
            console.error('Failed to create prompt template:', error);
            return null;
        }
    }

    async createCollection(name: string, description?: string): Promise<PromptCollection | null> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_CreateCollection',
                arguments: {
                    name,
                    description
                }
            });
            return this.extractToolResult(result);
        } catch (error) {
            console.error('Failed to create collection:', error);
            return null;
        }
    }

    async executePrompt(id: number, variables: Record<string, string>): Promise<ExecutionResult | null> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_ExecutePrompt',
                arguments: {
                    id,
                    variables: JSON.stringify(variables)
                }
            });
            return this.extractToolResult(result);
        } catch (error) {
            console.error('Failed to execute prompt:', error);
            return null;
        }
    }

    async getExecutionHistory(promptId?: number, limit: number = 50): Promise<ExecutionResult[]> {
        try {
            const result = await this.sendRequest('tools/call', {
                name: 'd94_GetExecutionHistory',
                arguments: {
                    promptId: promptId || 0,
                    limit
                }
            });
            return this.extractToolResult(result) || [];
        } catch (error) {
            console.error('Failed to get execution history:', error);
            return [];
        }
    }

    // Utility methods
    async testConnection(): Promise<boolean> {
        try {
            await this.connect();
            return this.isConnected;
        } catch (error) {
            console.error('MCP connection test failed:', error);
            return false;
        }
    }

    async resolvePrompt(template: PromptTemplate, variables: Record<string, string>): Promise<string> {
        return this.resolvePromptVariables(template.content, variables);
    }

    resolvePromptVariables(template: string, variables: Record<string, string>): string {
        let resolved = template;
        for (const [key, value] of Object.entries(variables)) {
            resolved = resolved.replace(new RegExp(`{{${key}}}`, 'g'), value);
        }
        return resolved;
    }

    extractVariables(template: string): string[] {
        const matches = template.match(/{{(\w+)}}/g);
        return matches ? matches.map(match => match.slice(2, -2)) : [];
    }

    dispose(): void {
        if (this.serverProcess) {
            this.serverProcess.kill();
            this.serverProcess = null;
        }
        this.isConnected = false;
        this.pendingRequests.clear();
    }
}
