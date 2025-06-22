import React, { memo } from 'react';
import { Handle, Position } from 'reactflow';
import { Paper, Typography, Box } from '@mui/material';
import { AccountTree } from '@mui/icons-material';
import { ConditionalNodeData } from '../../types/flow-types';

interface ConditionalNodeProps {
  data: ConditionalNodeData;
  selected: boolean;
}

const ConditionalNode: React.FC<ConditionalNodeProps> = memo(({ data, selected }) => {
  const getOperatorSymbol = (operator: string) => {
    switch (operator) {
      case 'equals': return '==';
      case 'contains': return '∈';
      case 'greater_than': return '>';
      case 'less_than': return '<';
      case 'exists': return '∃';
      default: return operator;
    }
  };

  return (
    <Paper
      elevation={selected ? 4 : 2}
      className={`custom-node node-conditional ${selected ? 'selected' : ''}`}
      sx={{
        minWidth: 180,
        maxWidth: 280,
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
          <AccountTree sx={{ fontSize: 18, color: '#ff9800' }} />
        </Box>
        <Typography variant="subtitle2" component="div">
          {data.label || 'Condition'}
        </Typography>
      </Box>

      <Box className="custom-node-content">
        {data.condition && (
          <Typography variant="body2" sx={{ mb: 1, fontSize: '0.75rem', fontFamily: 'monospace' }}>
            {typeof data.condition.leftOperand === 'string' 
              ? data.condition.leftOperand 
              : data.condition.leftOperand.name
            } {' '}
            {getOperatorSymbol(data.condition.operator)} {' '}
            {typeof data.condition.rightOperand === 'string' 
              ? data.condition.rightOperand 
              : data.condition.rightOperand.name
            }
          </Typography>
        )}

        {data.description && (
          <Typography variant="caption" color="text.secondary">
            {data.description}
          </Typography>
        )}
      </Box>

      <Handle
        type="source"
        position={Position.Bottom}
        id="true"
        className="custom-node-handle"
        style={{ bottom: -6, left: '25%' }}
      />

      <Handle
        type="source"
        position={Position.Bottom}
        id="false"
        className="custom-node-handle"
        style={{ bottom: -6, right: '25%' }}
      />

      {/* Labels for true/false paths */}
      <Typography
        variant="caption"
        sx={{
          position: 'absolute',
          bottom: -20,
          left: '20%',
          fontSize: '0.6rem',
          color: '#4caf50',
          fontWeight: 'bold'
        }}
      >
        TRUE
      </Typography>
      <Typography
        variant="caption"
        sx={{
          position: 'absolute',
          bottom: -20,
          right: '20%',
          fontSize: '0.6rem',
          color: '#f44336',
          fontWeight: 'bold'
        }}
      >
        FALSE
      </Typography>
    </Paper>
  );
});

ConditionalNode.displayName = 'ConditionalNode';

export default ConditionalNode;
