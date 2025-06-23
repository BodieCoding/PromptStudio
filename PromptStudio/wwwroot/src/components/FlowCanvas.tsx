import React, { useCallback, useEffect, useMemo, useState } from 'react';
import ReactFlow, {
  Node,
  Edge,
  Background,
  Controls,
  MiniMap,
  useNodesState,
  useEdgesState,
  addEdge,
  Connection,
  NodeChange,
  EdgeChange,
  applyNodeChanges,  applyEdgeChanges,
} from 'reactflow';
import 'reactflow/dist/style.css';

import { PromptFlow, FlowNode, FlowEdge, FlowCanvasProps, NodeType, FlowNodeData, PromptNodeData, VariableNodeData, ConditionalNodeData, TransformNodeData, OutputNodeData, ForEachNodeData, TemplateNodeData } from '../types/flow-types';
import PromptNode from './nodes/PromptNode';
import VariableNode from './nodes/VariableNode';
import ConditionalNode from './nodes/ConditionalNode';
import TransformNode from './nodes/TransformNode';
import OutputNode from './nodes/OutputNode';
import ForEachNode from './nodes/ForEachNode';
import TemplateNode from './nodes/TemplateNode';
import SmartContextMenu from './SmartContextMenu';
import ConnectionValidation from './ConnectionValidation';
import SmartFeaturesHelp from './SmartFeaturesHelp';

const FlowCanvas: React.FC<FlowCanvasProps> = ({
  flow,
  onFlowChange,
  onNodeSelect,
  onEdgeSelect,
  readonly = false
}) => {
  const [nodes, setNodes, onNodesChange] = useNodesState(flow.nodes as Node[]);
  const [edges, setEdges, onEdgesChange] = useEdgesState(flow.edges as Edge[]);  const nodeTypes = useMemo(() => ({
    prompt: PromptNode,
    variable: VariableNode,
    conditional: ConditionalNode,
    transform: TransformNode,
    output: OutputNode,
    for_each: ForEachNode,
    template: TemplateNode,
  }), []);

  // Smart context menu state
  const [contextMenu, setContextMenu] = useState<{
    anchorEl: HTMLElement | null;
    sourceNode: FlowNode | null;
    position: { x: number; y: number };
  }>({
    anchorEl: null,
    sourceNode: null,
    position: { x: 0, y: 0 }
  });

  // Connection validation state
  const [connectionValidation, setConnectionValidation] = useState<{
    isVisible: boolean;
    sourceNode: FlowNode | null;
    targetNode: FlowNode | null;
    position: { x: number; y: number };
  }>({
    isVisible: false,
    sourceNode: null,
    targetNode: null,
    position: { x: 0, y: 0 }
  });

  const handleNodesChange = useCallback((changes: NodeChange[]) => {
    const updatedNodes = applyNodeChanges(changes, nodes) as FlowNode[];
    setNodes(updatedNodes);
    
    if (onFlowChange) {
      onFlowChange({
        ...flow,
        nodes: updatedNodes,
        updatedAt: new Date()
      });
    }
  }, [nodes, flow, onFlowChange, setNodes]);

  const handleEdgesChange = useCallback((changes: EdgeChange[]) => {
    const updatedEdges = applyEdgeChanges(changes, edges) as FlowEdge[];
    setEdges(updatedEdges);
    
    if (onFlowChange) {
      onFlowChange({
        ...flow,
        edges: updatedEdges,
        updatedAt: new Date()
      });
    }
  }, [edges, flow, onFlowChange, setEdges]);

  const handleConnect = useCallback((connection: Connection) => {
    const newEdge = {
      ...connection,
      id: `edge-${connection.source}-${connection.target}-${Date.now()}`,
      type: 'smoothstep',
    } as FlowEdge;

    const updatedEdges = addEdge(newEdge, edges) as FlowEdge[];
    setEdges(updatedEdges);
    
    if (onFlowChange) {
      onFlowChange({
        ...flow,
        edges: updatedEdges,
        updatedAt: new Date()
      });
    }
  }, [edges, flow, onFlowChange, setEdges]);  const handleNodeClick = useCallback((event: React.MouseEvent, node: Node) => {
    console.log('Node clicked:', node.id, 'Event detail:', event.detail, 'Ctrl key:', event.ctrlKey);
    event.stopPropagation();
    
    // Show smart context menu if Ctrl/Cmd is held or if it's a double-click
    if (event.ctrlKey || event.metaKey || event.detail === 2) {
      console.log('Smart context menu triggered for node:', node.id); // Debug log
      setContextMenu({
        anchorEl: event.currentTarget as HTMLElement,
        sourceNode: node as FlowNode,
        position: { x: event.clientX, y: event.clientY }
      });
      return;
    }
    
    if (onNodeSelect) {
      onNodeSelect(node as FlowNode);
    }
  }, [onNodeSelect]);  // Smart context menu - right-click on node to show suggestions
  const handleNodeContextMenu = useCallback((event: React.MouseEvent, node: Node) => {
    console.log('Right-click detected on node:', node.id); // Debug log
    event.preventDefault();
    event.stopPropagation();
    
    setContextMenu({
      anchorEl: event.currentTarget as HTMLElement,
      sourceNode: node as FlowNode,
      position: { x: event.clientX, y: event.clientY }
    });
  }, []);

  // Add node from smart suggestions
  const handleSmartNodeAdd = useCallback((nodeType: NodeType, config?: any) => {
    const sourceNode = contextMenu.sourceNode;
    if (!sourceNode) return;

    // Calculate position near the source node
    const position = {
      x: sourceNode.position.x + 250,
      y: sourceNode.position.y
    };

    const createNodeData = (nodeType: NodeType): FlowNodeData => {
      const baseData = {
        label: config?.label || `${nodeType.charAt(0).toUpperCase() + nodeType.slice(1)} Node`,
        description: `A ${nodeType} node`
      };

      // Use provided config or create default based on type
      return { ...baseData, ...config } as FlowNodeData;
    };

    const newNode: FlowNode = {
      id: `${nodeType}-${Date.now()}`,
      type: nodeType,
      position,
      data: createNodeData(nodeType)
    };

    const updatedNodes = [...nodes, newNode] as FlowNode[];
    setNodes(updatedNodes as Node[]);

    // Auto-connect if suggested
    if (config?.autoConnect !== false) {
      const newEdge: FlowEdge = {
        id: `edge-${sourceNode.id}-${newNode.id}-${Date.now()}`,
        source: sourceNode.id,
        target: newNode.id,
        type: 'smoothstep',
      };

      const updatedEdges = [...edges, newEdge] as FlowEdge[];
      setEdges(updatedEdges as Edge[]);

      if (onFlowChange) {
        onFlowChange({
          ...flow,
          nodes: updatedNodes,
          edges: updatedEdges,
          updatedAt: new Date()
        });
        return;
      }
    }

    if (onFlowChange) {
      onFlowChange({
        ...flow,
        nodes: updatedNodes,
        updatedAt: new Date()
      });
    }
  }, [contextMenu.sourceNode, nodes, edges, flow, onFlowChange, setNodes, setEdges]);

  const handleEdgeClick = useCallback((event: React.MouseEvent, edge: Edge) => {
    if (onEdgeSelect) {
      onEdgeSelect(edge as FlowEdge);
    }
  }, [onEdgeSelect]);

  const handleCloseContextMenu = useCallback(() => {
    setContextMenu({
      anchorEl: null,
      sourceNode: null,
      position: { x: 0, y: 0 }
    });
  }, []);

  const handlePaneClick = useCallback(() => {
    handleCloseContextMenu();
    if (onNodeSelect) {
      onNodeSelect(null);
    }
    if (onEdgeSelect) {
      onEdgeSelect(null);
    }
  }, [onNodeSelect, onEdgeSelect, handleCloseContextMenu]);

  const onDragOver = useCallback((event: React.DragEvent) => {
    event.preventDefault();
    event.dataTransfer.dropEffect = 'move';
  }, []);  const handleDrop = useCallback((event: React.DragEvent) => {
    event.preventDefault();
    
    const reactFlowBounds = event.currentTarget.getBoundingClientRect();
    const nodeType = event.dataTransfer.getData('application/reactflow') as NodeType;
    
    if (!nodeType) return;
    
    // Calculate position based on drop location, accounting for any potential offset
    const position = {
      x: event.clientX - reactFlowBounds.left - 100, // Offset to center the node
      y: event.clientY - reactFlowBounds.top - 50,
    };
      const createNodeData = (nodeType: NodeType): FlowNodeData => {
      const baseData = {
        label: `${nodeType.charAt(0).toUpperCase() + nodeType.slice(1)} Node`,
        description: `A ${nodeType} node`
      };

      switch (nodeType) {
        case NodeType.PROMPT:
          return {
            ...baseData,
            content: 'Enter your prompt here...',
            model: 'gpt-3.5-turbo',
            parameters: {
              temperature: 0.7,
              maxTokens: 1000,
              topP: 1.0
            },
            variables: []
          } as PromptNodeData;
        
        case NodeType.VARIABLE:
          return {
            ...baseData,
            name: 'variable1',
            type: 'string' as const,
            defaultValue: ''
          } as VariableNodeData;
        
        case NodeType.CONDITIONAL:
          return {
            ...baseData,
            condition: {
              leftOperand: '',
              operator: 'equals' as const,
              rightOperand: ''
            }
          } as ConditionalNodeData;
        
        case NodeType.TRANSFORM:
          return {
            ...baseData,
            transformType: 'format' as const,
            parameters: {}
          } as TransformNodeData;
        
        case NodeType.OUTPUT:
          return {
            ...baseData,
            format: 'text' as const,
            template: '{{result}}'
          } as OutputNodeData;
        
        case NodeType.LLM_CALL:
          return {
            ...baseData,
            provider: 'openai',
            model: 'gpt-3.5-turbo',
            prompt: 'Your prompt here...',
            parameters: {}
          } as any; // LLMCallNodeData
          case NodeType.TEMPLATE:
          return {
            ...baseData,
            templateId: 'template1',
            variables: {}
          } as any; // TemplateNodeData
        
        case NodeType.FOR_EACH:
          return {
            ...baseData,
            sourceVariable: '',
            itemVariable: 'item',
            iterationMode: 'sequential' as const,
            itemProperties: []
          } as ForEachNodeData;
        
        default:
          return {
            ...baseData,
            content: 'Default node content',
            model: 'gpt-3.5-turbo',
            parameters: {
              temperature: 0.7,
              maxTokens: 1000,
              topP: 1.0
            },
            variables: []
          } as PromptNodeData;
      }
    };
    
    const newNode: FlowNode = {
      id: `${nodeType}-${Date.now()}`,
      type: nodeType,
      position,
      data: createNodeData(nodeType)
    };
      const updatedNodes = [...nodes, newNode] as FlowNode[];
    setNodes(updatedNodes as Node[]);
    
    if (onFlowChange) {
      onFlowChange({
        ...flow,
        nodes: updatedNodes,
        updatedAt: new Date()
      });
    }
  }, [nodes, flow, onFlowChange, setNodes]);

  const handleDragOver = useCallback((event: React.DragEvent) => {
    event.preventDefault();
    event.dataTransfer.dropEffect = 'move';
  }, []);
  // Sync nodes and edges when flow prop changes
  useEffect(() => {
    console.log('FlowCanvas: Syncing nodes from flow prop:', flow.nodes.length);
    setNodes(flow.nodes.map(node => ({
      ...node,
      // Force React Flow to re-render by updating a timestamp
      data: { ...node.data, _lastUpdate: Date.now() }
    })) as Node[]);
  }, [flow.nodes, setNodes]);

  useEffect(() => {
    console.log('FlowCanvas: Syncing edges from flow prop:', flow.edges.length);
    setEdges(flow.edges as Edge[]);
  }, [flow.edges, setEdges]);

  return (
    <div style={{ width: '100%', height: '100%' }}>
      <ReactFlow
        nodes={nodes}
        edges={edges}
        onNodesChange={readonly ? undefined : handleNodesChange}
        onEdgesChange={readonly ? undefined : handleEdgesChange}
        onConnect={readonly ? undefined : handleConnect}
        onNodeClick={handleNodeClick}
        onNodeContextMenu={handleNodeContextMenu}
        onEdgeClick={handleEdgeClick}
        onPaneClick={handlePaneClick}        onDragOver={handleDragOver}
        onDrop={handleDrop}
        nodeTypes={nodeTypes}
        fitView
        snapToGrid
        snapGrid={[15, 15]}
        defaultViewport={{ x: 0, y: 0, zoom: 1 }}
        minZoom={0.1}
        maxZoom={2}
        deleteKeyCode={readonly ? null : 'Delete'}
      >
        <Background color="#aaa" gap={16} />
        <Controls />
        <MiniMap 
          nodeColor={(node: any) => {
            switch (node.type) {
              case 'prompt': return '#2196f3';
              case 'variable': return '#4caf50';
              case 'conditional': return '#ff9800';
              case 'transform': return '#9c27b0';
              case 'output': return '#f44336';
              default: return '#999';
            }
          }}
          nodeStrokeWidth={3}
          zoomable
          pannable
        />
      </ReactFlow>

      {/* Connection Validation Feedback */}
      <ConnectionValidation
        sourceNode={connectionValidation.sourceNode}
        targetNode={connectionValidation.targetNode}
        isVisible={connectionValidation.isVisible}
        position={connectionValidation.position}
      />

      {/* Smart Features Help Panel */}
      <SmartFeaturesHelp />

      {/* Smart Context Menu for intelligent flow suggestions */}
      <SmartContextMenu
        anchorEl={contextMenu.anchorEl}
        sourceNode={contextMenu.sourceNode}
        existingNodes={nodes as FlowNode[]}
        onClose={handleCloseContextMenu}
        onNodeAdd={handleSmartNodeAdd}
        position={contextMenu.position}
      />
    </div>
  );
};

export default FlowCanvas;
