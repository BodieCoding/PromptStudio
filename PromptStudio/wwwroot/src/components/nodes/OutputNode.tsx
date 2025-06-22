import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';
import { Paper, Typography, Box, Chip } from '@mui/material';
import { Output } from '@mui/icons-material';
import { OutputNodeData } from '../../types/flow-types';

interface OutputNodeProps {
  data: OutputNodeData;
  selected: boolean;
}

const OutputNode: React.FC<OutputNodeProps> = memo(({ data, selected }) => {
  return (
    <Paper
      elevation={selected ? 4 : 2}
      className={`custom-node node-output ${selected ? 'selected' : ''}`}
      sx={{
        minWidth: 140,
        maxWidth: 240,
        border: selected ? '2px solid #1976d2' : '2px solid transparent',
        transition: 'all 0.2s ease',
      }}
    >
      <Handle
        type="target"
        position={Position.Top}
        className="custom-node-handle"
        style={{ top: -6 }}
      />

      <Box className="custom-node-header">
        <Box className="custom-node-icon">
          <Output sx={{ fontSize: 18, color: '#f44336' }} />
        </Box>
        <Typography variant="subtitle2" component="div">
          {data.label || 'Output'}
        </Typography>
      </Box>

      <Box className="custom-node-content">
        <Chip
          label={data.format || 'text'}
          size="small"
          variant="outlined"
          sx={{ fontSize: '0.6rem', height: 20, mb: 1 }}
        />

        {data.template && (
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block' }}>
            Template: {data.template.length > 20 ? `${data.template.substring(0, 20)}...` : data.template}
          </Typography>
        )}

        {data.description && (
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mt: 0.5 }}>
            {data.description}
          </Typography>
        )}
      </Box>
    </Paper>
  );
});

OutputNode.displayName = 'OutputNode';

export default OutputNode;
