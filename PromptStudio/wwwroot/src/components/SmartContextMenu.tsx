import React, { useState, useEffect } from 'react';
import {
  Popover,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  ListItemButton,
  Typography,
  Chip,
  Box,
  Divider,
  Paper
} from '@mui/material';
import {
  ChatBubbleOutline,
  DataObject,
  AccountTree,
  Transform,
  Output,
  Add,
  LightbulbOutlined,
  TrendingFlat
} from '@mui/icons-material';

import { FlowNode, NodeType, FlowSuggestion } from '../types/flow-types';
import { FlowOrchestrationService } from '../services/FlowOrchestrationService';

interface SmartContextMenuProps {
  anchorEl: HTMLElement | null;
  sourceNode: FlowNode | null;
  existingNodes: FlowNode[];
  onClose: () => void;
  onNodeAdd: (nodeType: NodeType, config?: any) => void;
  position: { x: number; y: number };
}

const nodeIcons = {
  [NodeType.PROMPT]: <ChatBubbleOutline />,
  [NodeType.VARIABLE]: <DataObject />,
  [NodeType.CONDITIONAL]: <AccountTree />,
  [NodeType.TRANSFORM]: <Transform />,
  [NodeType.OUTPUT]: <Output />,
  [NodeType.LLM_CALL]: <ChatBubbleOutline />,
  [NodeType.TEMPLATE]: <ChatBubbleOutline />,
  [NodeType.FOR_EACH]: <TrendingFlat />
};

const nodeColors = {
  [NodeType.PROMPT]: '#2196f3',
  [NodeType.VARIABLE]: '#4caf50',
  [NodeType.CONDITIONAL]: '#ff9800',
  [NodeType.TRANSFORM]: '#9c27b0',
  [NodeType.OUTPUT]: '#f44336',
  [NodeType.LLM_CALL]: '#00bcd4',
  [NodeType.TEMPLATE]: '#795548',
  [NodeType.FOR_EACH]: '#e91e63'
};

const SmartContextMenu: React.FC<SmartContextMenuProps> = ({
  anchorEl,
  sourceNode,
  existingNodes,
  onClose,
  onNodeAdd,
  position
}) => {
  const [suggestions, setSuggestions] = useState<FlowSuggestion[]>([]);
  useEffect(() => {
    console.log('SmartContextMenu useEffect - sourceNode:', sourceNode?.id, 'anchorEl:', !!anchorEl);
    if (sourceNode) {
      const newSuggestions = FlowOrchestrationService.getSuggestedNextNodes(
        sourceNode,
        existingNodes,
        position
      );
      console.log('Generated suggestions:', newSuggestions);
      setSuggestions(newSuggestions);
    }
  }, [sourceNode, existingNodes, position]);

  const handleNodeAdd = (suggestion: FlowSuggestion) => {
    onNodeAdd(suggestion.nodeType, suggestion.defaultConfig);
    onClose();
  };

  const getPriorityChip = (priority: number) => {
    if (priority >= 90) return <Chip label="Recommended" size="small" color="success" />;
    if (priority >= 80) return <Chip label="Good fit" size="small" color="primary" />;
    if (priority >= 70) return <Chip label="Possible" size="small" color="default" />;
    return null;
  };

  return (
    <Popover
      open={Boolean(anchorEl)}
      anchorEl={anchorEl}
      onClose={onClose}
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'center',
      }}
      transformOrigin={{
        vertical: 'top',
        horizontal: 'center',
      }}      slotProps={{
        paper: {
          sx: { 
            maxWidth: 450, 
            minWidth: 400,
            maxHeight: 500,
            overflow: 'auto'
          }
        }
      }}
    >
      <Paper sx={{ p: 2 }}>
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
          <LightbulbOutlined sx={{ mr: 1, color: 'warning.main' }} />
          <Typography variant="h6" component="div">
            Smart Suggestions
          </Typography>
        </Box>
        
        {sourceNode && (
          <Box sx={{ mb: 2 }}>
            <Typography variant="body2" color="text.secondary">
              Add nodes after "{sourceNode.data.label}"
            </Typography>
          </Box>
        )}

        {suggestions.length > 0 ? (
          <List dense>
            {suggestions.map((suggestion, index) => (
              <React.Fragment key={index}>
                <ListItem disablePadding>
                  <ListItemButton 
                    onClick={() => handleNodeAdd(suggestion)}
                    sx={{
                      borderRadius: 1,
                      mb: 1,
                      border: `1px solid ${nodeColors[suggestion.nodeType]}20`,
                      '&:hover': {
                        backgroundColor: `${nodeColors[suggestion.nodeType]}10`,
                        borderColor: `${nodeColors[suggestion.nodeType]}40`
                      }
                    }}
                  >
                    <ListItemIcon sx={{ minWidth: 36 }}>
                      <Box sx={{ color: nodeColors[suggestion.nodeType] }}>
                        {nodeIcons[suggestion.nodeType]}
                      </Box>
                    </ListItemIcon>
                    
                    <ListItemText
                      primary={
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                          <Typography variant="body2" fontWeight="medium">
                            {suggestion.nodeType.charAt(0).toUpperCase() + suggestion.nodeType.slice(1)} Node
                          </Typography>
                          {getPriorityChip(suggestion.priority)}
                          {suggestion.autoConnect && (
                            <Chip 
                              icon={<TrendingFlat />} 
                              label="Auto-connect" 
                              size="small" 
                              variant="outlined" 
                            />
                          )}
                        </Box>
                      }
                      secondary={
                        <Typography variant="caption" color="text.secondary">
                          {suggestion.reason}
                        </Typography>
                      }
                    />
                  </ListItemButton>
                </ListItem>
                {index < suggestions.length - 1 && <Divider />}
              </React.Fragment>
            ))}
          </List>
        ) : (
          <Box sx={{ textAlign: 'center', py: 2 }}>
            <Typography variant="body2" color="text.secondary">
              No specific suggestions available. You can add any node type.
            </Typography>
          </Box>
        )}

        <Divider sx={{ my: 2 }} />
        
        <Box sx={{ textAlign: 'center' }}>
          <Typography variant="caption" color="text.secondary">
            💡 Click any suggestion to add and auto-connect the node
          </Typography>
        </Box>
      </Paper>
    </Popover>
  );
};

export default SmartContextMenu;
