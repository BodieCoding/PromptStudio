import * as vscode from 'vscode';
import { McpService } from './mcpService';

export class PromptWebviewProvider implements vscode.WebviewViewProvider {
    public static readonly viewType = 'promptstudio.webview';
    private _view?: vscode.WebviewView;

    constructor(
        private readonly _extensionUri: vscode.Uri,
        private promptService: McpService
    ) {}

    public resolveWebviewView(
        webviewView: vscode.WebviewView,
        context: vscode.WebviewViewResolveContext,
        _token: vscode.CancellationToken,
    ) {
        this._view = webviewView;        webviewView.webview.options = {
            enableScripts: true,
            localResourceRoots: [this._extensionUri]
        };

        webviewView.webview.html = this._getHtmlForWebview(webviewView.webview);

        // Handle messages from the webview
        webviewView.webview.onDidReceiveMessage(
            message => {
                switch (message.type) {
                    case 'openPromptStudio':
                        this.openPromptStudioInBrowser();
                        break;
                    case 'testConnection':
                        this.testConnection();
                        break;
                }
            },
            undefined,
            []
        );
    }

    public show() {
        if (this._view) {
            this._view.show?.(true);
        }
    }

    private async openPromptStudioInBrowser() {
        const config = vscode.workspace.getConfiguration('promptstudio');
        const serverUrl = config.get('serverUrl', 'http://localhost:5131');
        
        try {
            await vscode.env.openExternal(vscode.Uri.parse(serverUrl));
        } catch (error) {
            vscode.window.showErrorMessage(`Failed to open PromptStudio: ${error}`);
        }
    }

    private async testConnection() {
        try {
            const isConnected = await this.promptService.testConnection();
            if (isConnected) {
                vscode.window.showInformationMessage('✅ Connected to PromptStudio server!');
            } else {
                vscode.window.showWarningMessage('⚠️ Cannot connect to PromptStudio server. Please ensure it is running.');
            }
        } catch (error) {
            vscode.window.showErrorMessage(`Connection test failed: ${error}`);
        }
    }

    private _getHtmlForWebview(webview: vscode.Webview) {
        const config = vscode.workspace.getConfiguration('promptstudio');
        const serverUrl = config.get('serverUrl', 'http://localhost:5131');

        return `<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>PromptStudio</title>
    <style>
        body {
            font-family: var(--vscode-font-family);
            font-size: var(--vscode-font-size);
            color: var(--vscode-foreground);
            background-color: var(--vscode-editor-background);
            margin: 0;
            padding: 16px;
        }
        .container {
            max-width: 100%;
        }
        .header {
            text-align: center;
            margin-bottom: 24px;
        }
        .logo {
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 8px;
        }
        .description {
            color: var(--vscode-descriptionForeground);
            margin-bottom: 16px;
        }
        .button {
            display: inline-block;
            padding: 8px 16px;
            background-color: var(--vscode-button-background);
            color: var(--vscode-button-foreground);
            border: none;
            border-radius: 4px;
            cursor: pointer;
            text-decoration: none;
            margin: 4px;
            font-size: 14px;
        }
        .button:hover {
            background-color: var(--vscode-button-hoverBackground);
        }
        .button.secondary {
            background-color: var(--vscode-button-secondaryBackground);
            color: var(--vscode-button-secondaryForeground);
        }
        .button.secondary:hover {
            background-color: var(--vscode-button-secondaryHoverBackground);
        }
        .section {
            margin: 16px 0;
            padding: 16px;
            border: 1px solid var(--vscode-panel-border);
            border-radius: 4px;
        }
        .section h3 {
            margin-top: 0;
            color: var(--vscode-textPreformat-foreground);
        }
        .feature-list {
            list-style-type: none;
            padding: 0;
        }
        .feature-list li {
            margin: 8px 0;
            padding-left: 20px;
            position: relative;
        }
        .feature-list li:before {
            content: "✨";
            position: absolute;
            left: 0;
        }
        .usage-example {
            background-color: var(--vscode-textCodeBlock-background);
            padding: 12px;
            border-radius: 4px;
            font-family: var(--vscode-editor-font-family);
            font-size: var(--vscode-editor-font-size);
            margin: 8px 0;
        }
        .status {
            display: flex;
            align-items: center;
            margin: 8px 0;
        }
        .status-indicator {
            width: 8px;
            height: 8px;
            border-radius: 50%;
            margin-right: 8px;
        }
        .status-connected {
            background-color: #4CAF50;
        }
        .status-disconnected {
            background-color: #f44336;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <div class="logo">🚀 PromptStudio</div>
            <div class="description">AI Prompt Management for VS Code</div>
            
            <button class="button" onclick="openPromptStudio()">
                Open PromptStudio Web Interface
            </button>
            <button class="button secondary" onclick="testConnection()">
                Test Connection
            </button>
        </div>

        <div class="section">
            <h3>📋 Features</h3>
            <ul class="feature-list">
                <li>Manage AI prompt templates</li>
                <li>Execute prompts with GitHub Copilot</li>
                <li>Organize prompts in collections</li>
                <li>Variable substitution support</li>
                <li>Chat integration with @promptstudio</li>
            </ul>
        </div>

        <div class="section">
            <h3>💬 Chat Integration</h3>
            <p>Use PromptStudio directly in Copilot Chat:</p>
            <div class="usage-example">@promptstudio list</div>
            <div class="usage-example">@promptstudio execute "My Prompt"</div>
            <div class="usage-example">@promptstudio search keyword</div>
        </div>

        <div class="section">
            <h3>⚙️ Server Configuration</h3>
            <p>Server URL: <code>${serverUrl}</code></p>
            <div class="status">
                <div class="status-indicator status-disconnected" id="statusIndicator"></div>
                <span id="statusText">Click "Test Connection" to check server status</span>
            </div>
        </div>

        <div class="section">
            <h3>🚀 Quick Start</h3>
            <ol>
                <li>Start the PromptStudio server: <code>dotnet run</code></li>
                <li>Use the tree view to browse prompts</li>
                <li>Create new prompts with variables</li>
                <li>Execute prompts in Copilot Chat with <code>@promptstudio</code></li>
            </ol>
        </div>
    </div>

    <script>
        const vscode = acquireVsCodeApi();

        function openPromptStudio() {
            vscode.postMessage({ type: 'openPromptStudio' });
        }

        function testConnection() {
            vscode.postMessage({ type: 'testConnection' });
        }

        // Update status indicator based on connection
        window.addEventListener('message', event => {
            const message = event.data;
            switch (message.type) {
                case 'connectionStatus':
                    updateConnectionStatus(message.connected);
                    break;
            }
        });

        function updateConnectionStatus(connected) {
            const indicator = document.getElementById('statusIndicator');
            const text = document.getElementById('statusText');
            
            if (connected) {
                indicator.className = 'status-indicator status-connected';
                text.textContent = 'Connected to PromptStudio server';
            } else {
                indicator.className = 'status-indicator status-disconnected';
                text.textContent = 'Not connected to server';
            }
        }
    </script>
</body>
</html>`;
    }
}
