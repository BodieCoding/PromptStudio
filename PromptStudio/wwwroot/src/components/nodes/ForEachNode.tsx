import React from 'react';
import { Handle, Position } from 'reactflow';
import { Paper, Typography, Box, Chip, Tooltip } from '@mui/material';
import { Loop, PlayArrow } from '@mui/icons-material';
import { ForEachNodeData } from '../../types/flow-types';

interface ForEachNodeProps {
  data: ForEachNodeData;
  selected: boolean;
}

const ForEachNode: React.FC<ForEachNodeProps> = ({ data, selected }) => {
  const truncateText = (text: string, maxLength: number = 30) => {
    return text.length > maxLength ? `${text.substring(0, maxLength)}...` : text;
  };

  return (
    <Tooltip title="Right-click or Ctrl+Click for smart suggestions" placement="top">
      <Paper
        elevation={selected ? 4 : 2}
        className={`custom-node node-for-each ${selected ? 'selected' : ''}`}
        sx={{
          minWidth: 200,
          maxWidth: 300,
          border: selected ? '2px solid #9c27b0' : '2px solid transparent',
          transition: 'all 0.2s ease',
          cursor: 'pointer',
          '&:hover': {
            elevation: 3,
            borderColor: '#9c27b0'
          }
        }}
      >
        <Handle
          type="target"
          position={Position.Top}
          className="custom-node-handle"
          style={{ top: -6 }}
        />

        <Box className="custom-node-header" sx={{ backgroundColor: '#f3e5f5' }}>
          <Box className="custom-node-icon">
            <Loop sx={{ fontSize: 18, color: '#9c27b0' }} />
          </Box>
          <Typography variant="subtitle2" component="div">
            {data.label || 'For Each Loop'}
          </Typography>
        </Box>

        <Box className="custom-node-content" sx={{ p: 2 }}>
          <Typography variant="body2" sx={{ mb: 1, fontSize: '0.75rem' }}>
            {data.description && truncateText(data.description)}
          </Typography>
          
          <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5, mb: 1 }}>
            <Chip
              label={`Source: ${data.sourceVariable || 'Not set'}`}
              size="small"
              variant="outlined"
              sx={{ fontSize: '0.6rem', height: 20 }}
            />
            <Chip
              label={`Item: ${data.itemVariable || 'Not set'}`}
              size="small"
              variant="outlined"
              sx={{ fontSize: '0.6rem', height: 20 }}
            />
          </Box>

          <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
            <Chip
              icon={<PlayArrow sx={{ fontSize: 12 }} />}
              label={data.iterationMode || 'sequential'}
              size="small"
              color="primary"
              variant="outlined"
              sx={{ fontSize: '0.6rem', height: 20 }}
            />
            {data.itemProperties && data.itemProperties.length > 0 && (
              <Chip
                label={`${data.itemProperties.length} props`}
                size="small"
                variant="outlined"
                sx={{ fontSize: '0.6rem', height: 20 }}
              />
            )}
          </Box>
        </Box>

        <Handle
          type="source"
          position={Position.Bottom}
          className="custom-node-handle"
          style={{ bottom: -6 }}
        />
      </Paper>
    </Tooltip>
  );
};

ForEachNode.displayName = 'ForEachNode';

export default ForEachNode;
