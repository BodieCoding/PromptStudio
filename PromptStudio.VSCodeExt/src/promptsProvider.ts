import * as vscode from 'vscode';
import { McpService, PromptTemplate, PromptCollection } from './mcpService';

export type TreeItem = PromptItem | CollectionItem | UngroupedPromptsItem | ErrorItem;

export class PromptsProvider implements vscode.TreeDataProvider<TreeItem> {
    private _onDidChangeTreeData: vscode.EventEmitter<TreeItem | undefined | null | void> = new vscode.EventEmitter<TreeItem | undefined | null | void>();
    readonly onDidChangeTreeData: vscode.Event<TreeItem | undefined | null | void> = this._onDidChangeTreeData.event;

    constructor(private promptService: McpService) {}

    refresh(): void {
        this._onDidChangeTreeData.fire();
    }

    getTreeItem(element: TreeItem): vscode.TreeItem {
        return element;
    }

    async getChildren(element?: TreeItem): Promise<TreeItem[]> {
        if (!element) {
            // Root level - show collections and ungrouped prompts
            try {
                const [collections, allPrompts] = await Promise.all([
                    this.promptService.getCollections(),
                    this.promptService.getPromptTemplates()
                ]);

                const items: TreeItem[] = [];

                // Add collections
                for (const collection of collections) {
                    items.push(new CollectionItem(collection));
                }

                // Add ungrouped prompts
                const ungroupedPrompts = allPrompts.filter(p => !p.collectionId);
                if (ungroupedPrompts.length > 0) {
                    items.push(new UngroupedPromptsItem(ungroupedPrompts));
                }

                return items;
            } catch (error) {
                vscode.window.showErrorMessage(`Failed to load prompts: ${error}`);
                return [new ErrorItem(error instanceof Error ? error.message : 'Unknown error')];
            }
        } else if (element instanceof CollectionItem) {
            // Show prompts in collection
            try {
                const prompts = await this.promptService.getPromptTemplates(element.collection.id);
                return prompts.map(prompt => new PromptItem(prompt));
            } catch (error) {
                return [new ErrorItem(`Failed to load prompts: ${error}`)];
            }
        } else if (element instanceof UngroupedPromptsItem) {
            // Show ungrouped prompts
            return element.prompts.map(prompt => new PromptItem(prompt));
        }

        return [];
    }

    async createNewPrompt(): Promise<void> {
        const name = await vscode.window.showInputBox({
            prompt: 'Enter prompt name',
            validateInput: (value) => {
                if (!value || value.trim().length === 0) {
                    return 'Prompt name is required';
                }
                return null;
            }
        });

        if (!name) {
            return;
        }        const description = await vscode.window.showInputBox({
            prompt: 'Enter prompt description (optional)',
        });

        // Get collections for selection
        const collections = await this.promptService.getCollections();
        let collectionId = 1; // Default collection
        
        if (collections.length > 0) {
            const collectionItems = collections.map(c => ({ label: c.name, description: c.description, id: c.id }));
            collectionItems.unshift({ label: 'Default Collection', description: 'No specific collection', id: 1 });
            
            const selectedCollection = await vscode.window.showQuickPick(collectionItems, {
                placeHolder: 'Select a collection for this prompt'
            });
            
            if (selectedCollection) {
                collectionId = selectedCollection.id;
            }
        }

        const content = await vscode.window.showInputBox({
            prompt: 'Enter prompt content (use {{variableName}} for variables)',
            validateInput: (value) => {
                if (!value || value.trim().length === 0) {
                    return 'Prompt content is required';
                }
                return null;
            }
        });

        if (!content) {
            return;
        }        try {
            const newPrompt = await this.promptService.createPromptTemplate(
                name.trim(),
                content.trim(),
                collectionId,
                description?.trim()
            );

            if (newPrompt) {
                vscode.window.showInformationMessage(`Prompt "${newPrompt.name}" created successfully!`);
                this.refresh();
            } else {
                vscode.window.showErrorMessage('Failed to create prompt: No response from server');
            }
        } catch (error) {
            vscode.window.showErrorMessage(`Failed to create prompt: ${error}`);
        }
    }
}

export class PromptItem extends vscode.TreeItem {
    constructor(
        public readonly prompt: PromptTemplate,
        public readonly collapsibleState: vscode.TreeItemCollapsibleState = vscode.TreeItemCollapsibleState.None
    ) {
        super(prompt.name, collapsibleState);
        this.tooltip = prompt.description || prompt.name;
        this.description = prompt.description;
        this.contextValue = 'prompt';
        
        // Add icon and command
        this.iconPath = new vscode.ThemeIcon('symbol-text');
        this.command = {
            command: 'promptstudio.executePrompt',
            title: 'Execute Prompt',
            arguments: [this]
        };
    }
}

export class CollectionItem extends vscode.TreeItem {
    constructor(
        public readonly collection: PromptCollection,
        public readonly collapsibleState: vscode.TreeItemCollapsibleState = vscode.TreeItemCollapsibleState.Collapsed
    ) {
        super(collection.name, collapsibleState);
        this.tooltip = collection.description || collection.name;
        this.description = collection.description;
        this.contextValue = 'collection';
        this.iconPath = new vscode.ThemeIcon('folder');
    }
}

export class UngroupedPromptsItem extends vscode.TreeItem {
    constructor(
        public readonly prompts: PromptTemplate[],
        public readonly collapsibleState: vscode.TreeItemCollapsibleState = vscode.TreeItemCollapsibleState.Collapsed
    ) {
        super('Ungrouped Prompts', collapsibleState);
        this.tooltip = 'Prompts not assigned to any collection';
        this.description = `${prompts.length} prompts`;
        this.contextValue = 'ungrouped';
        this.iconPath = new vscode.ThemeIcon('folder-opened');
    }
}

export class ErrorItem extends vscode.TreeItem {
    constructor(
        public readonly error: string,
        public readonly collapsibleState: vscode.TreeItemCollapsibleState = vscode.TreeItemCollapsibleState.None
    ) {
        super('Error loading prompts', collapsibleState);
        this.tooltip = error;
        this.description = 'Click to retry';
        this.iconPath = new vscode.ThemeIcon('error');
        this.command = {
            command: 'promptstudio.refreshPrompts',
            title: 'Retry'
        };
    }
}
