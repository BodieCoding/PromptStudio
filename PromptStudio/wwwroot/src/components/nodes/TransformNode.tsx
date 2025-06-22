import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';
import { Paper, Typography, Box, Chip } from '@mui/material';
import { Transform } from '@mui/icons-material';
import { TransformNodeData } from '../../types/flow-types';

interface TransformNodeProps {
  data: TransformNodeData;
  selected: boolean;
}

const TransformNode: React.FC<TransformNodeProps> = memo(({ data, selected }) => {
  return (
    <Paper
      elevation={selected ? 4 : 2}
      className={`custom-node node-transform ${selected ? 'selected' : ''}`}
      sx={{
        minWidth: 160,
        maxWidth: 260,
        border: selected ? '2px solid #1976d2' : '2px solid transparent',
        transition: 'all 0.2s ease',
      }}
    >
      <Handle
        type="target"
        position={Position.Left}
        className="custom-node-handle"
        style={{ left: -6 }}
      />

      <Box className="custom-node-header">
        <Box className="custom-node-icon">
          <Transform sx={{ fontSize: 18, color: '#9c27b0' }} />
        </Box>
        <Typography variant="subtitle2" component="div">
          {data.label || 'Transform'}
        </Typography>
      </Box>

      <Box className="custom-node-content">
        <Chip
          label={data.transformType}
          size="small"
          variant="outlined"
          sx={{ fontSize: '0.6rem', height: 20, mb: 1 }}
        />

        {data.code && (
          <Typography variant="caption" color="text.secondary" sx={{ display: 'block', fontFamily: 'monospace' }}>
            {data.code.length > 30 ? `${data.code.substring(0, 30)}...` : data.code}
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

TransformNode.displayName = 'TransformNode';

export default TransformNode;
