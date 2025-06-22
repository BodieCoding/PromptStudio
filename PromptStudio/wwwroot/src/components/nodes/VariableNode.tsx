import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';
import { Paper, Typography, Box, Chip } from '@mui/material';
import { DataObject } from '@mui/icons-material';
import { VariableNodeData } from '../../types/flow-types';

interface VariableNodeProps {
  data: VariableNodeData;
  selected: boolean;
}

const VariableNode: React.FC<VariableNodeProps> = memo(({ data, selected }) => {
  return (
    <Paper
      elevation={selected ? 4 : 2}
      className={`custom-node node-variable ${selected ? 'selected' : ''}`}
      sx={{
        minWidth: 150,
        maxWidth: 250,
        border: selected ? '2px solid #1976d2' : '2px solid transparent',
        transition: 'all 0.2s ease',
      }}
    >
      <Box className="custom-node-header">
        <Box className="custom-node-icon">
          <DataObject sx={{ fontSize: 18, color: '#4caf50' }} />
        </Box>
        <Typography variant="subtitle2" component="div">
          {data.label || 'Variable'}
        </Typography>
      </Box>

      <Box className="custom-node-content">
        <Typography variant="body2" sx={{ fontWeight: 'bold', mb: 0.5 }}>
          {data.name}
        </Typography>
        
        <Chip
          label={data.type}
          size="small"
          variant="outlined"
          sx={{ fontSize: '0.6rem', height: 20, mb: 1 }}
        />

        {data.defaultValue !== undefined && (
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block' }}>
            Default: {String(data.defaultValue)}
          </Typography>
        )}

        {data.description && (
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mt: 0.5 }}>
            {data.description}
          </Typography>
        )}
      </Box>

      <Handle
        type="source"
        position={Position.Right}
        className="custom-node-handle"
        style={{ right: -6 }}
      />
    </Paper>
  );
});

VariableNode.displayName = 'VariableNode';

export default VariableNode;
