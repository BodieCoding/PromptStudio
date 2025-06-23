import React, { useState, useCallback, useImperativeHandle, forwardRef } from 'react';
import { Box, AppBar, Toolbar, Typography, Button, IconButton } from '@mui/material';
import { PlayArrow, Save, Settings, Help } from '@mui/icons-material';
import FlowCanvas from './components/FlowCanvas';
import NodePalette from './components/NodePalette';
import NodePropertyPanel from './components/NodePropertyPanel';
import FlowExecutionDialog from './components/FlowExecutionDialog';
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
  const [showExecutionDialog, setShowExecutionDialog] = useState(false);

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
    setShowExecutionDialog(true);
  }, [isExecuting]);

  const handleExecuteFlowWithVariables = useCallback(async (variables: Record<string, any>) => {
    setIsExecuting(true);
    setShowExecutionDialog(false);
    
    try {
      console.log('Executing flow with variables:', currentFlow, variables);
      
      // TODO: Call API to execute flow
      // const result = await flowApi.executeFlow(currentFlow.id, variables);
      
      // Simulate execution delay
      await new Promise(resolve => setTimeout(resolve, 2000));
      
      console.log('Flow execution completed');
    } catch (error) {
      console.error('Failed to execute flow:', error);
    } finally {
      setIsExecuting(false);
    }
  }, [currentFlow]);

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
        </Box>        {selectedNode && (
          <Box sx={{ 
            width: 400, 
            minWidth: 400,
            maxWidth: 500,
            height: '100%',
            borderLeft: '1px solid #e0e0e0',
            backgroundColor: '#fafafa',
            overflow: 'hidden',
            display: 'flex',
            flexDirection: 'column'
          }}>
            <NodePropertyPanel
              node={selectedNode}
              onUpdate={(updatedNode: FlowNode) => {
                console.log('Updating node:', updatedNode.id, updatedNode.data);
                const updatedNodes = currentFlow.nodes.map(node => 
                  node.id === updatedNode.id ? updatedNode : node
                );
                
                // Update the selected node to reflect changes immediately
                setSelectedNode(updatedNode);
                
                handleFlowChange({
                  ...currentFlow,
                  nodes: updatedNodes,
                  updatedAt: new Date()
                });
              }}
              onClose={() => setSelectedNode(null)}
            />          </Box>
        )}
      </Box>

      <FlowExecutionDialog
        open={showExecutionDialog}
        onClose={() => setShowExecutionDialog(false)}
        flow={currentFlow}
        onExecute={handleExecuteFlowWithVariables}
      />
    </Box>);
});

export default App;
