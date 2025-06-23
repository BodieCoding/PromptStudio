import { FlowNode, NodeType, DataType, FlowSuggestion, ConnectionRule, OutputFormat } from '../types/flow-types';

/**
 * Intelligent Flow Orchestration Service
 * Provides contextual suggestions and validates connections based on data flow logic
 */
export class FlowOrchestrationService {
  
  // Define connection rules for logical flow patterns
  private static connectionRules: ConnectionRule[] = [
    // Prompt to processing nodes
    {
      sourceType: NodeType.PROMPT,
      targetType: NodeType.CONDITIONAL,
      suggestion: "Add conditional logic to branch based on prompt output"
    },
    {
      sourceType: NodeType.PROMPT,
      targetType: NodeType.TRANSFORM,
      suggestion: "Transform the prompt output format or extract specific data"
    },
    {
      sourceType: NodeType.PROMPT,
      targetType: NodeType.OUTPUT,
      suggestion: "Output the prompt result directly"
    },
    
    // List processing patterns
    {
      sourceType: NodeType.PROMPT,
      targetType: NodeType.TRANSFORM,
      condition: (source, target) => {
        const promptData = source.data as any;
        return promptData.expectedFormat === OutputFormat.STRUCTURED_LIST;
      },
      suggestion: "Split list into individual items for processing"
    },
    
    // Variable to prompt connections
    {
      sourceType: NodeType.VARIABLE,
      targetType: NodeType.PROMPT,
      suggestion: "Use variable as input to customize the prompt"
    },
    
    // Transform to conditional patterns
    {
      sourceType: NodeType.TRANSFORM,
      targetType: NodeType.CONDITIONAL,
      suggestion: "Apply conditional logic to transformed data"
    }
  ];

  /**
   * Get intelligent suggestions for next nodes based on current context
   */
  static getSuggestedNextNodes(
    sourceNode: FlowNode,
    existingNodes: FlowNode[],
    position: { x: number; y: number }
  ): FlowSuggestion[] {
    const suggestions: FlowSuggestion[] = [];
    
    // Analyze source node to determine logical next steps
    switch (sourceNode.type) {
      case NodeType.PROMPT:
        suggestions.push(...this.getPromptNodeSuggestions(sourceNode, existingNodes));
        break;
        
      case NodeType.VARIABLE:
        suggestions.push(...this.getVariableNodeSuggestions(sourceNode, existingNodes));
        break;
        
      case NodeType.CONDITIONAL:
        suggestions.push(...this.getConditionalNodeSuggestions(sourceNode, existingNodes));
        break;
        
      case NodeType.TRANSFORM:
        suggestions.push(...this.getTransformNodeSuggestions(sourceNode, existingNodes));
        break;
    }
    
    // Sort by priority and return top suggestions
    return suggestions.sort((a, b) => b.priority - a.priority).slice(0, 5);
  }

  private static getPromptNodeSuggestions(
    sourceNode: FlowNode,
    existingNodes: FlowNode[]
  ): FlowSuggestion[] {
    const promptData = sourceNode.data as any;
    const suggestions: FlowSuggestion[] = [];
      // Check if prompt is configured to output a list
    if (this.isListOutputPrompt(promptData)) {
      suggestions.push({
        nodeType: NodeType.FOR_EACH,
        reason: "Iterate over each item in the generated list",
        priority: 95,
        autoConnect: true,
        defaultConfig: {
          label: "Process Each Item",
          sourceVariable: "result",
          itemVariable: "item",
          iterationMode: 'sequential'
        }
      });
      
      suggestions.push({
        nodeType: NodeType.TRANSFORM,
        reason: "Parse and split the list output into individual items",
        priority: 90,
        autoConnect: true,
        defaultConfig: {
          label: "Split List",
          transformType: 'custom',
          code: `
// Split list output into individual items
const items = input.split('\\n').filter(item => item.trim());
return items.map(item => ({ value: item.trim(), index: items.indexOf(item) }));
          `.trim()
        }
      });
      
      suggestions.push({
        nodeType: NodeType.CONDITIONAL,
        reason: "Apply conditional logic to each list item",
        priority: 85,
        defaultConfig: {
          label: "Filter List Items",
          condition: {
            leftOperand: "item.length",
            operator: 'greater_than',
            rightOperand: "0"
          }
        }
      });
    }
    
    // Check for text analysis patterns
    if (this.isAnalysisPrompt(promptData)) {
      suggestions.push({
        nodeType: NodeType.CONDITIONAL,
        reason: "Branch based on analysis results (positive/negative, categories, etc.)",
        priority: 80,
        autoConnect: true
      });
    }
    
    // Always suggest output as an option
    suggestions.push({
      nodeType: NodeType.OUTPUT,
      reason: "Format and display the prompt results",
      priority: 70,
      autoConnect: true,
      defaultConfig: {
        label: "Display Results",
        format: 'text'
      }
    });
    
    return suggestions;
  }
  private static getVariableNodeSuggestions(
    sourceNode: FlowNode,
    existingNodes: FlowNode[]
  ): FlowSuggestion[] {
    const variableData = sourceNode.data as any;
    const suggestions: FlowSuggestion[] = [];
    
    // Check if variable might contain a list/array
    if (this.isListVariable(variableData)) {
      suggestions.push({
        nodeType: NodeType.FOR_EACH,
        reason: "Iterate over each item in this list/array variable",
        priority: 95,
        autoConnect: true,
        defaultConfig: {
          label: `Process Each ${variableData.name || 'Item'}`,
          sourceVariable: variableData.name || 'list',
          itemVariable: 'item',
          iterationMode: 'sequential'
        }
      });
    }
    
    suggestions.push(
      {
        nodeType: NodeType.PROMPT,
        reason: "Use this variable to personalize or parameterize a prompt",
        priority: 95,
        autoConnect: true,
        defaultConfig: {
          label: "Dynamic Prompt",
          content: `Please analyze the following: {{${(sourceNode.data as any).name || 'input'}}}`
        }
      },
      {
        nodeType: NodeType.CONDITIONAL,
        reason: "Apply conditional logic based on variable value",
        priority: 80,
        defaultConfig: {
          label: "Check Variable",
          condition: {
            leftOperand: (sourceNode.data as any).name || 'input',
            operator: 'exists',
            rightOperand: 'true'
          }
        }
      }
    );
    
    return suggestions;
  }

  private static getConditionalNodeSuggestions(
    sourceNode: FlowNode,
    existingNodes: FlowNode[]
  ): FlowSuggestion[] {
    return [
      {
        nodeType: NodeType.PROMPT,
        reason: "Create different prompts for each conditional branch",
        priority: 85,
        defaultConfig: {
          label: "Branch-Specific Prompt"
        }
      },
      {
        nodeType: NodeType.TRANSFORM,
        reason: "Apply different transformations based on condition results",
        priority: 80,
        defaultConfig: {
          label: "Conditional Transform"
        }
      },
      {
        nodeType: NodeType.OUTPUT,
        reason: "Output different results for each condition",
        priority: 75,
        defaultConfig: {
          label: "Conditional Output"
        }
      }
    ];
  }

  private static getTransformNodeSuggestions(
    sourceNode: FlowNode,
    existingNodes: FlowNode[]
  ): FlowSuggestion[] {
    const transformData = sourceNode.data as any;
    const suggestions: FlowSuggestion[] = [];
    
    // If this transform splits data, suggest processing each item
    if (this.isListTransform(transformData)) {
      suggestions.push({
        nodeType: NodeType.CONDITIONAL,
        reason: "Apply conditional logic to each transformed item",
        priority: 85,
        autoConnect: true
      });
      
      suggestions.push({
        nodeType: NodeType.PROMPT,
        reason: "Process each item with a specialized prompt",
        priority: 80,
        autoConnect: true,
        defaultConfig: {
          label: "Process Item",
          content: "Analyze this item: {{item}}"
        }
      });
    }
    
    suggestions.push({
      nodeType: NodeType.OUTPUT,
      reason: "Display the transformed results",
      priority: 70,
      autoConnect: true
    });
    
    return suggestions;
  }

  /**
   * Validate if a connection between two nodes makes logical sense
   */
  static validateConnection(
    sourceNode: FlowNode,
    targetNode: FlowNode,
    sourceHandle?: string,
    targetHandle?: string
  ): { valid: boolean; message?: string; suggestion?: string } {
    
    // Check for circular dependencies
    if (this.wouldCreateCircularDependency(sourceNode, targetNode)) {
      return {
        valid: false,
        message: "This connection would create a circular dependency"
      };
    }
    
    // Check data type compatibility
    const compatibility = this.checkDataTypeCompatibility(sourceNode, targetNode);
    if (!compatibility.compatible) {
      return {
        valid: false,
        message: compatibility.message,
        suggestion: compatibility.suggestion
      };
    }
    
    // Check for logical flow patterns
    const rule = this.connectionRules.find(rule => 
      rule.sourceType === sourceNode.type && 
      rule.targetType === targetNode.type &&
      (!rule.condition || rule.condition(sourceNode, targetNode))
    );
    
    if (rule) {
      return {
        valid: true,
        suggestion: rule.suggestion
      };
    }
    
    return { valid: true };
  }

  // Helper methods for pattern recognition
  private static isListOutputPrompt(promptData: any): boolean {
    const content = promptData.content?.toLowerCase() || '';
    return content.includes('list') || 
           content.includes('items') || 
           content.includes('bullet') || 
           content.includes('numbered') ||
           promptData.expectedFormat === OutputFormat.STRUCTURED_LIST;
  }

  private static isAnalysisPrompt(promptData: any): boolean {
    const content = promptData.content?.toLowerCase() || '';
    return content.includes('analyze') || 
           content.includes('sentiment') || 
           content.includes('classify') || 
           content.includes('categorize');
  }

  private static isListTransform(transformData: any): boolean {
    return transformData.transformType === 'split' || 
           transformData.code?.includes('split') ||
           transformData.code?.includes('map');
  }

  private static wouldCreateCircularDependency(
    sourceNode: FlowNode,
    targetNode: FlowNode
  ): boolean {
    // Simple circular dependency check - in a real implementation,
    // you'd traverse the graph to check for cycles
    return sourceNode.id === targetNode.id;
  }

  private static checkDataTypeCompatibility(
    sourceNode: FlowNode,
    targetNode: FlowNode
  ): { compatible: boolean; message?: string; suggestion?: string } {
    // This would check if the output type of source matches input type of target
    // For now, return compatible for basic implementation
    return { compatible: true };
  }

  /**
   * Auto-suggest flow completion based on current incomplete flows
   */
  static suggestFlowCompletion(nodes: FlowNode[]): FlowSuggestion[] {
    const suggestions: FlowSuggestion[] = [];
    
    // Find nodes without outputs
    const terminalNodes = nodes.filter(node => 
      !nodes.some(other => 
        // Check if any other node takes this node as input
        other.data && (other.data as any).inputs?.some((input: any) => 
          input.sourceNodeId === node.id
        )
      )
    );
    
    // Suggest output nodes for terminal nodes that aren't already outputs
    terminalNodes
      .filter(node => node.type !== NodeType.OUTPUT)
      .forEach(node => {
        suggestions.push({
          nodeType: NodeType.OUTPUT,
          reason: `Complete the flow by outputting results from ${node.data.label}`,
          priority: 60,
          autoConnect: true
        });
      });
    
    return suggestions;
  }

  private static isListVariable(variableData: any): boolean {
    const name = variableData.name?.toLowerCase() || '';
    const defaultValue = variableData.defaultValue || '';
    
    // Check if variable name suggests it's a list
    if (name.includes('list') || name.includes('array') || name.includes('items')) {
      return true;
    }
    
    // Check if default value looks like a list (comma-separated, JSON array, etc.)
    if (typeof defaultValue === 'string') {
      if (defaultValue.startsWith('[') && defaultValue.endsWith(']')) {
        return true;
      }
      if (defaultValue.includes(',') && defaultValue.split(',').length > 2) {
        return true;
      }
    }
    
    return false;
  }
}
