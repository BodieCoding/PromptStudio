import React, { useState, useCallback, useImperativeHandle, forwardRef } from 'react';
import { Box, AppBar, Toolbar, Typography, Button, IconButton } from '@mui/material';
import { PlayArrow, Save, Settings, Help } from '@mui/icons-material';
import FlowCanvas from './components/FlowCanvas';
import NodePalette from './components/NodePalette';
import NodePropertyPanel from './components/NodePropertyPanel';
import { PromptFlow, FlowNode, FlowEdge } from './types/flow-types';
import { v4 as uuidv4 } from 'uuid';

export interface AppRef {
  getCurrentFlow: () => PromptFlow;
  loadFlow: (flow: PromptFlow) => void;
}

const App = forwardRef<AppRef>((props, ref) => {
  const [currentFlow, setCurrentFlow] = useState<PromptFlow>({
    id: uuidv4(),
    name: 'New Flow',
    description: 'A new prompt flow',
    nodes: [],
    edges: [],
    variables: {},
    version: '1.0.0',
    createdAt: new Date(),
    updatedAt: new Date(),
    tags: []
  });

  const [selectedNode, setSelectedNode] = useState<FlowNode | null>(null);
  const [isExecuting, setIsExecuting] = useState(false);
  const [showPalette, setShowPalette] = useState(true);

  // Expose methods to parent components
  useImperativeHandle(ref, () => ({
    getCurrentFlow: () => currentFlow,
    loadFlow: (flow: PromptFlow) => {
      setCurrentFlow(flow);
      setSelectedNode(null);
    }
  }), [currentFlow]);

  const handleFlowChange = useCallback((updatedFlow: PromptFlow) => {
    setCurrentFlow(updatedFlow);
  }, []);

  const handleNodeSelect = useCallback((node: FlowNode | null) => {
    setSelectedNode(node);
  }, []);

  const handleSaveFlow = useCallback(async () => {
    try {
      // TODO: Implement API call to save flow
      console.log('Saving flow:', currentFlow);
      // await flowApi.saveFlow(currentFlow);
    } catch (error) {
      console.error('Failed to save flow:', error);
    }
  }, [currentFlow]);

  const handleExecuteFlow = useCallback(async () => {
    if (isExecuting) return;
    
    setIsExecuting(true);
    try {
      // TODO: Implement flow execution
      console.log('Executing flow:', currentFlow);
      // await flowApi.executeFlow(currentFlow, {});
      
      // Simulate execution delay
      await new Promise(resolve => setTimeout(resolve, 2000));
    } catch (error) {
      console.error('Failed to execute flow:', error);
    } finally {
      setIsExecuting(false);
    }
  }, [currentFlow, isExecuting]);

  return (
    <Box className="visual-builder-container">
      <AppBar position="static" color="default" elevation={1}>
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            PromptStudio - {currentFlow.name}
          </Typography>
          
          <Button
            startIcon={<Save />}
            onClick={handleSaveFlow}
            variant="outlined"
            size="small"
            sx={{ mr: 1 }}
          >
            Save
          </Button>
          
          <Button
            startIcon={<PlayArrow />}
            onClick={handleExecuteFlow}
            variant="contained"
            size="small"
            disabled={isExecuting}
            sx={{ mr: 1 }}
          >
            {isExecuting ? 'Executing...' : 'Execute'}
          </Button>
          
          <IconButton size="small" sx={{ mr: 1 }}>
            <Settings />
          </IconButton>
          
          <IconButton size="small">
            <Help />
          </IconButton>
        </Toolbar>
      </AppBar>

      <Box className="visual-builder-content">
        {showPalette && (
          <NodePalette onToggle={() => setShowPalette(!showPalette)} />
        )}
        
        <Box className="flow-canvas" sx={{ flex: 1, position: 'relative' }}>
          <FlowCanvas
            flow={currentFlow}
            onFlowChange={handleFlowChange}
            onNodeSelect={handleNodeSelect}
            readonly={false}
          />
          
          {isExecuting && (
            <Box className="execution-indicator">
              <Box className="execution-progress" />
              <Typography>Executing flow...</Typography>
            </Box>
          )}
        </Box>

        {selectedNode && (
          <NodePropertyPanel
            node={selectedNode}            onUpdate={(updatedNode: FlowNode) => {
              const updatedNodes = currentFlow.nodes.map(node => 
                node.id === updatedNode.id ? updatedNode : node
              );
              handleFlowChange({
                ...currentFlow,
                nodes: updatedNodes,
                updatedAt: new Date()
              });
            }}
            onClose={() => setSelectedNode(null)}
          />
        )}
      </Box>
    </Box>  );
});

export default App;
