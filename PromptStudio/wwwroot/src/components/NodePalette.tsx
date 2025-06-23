import React from 'react';
import { 
  Box, 
  Typography, 
  List, 
  ListItem, 
  ListItemIcon, 
  ListItemText,
  Paper,
  IconButton,
  Divider
} from '@mui/material';
import {
  ChatBubbleOutline,
  DataObject,
  AccountTree,
  Transform,
  Output,
  Cloud,
  Description,
  Close,
  Loop
} from '@mui/icons-material';
import { NodeType } from '../types/flow-types';

interface NodePaletteProps {
  onToggle: () => void;
}

interface PaletteItem {
  type: NodeType;
  label: string;
  icon: React.ReactNode;
  description: string;
  color: string;
}

const paletteItems: PaletteItem[] = [
  {
    type: NodeType.PROMPT,
    label: 'Prompt',
    icon: <ChatBubbleOutline />,
    description: 'Send a prompt to an LLM',
    color: '#2196f3'
  },
  {
    type: NodeType.VARIABLE,
    label: 'Variable',
    icon: <DataObject />,
    description: 'Define input variables',
    color: '#4caf50'
  },
  {
    type: NodeType.CONDITIONAL,
    label: 'Condition',
    icon: <AccountTree />,
    description: 'Conditional logic branching',
    color: '#ff9800'
  },
  {
    type: NodeType.TRANSFORM,
    label: 'Transform',
    icon: <Transform />,
    description: 'Transform data or text',
    color: '#9c27b0'
  },  {
    type: NodeType.OUTPUT,
    label: 'Output',
    icon: <Output />,
    description: 'Format and output results',
    color: '#f44336'
  },
  {
    type: NodeType.FOR_EACH,
    label: 'For Each',
    icon: <Loop />,
    description: 'Iterate over lists/arrays',
    color: '#9c27b0'
  },
  {
    type: NodeType.LLM_CALL,
    label: 'LLM Call',
    icon: <Cloud />,
    description: 'Direct LLM API call',
    color: '#00bcd4'
  },
  {
    type: NodeType.TEMPLATE,
    label: 'Template',
    icon: <Description />,
    description: 'Reusable prompt template',
    color: '#795548'
  }
];

const NodePalette: React.FC<NodePaletteProps> = ({ onToggle }) => {
  const handleDragStart = (event: React.DragEvent, nodeType: NodeType) => {
    event.dataTransfer.setData('application/reactflow', nodeType);
    event.dataTransfer.effectAllowed = 'move';
  };

  return (
    <Paper
      className="node-palette"
      elevation={2}
      sx={{
        width: 280,
        height: '100%',
        borderRadius: 0,
        borderRight: '1px solid #e0e0e0',
        display: 'flex',
        flexDirection: 'column',
        position: 'relative'
      }}
    >
      <Box sx={{ p: 2, borderBottom: '1px solid #e0e0e0', display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
        <Typography variant="h6" component="div" sx={{ fontWeight: 600 }}>
          Node Palette
        </Typography>
        <IconButton size="small" onClick={onToggle}>
          <Close />
        </IconButton>
      </Box>

      <Box sx={{ flex: 1, overflow: 'auto', p: 1 }}>
        <Typography variant="subtitle2" sx={{ px: 1, py: 1, color: 'text.secondary', fontWeight: 600 }}>
          Core Nodes
        </Typography>

        <List dense>
          {paletteItems.map((item) => (
            <ListItem
              key={item.type}
              draggable
              onDragStart={(e) => handleDragStart(e, item.type)}
              sx={{
                cursor: 'grab',
                borderRadius: 1,
                mb: 0.5,
                border: '1px solid #e0e0e0',
                backgroundColor: '#fafafa',
                transition: 'all 0.2s ease',
                '&:hover': {
                  backgroundColor: '#f0f0f0',
                  transform: 'translateX(4px)',
                  boxShadow: '2px 2px 8px rgba(0,0,0,0.1)'
                },
                '&:active': {
                  cursor: 'grabbing',
                  transform: 'scale(0.95)'
                }
              }}
            >
              <ListItemIcon sx={{ minWidth: 36 }}>
                <Box sx={{ color: item.color, display: 'flex', alignItems: 'center' }}>
                  {item.icon}
                </Box>
              </ListItemIcon>
              <ListItemText
                primary={
                  <Typography variant="body2" sx={{ fontWeight: 500 }}>
                    {item.label}
                  </Typography>
                }
                secondary={
                  <Typography variant="caption" color="text.secondary">
                    {item.description}
                  </Typography>
                }
              />
            </ListItem>
          ))}
        </List>

        <Divider sx={{ my: 2 }} />

        <Typography variant="subtitle2" sx={{ px: 1, py: 1, color: 'text.secondary', fontWeight: 600 }}>
          Getting Started
        </Typography>

        <Box sx={{ p: 2, backgroundColor: '#f8f9fa', borderRadius: 1, m: 1 }}>
          <Typography variant="body2" sx={{ mb: 1, fontWeight: 500 }}>
            💡 Quick Tips
          </Typography>
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mb: 1 }}>
            • Drag nodes from this palette to the canvas
          </Typography>
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mb: 1 }}>
            • Connect nodes by dragging from output to input handles
          </Typography>
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mb: 1 }}>
            • Click nodes to edit their properties
          </Typography>
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block' }}>
            • Press Delete to remove selected elements
          </Typography>
        </Box>
      </Box>
    </Paper>
  );
};

export default NodePalette;
