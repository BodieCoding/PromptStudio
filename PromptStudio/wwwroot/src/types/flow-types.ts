// Note: We'll import ReactFlow types when they're available
// For now, define our own interfaces that are compatible

// Core node types
export enum NodeType {
  PROMPT = 'prompt',
  VARIABLE = 'variable',
  CONDITIONAL = 'conditional',
  TRANSFORM = 'transform',
  OUTPUT = 'output',
  LLM_CALL = 'llm_call',
  TEMPLATE = 'template'
}

// Base interfaces
export interface BaseNodeData {
  label: string;
  description?: string;
}

// Temporary ReactFlow-compatible interfaces until ReactFlow types are properly loaded
export interface Position {
  x: number;
  y: number;
}

export interface Connection {
  source: string;
  target: string;
  sourceHandle?: string;
  targetHandle?: string;
}

export interface VariableReference {
  name: string;
  nodeId?: string;
}

export interface ValidationRule {
  type: 'required' | 'minLength' | 'maxLength' | 'pattern' | 'custom';
  value?: any;
  message: string;
}

// Specific node data interfaces
export interface PromptNodeData extends BaseNodeData {
  content: string;
  model: string;
  parameters: {
    temperature: number;
    maxTokens: number;
    topP: number;
  };
  systemMessage?: string;
  variables: VariableReference[];
}

export interface VariableNodeData extends BaseNodeData {
  name: string;
  type: 'string' | 'number' | 'boolean' | 'json';
  defaultValue?: any;
  validation?: ValidationRule[];
}

export interface ConditionalNodeData extends BaseNodeData {
  condition: {
    leftOperand: string | VariableReference;
    operator: 'equals' | 'contains' | 'greater_than' | 'less_than' | 'exists';
    rightOperand: string | VariableReference;
  };
}

export interface TransformNodeData extends BaseNodeData {
  transformType: 'format' | 'filter' | 'map' | 'custom';
  code?: string;
  parameters?: Record<string, any>;
}

export interface OutputNodeData extends BaseNodeData {
  format: 'text' | 'json' | 'markdown';
  template?: string;
}

export interface LLMCallNodeData extends BaseNodeData {
  provider: string;
  model: string;
  prompt: string;
  parameters: Record<string, any>;
}

export interface TemplateNodeData extends BaseNodeData {
  templateId: string;
  variables: Record<string, any>;
}

// Union type for all node data
export type FlowNodeData = 
  | PromptNodeData 
  | VariableNodeData 
  | ConditionalNodeData 
  | TransformNodeData 
  | OutputNodeData 
  | LLMCallNodeData 
  | TemplateNodeData;

// Flow-specific types
export interface FlowNode {
  id: string;
  type: NodeType;
  position: Position;
  data: FlowNodeData;
  selected?: boolean;
  dragging?: boolean;
}

export interface FlowEdge {
  id: string;
  source: string;
  target: string;
  sourceHandle?: string;
  targetHandle?: string;
  type?: string;
  animated?: boolean;
  selected?: boolean;
}

export interface PromptFlow {
  id: string;
  name: string;
  description?: string;
  nodes: FlowNode[];
  edges: FlowEdge[];
  variables: Record<string, any>;
  version: string;
  createdAt: Date;
  updatedAt: Date;
  tags: string[];
}

// Execution types
export interface FlowExecutionContext {
  flowId: string;
  variables: Record<string, any>;
  executionHistory: NodeExecution[];
  options: FlowExecutionOptions;
}

export interface NodeExecution {
  nodeId: string;
  startTime: Date;
  endTime?: Date;
  input: any;
  output: any;
  status: 'pending' | 'running' | 'completed' | 'failed';
  error?: string;
  duration?: number;
}

export interface FlowExecutionOptions {
  timeout?: number;
  debug?: boolean;
  validateOnly?: boolean;
}

export interface FlowExecutionResult {
  success: boolean;
  output: any;
  executionTime: number;
  nodeExecutions: NodeExecution[];
  error?: string;
}

export interface NodeExecutionResult {
  success: boolean;
  output: any;
  error?: string;
  duration: number;
}

export interface FlowValidationResult {
  isValid: boolean;
  errors: ValidationError[];
  warnings: ValidationWarning[];
}

export interface ValidationError {
  nodeId?: string;
  message: string;
  type: 'connection' | 'data' | 'logic' | 'syntax';
}

export interface ValidationWarning {
  nodeId?: string;
  message: string;
  type: 'performance' | 'best_practice' | 'optimization';
}

// API types
export interface CreateFlowRequest {
  name: string;
  description?: string;
  nodes: FlowNode[];
  edges: FlowEdge[];
  variables?: Record<string, any>;
  tags?: string[];
}

export interface UpdateFlowRequest extends Partial<CreateFlowRequest> {
  id: string;
}

export interface ExecuteFlowRequest {
  flowId: string;
  variables: Record<string, any>;
  options?: FlowExecutionOptions;
}

export interface FlowListResponse {
  flows: PromptFlow[];
  total: number;
  page: number;
  pageSize: number;
}

// UI state types
export interface FlowEditorState {
  selectedNode: FlowNode | null;
  selectedEdge: FlowEdge | null;
  isDragging: boolean;
  isExecuting: boolean;
  executionResults: Record<string, NodeExecutionResult>;
  validationResults: FlowValidationResult | null;
}

export interface NodePropertyPanelProps {
  node: FlowNode;
  onUpdate: (node: FlowNode) => void;
  onClose: () => void;
}

export interface FlowCanvasProps {
  flow: PromptFlow;
  onFlowChange: (flow: PromptFlow) => void;
  readonly?: boolean;
  onNodeSelect?: (node: FlowNode | null) => void;
  onEdgeSelect?: (edge: FlowEdge | null) => void;
}

// LLM Provider types
export interface LLMProvider {
  id: string;
  name: string;
  models: LLMModel[];
  supportsStreaming: boolean;
  maxTokens: number;
}

export interface LLMModel {
  id: string;
  name: string;
  description?: string;
  contextLength: number;
  costPer1kTokens: number;
}

export interface LLMResponse {
  content: string;
  model: string;
  usage: {
    promptTokens: number;
    completionTokens: number;
    totalTokens: number;
  };
  cost: number;
  latency: number;
}
