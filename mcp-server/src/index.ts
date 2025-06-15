#!/usr/bin/env node

import { Server } from '@modelcontextprotocol/sdk/server/index.js';
import { StdioServerTransport } from '@modelcontextprotocol/sdk/server/stdio.js';
import {
  CallToolRequestSchema,
  ListToolsRequestSchema,
  Tool,
} from '@modelcontextprotocol/sdk/types.js';
import axios from 'axios';
import { z } from 'zod';

// Configuration
const PROMPTSTUDIO_BASE_URL = process.env.PROMPTSTUDIO_URL || 'http://localhost:5131';

// API client for PromptStudio
class PromptStudioClient {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }  async getCollections() {
    const response = await axios.get(`${this.baseURL}/api/prompts/collections`);
    return response.data;
  }

  async getCollection(id: number) {
    const response = await axios.get(`${this.baseURL}/api/prompts/collections/${id}`);
    return response.data;
  }

  async getPromptTemplates(collectionId?: number) {
    const url = collectionId
      ? `${this.baseURL}/api/prompts/prompts?collectionId=${collectionId}`
      : `${this.baseURL}/api/prompts/prompts`;
    const response = await axios.get(url);
    return response.data;
  }

  async getPromptTemplate(id: number) {
    const response = await axios.get(`${this.baseURL}/api/prompts/prompts/${id}`);
    return response.data;
  }

  async executePrompt(templateId: number, variables: Record<string, string>) {
    const response = await axios.post(`${this.baseURL}/api/prompts/prompts/${templateId}/execute`, {
      variables
    });
    return response.data;
  }
  async createPromptTemplate(data: {
    name: string;
    description?: string;
    content: string;
    collectionId: number;
  }) {
    const response = await axios.post(`${this.baseURL}/api/prompts/prompts`, data);
    return response.data;
  }

  async executeBatch(promptId: number, collectionId: number) {
    const response = await axios.post(`${this.baseURL}/api/prompts/variable-collections/${collectionId}/execute`, {
      promptId
    });
    return response.data;
  }
  
  async generateCsvTemplate(templateId: number): Promise<string> {
    const response = await axios.get(`${this.baseURL}/api/prompts/prompt-templates/${templateId}/csv-template`, {
      responseType: 'text'
    });
    return response.data;
  }

  async createVariableCollectionFromCsv(promptId: number, name: string, description: string, csvData: string) {
    const response = await axios.post(`${this.baseURL}/api/prompts/variable-collections`, {
      promptId,
      name,
      description,
      csvData
    });
    return response.data;
  }

  async getVariableCollections(promptId: number) {
    const response = await axios.get(`${this.baseURL}/api/prompts/variable-collections?promptId=${promptId}`);
    return response.data;
  }
  async getExecutionHistory(promptId?: number, limit: number = 50) {
    const url = promptId
      ? `${this.baseURL}/api/prompts/executions?promptId=${promptId}&limit=${limit}`
      : `${this.baseURL}/api/prompts/executions?limit=${limit}`;
    const response = await axios.get(url);
    return response.data;
  }
}

const client = new PromptStudioClient(PROMPTSTUDIO_BASE_URL);

// Validation schemas
const ExecutePromptSchema = z.object({
  templateId: z.number().int().positive(),
  variables: z.record(z.string()),
});

const CreatePromptSchema = z.object({
  name: z.string().min(1).max(100),
  description: z.string().optional(),
  content: z.string().min(1),
  collectionId: z.number().int().positive(),
});

const BatchExecuteSchema = z.object({
  promptId: z.number().int().positive(),
  collectionId: z.number().int().positive(),
});

// MCP Server setup
const server = new Server(
  {
    name: 'promptstudio-mcp',
    version: '1.0.0',
  },
  {
    capabilities: {
      tools: {},
    },
  }
);

// Define available tools
const tools: Tool[] = [
  {
    name: 'prompt_templates_list',
    description: 'List all prompt templates, optionally filtered by collection',
    inputSchema: {
      type: 'object',
      properties: {
        collectionId: {
          type: 'number',
          description: 'Optional collection ID to filter templates',
        },
      },
    },
  },
  {
    name: 'prompt_templates_get',
    description: 'Get details of a specific prompt template including variables',
    inputSchema: {
      type: 'object',
      properties: {
        templateId: {
          type: 'number',
          description: 'ID of the prompt template to retrieve',
        },
      },
      required: ['templateId'],
    },
  },
  {
    name: 'prompt_templates_execute',
    description: 'Execute a prompt template with provided variable values',
    inputSchema: {
      type: 'object',
      properties: {
        templateId: {
          type: 'number',
          description: 'ID of the prompt template to execute',
        },
        variables: {
          type: 'object',
          description: 'Key-value pairs for template variables',
          additionalProperties: {
            type: 'string',
          },
        },
      },
      required: ['templateId', 'variables'],
    },
  },
  {
    name: 'prompt_templates_create',
    description: 'Create a new prompt template',
    inputSchema: {
      type: 'object',
      properties: {
        name: {
          type: 'string',
          description: 'Name of the prompt template',
        },
        description: {
          type: 'string',
          description: 'Optional description of the prompt template',
        },
        content: {
          type: 'string',
          description: 'Prompt content with {{variable}} placeholders',
        },
        collectionId: {
          type: 'number',
          description: 'ID of the collection to add the template to',
        },
      },
      required: ['name', 'content', 'collectionId'],
    },
  },
  {
    name: 'collections_list',
    description: 'List all prompt collections',
    inputSchema: {
      type: 'object',
      properties: {},
    },
  },
  {
    name: 'collections_get',
    description: 'Get details of a specific collection including its prompts',
    inputSchema: {
      type: 'object',
      properties: {
        collectionId: {
          type: 'number',
          description: 'ID of the collection to retrieve',
        },
      },
      required: ['collectionId'],
    },
  },
  {
    name: 'variable_collections_execute',
    description: 'Execute a prompt template with a batch of variable sets from a CSV collection',
    inputSchema: {
      type: 'object',
      properties: {
        promptId: {
          type: 'number',
          description: 'ID of the prompt template to execute',
        },
        collectionId: {
          type: 'number',
          description: 'ID of the variable collection to use',
        },
      },
      required: ['promptId', 'collectionId'],
    },
  },
  {
    name: 'execution_history_list',
    description: 'Get execution history, optionally filtered by prompt template',
    inputSchema: {
      type: 'object',
      properties: {
        promptId: {
          type: 'number',
          description: 'Optional prompt template ID to filter history',
        },
        limit: {
          type: 'number',
          description: 'Maximum number of executions to return (default: 50)',
          default: 50,
        },
      },
    },
  },
  {
    name: 'csv_template_generate',
    description: 'Generate a CSV template for a prompt template with sample data',
    inputSchema: {
      type: 'object',
      properties: {
        templateId: {
          type: 'number',
          description: 'ID of the prompt template to generate CSV template for',
        },
      },
      required: ['templateId'],
    },
  },
  {
    name: 'variable_collection_create_from_csv',
    description: 'Create a new variable collection from CSV data',
    inputSchema: {
      type: 'object',
      properties: {
        promptId: {
          type: 'number',
          description: 'ID of the prompt template',
        },
        name: {
          type: 'string',
          description: 'Name for the variable collection',
        },
        description: {
          type: 'string',
          description: 'Optional description for the collection',
        },
        csvData: {
          type: 'string',
          description: 'CSV data with headers matching prompt variables',
        },
      },
      required: ['promptId', 'name', 'csvData'],
    },
  },
  {
    name: 'variable_collections_list',
    description: 'List variable collections for a specific prompt template',
    inputSchema: {
      type: 'object',
      properties: {
        promptId: {
          type: 'number',
          description: 'ID of the prompt template',
        },
      },
      required: ['promptId'],
    },
  },
];

// Handle tool listing
server.setRequestHandler(ListToolsRequestSchema, async () => {
  return { tools };
});

// Handle tool execution
server.setRequestHandler(CallToolRequestSchema, async (request) => {
  const { name, arguments: args } = request.params;

  try {
    switch (name) {
      case 'prompt_templates_list': {
        const collectionId = args?.collectionId as number | undefined;
        const templates = await client.getPromptTemplates(collectionId);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(templates, null, 2),
            },
          ],
        };
      }

      case 'prompt_templates_get': {
        const templateId = args?.templateId as number;
        if (!templateId) {
          throw new Error('templateId is required');
        }
        const template = await client.getPromptTemplate(templateId);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(template, null, 2),
            },
          ],
        };
      }

      case 'prompt_templates_execute': {
        const validated = ExecutePromptSchema.parse(args);
        const result = await client.executePrompt(validated.templateId, validated.variables);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(result, null, 2),
            },
          ],
        };
      }

      case 'prompt_templates_create': {
        const validated = CreatePromptSchema.parse(args);
        const template = await client.createPromptTemplate(validated);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(template, null, 2),
            },
          ],
        };
      }

      case 'collections_list': {
        const collections = await client.getCollections();
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(collections, null, 2),
            },
          ],
        };
      }

      case 'collections_get': {
        const collectionId = args?.collectionId as number;
        if (!collectionId) {
          throw new Error('collectionId is required');
        }
        const collection = await client.getCollection(collectionId);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(collection, null, 2),
            },
          ],
        };
      }

      case 'variable_collections_execute': {
        const validated = BatchExecuteSchema.parse(args);
        const result = await client.executeBatch(validated.promptId, validated.collectionId);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(result, null, 2),
            },
          ],
        };
      }      case 'csv_template_generate': {
        const templateId = args?.templateId as number;
        if (!templateId) {
          throw new Error('templateId is required');
        }
        const csvContent = await client.generateCsvTemplate(templateId);
        return {
          content: [
            {
              type: 'text',
              text: `CSV Template for Prompt Template ${templateId}:\n\n${csvContent}`,
            },
          ],
        };
      }

      case 'variable_collection_create_from_csv': {
        const promptId = args?.promptId as number;
        const name = args?.name as string;
        const description = args?.description as string;
        const csvData = args?.csvData as string;

        if (!promptId || !name || !csvData) {
          throw new Error('promptId, name, and csvData are required');
        }

        const result = await client.createVariableCollectionFromCsv(promptId, name, description, csvData);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(result, null, 2),
            },
          ],
        };
      }

      case 'variable_collections_list': {
        const promptId = args?.promptId as number;
        if (!promptId) {
          throw new Error('promptId is required');
        }
        const collections = await client.getVariableCollections(promptId);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(collections, null, 2),
            },
          ],
        };
      }

      case 'execution_history_list': {
        const promptId = args?.promptId as number | undefined;
        const limit = (args?.limit as number) || 50;
        const history = await client.getExecutionHistory(promptId, limit);
        return {
          content: [
            {
              type: 'text',
              text: JSON.stringify(history, null, 2),
            },
          ],
        };
      }

      default:
        throw new Error(`Unknown tool: ${name}`);
    }
  } catch (error) {
    const errorMessage = error instanceof Error ? error.message : 'Unknown error';
    return {
      content: [
        {
          type: 'text',
          text: `Error: ${errorMessage}`,
        },
      ],
      isError: true,
    };
  }
});

// Start the server
async function main() {
  try {
    console.error('Starting PromptStudio MCP server...');
    const transport = new StdioServerTransport();
    console.error('Created transport, connecting...');
    await server.connect(transport);
    console.error('PromptStudio MCP server running on stdio');
  } catch (error) {
    console.error('Failed to start server:', error);
    process.exit(1);
  }
}

main().catch((error) => {
  console.error('Server error:', error);
  process.exit(1);
});
