import React from 'react';
import { Handle, Position } from 'reactflow';
import { Paper, Typography, Box, Chip, Tooltip } from '@mui/material';
import { ChatBubbleOutline } from '@mui/icons-material';
import { PromptNodeData } from '../../types/flow-types';

interface PromptNodeProps {
  data: PromptNodeData;
  selected: boolean;
}

const PromptNode: React.FC<PromptNodeProps> = ({ data, selected }) => {
  console.log('PromptNode rendering with data:', data);
  
  const truncateText = (text: string, maxLength: number = 50) => {
    return text.length > maxLength ? `${text.substring(0, maxLength)}...` : text;
  };
  return (
    <Tooltip title="Right-click or Ctrl+Click for smart suggestions" placement="top">
      <Paper
        elevation={selected ? 4 : 2}
        className={`custom-node node-prompt ${selected ? 'selected' : ''}`}
        sx={{
          minWidth: 200,
          maxWidth: 300,
          border: selected ? '2px solid #1976d2' : '2px solid transparent',
          transition: 'all 0.2s ease',
          cursor: 'pointer',
          '&:hover': {
            elevation: 3,
            borderColor: '#1976d2',
            '& .smart-chip': {
              opacity: 1
            }
          }
        }}
      >
      <Handle
        type="target"
        position={Position.Top}
        className="custom-node-handle"
        style={{ top: -6 }}
      />      <Box className="custom-node-header">
        <Box className="custom-node-icon">
          <ChatBubbleOutline sx={{ fontSize: 18, color: '#2196f3' }} />
        </Box>
        <Typography variant="subtitle2" component="div">
          {data.label || 'Prompt Node'}
        </Typography>        <Chip 
          label="Smart" 
          size="small" 
          variant="outlined" 
          className="smart-chip"
          sx={{ 
            fontSize: '0.6rem', 
            height: 16, 
            ml: 'auto',
            borderColor: '#2196f3',
            color: '#2196f3',
            opacity: 0.7
          }} 
        />
      </Box>

      <Box className="custom-node-content">
        {data.content && (
          <Typography variant="body2" sx={{ mb: 1, fontSize: '0.75rem' }}>
            {truncateText(data.content)}
          </Typography>
        )}
        
        <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5, mb: 1 }}>
          <Chip
            label={data.model || 'No Model'}
            size="small"
            variant="outlined"
            sx={{ fontSize: '0.6rem', height: 20 }}
          />
          {data.parameters?.temperature !== undefined && (
            <Chip
              label={`T: ${data.parameters.temperature}`}
              size="small"
              variant="outlined"
              sx={{ fontSize: '0.6rem', height: 20 }}
            />
          )}
        </Box>

        {data.variables && data.variables.length > 0 && (
          <Typography variant="caption" color="text.secondary">
            Variables: {data.variables.map(v => v.name).join(', ')}
          </Typography>
        )}
      </Box>

      <Handle
        type="source"
        position={Position.Bottom}
        className="custom-node-handle"        style={{ bottom: -6 }}      />
    </Paper>
    </Tooltip>
  );
};

PromptNode.displayName = 'PromptNode';

export default PromptNode;
