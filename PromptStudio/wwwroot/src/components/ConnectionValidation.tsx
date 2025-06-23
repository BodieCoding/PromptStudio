import React from 'react';
import { Box, Alert, AlertTitle, Chip } from '@mui/material';
import { CheckCircle, Warning, Error, Lightbulb } from '@mui/icons-material';

import { FlowNode } from '../types/flow-types';
import { FlowOrchestrationService } from '../services/FlowOrchestrationService';

interface ConnectionValidationProps {
  sourceNode: FlowNode | null;
  targetNode: FlowNode | null;
  isVisible: boolean;
  position: { x: number; y: number };
}

const ConnectionValidation: React.FC<ConnectionValidationProps> = ({
  sourceNode,
  targetNode,
  isVisible,
  position
}) => {
  if (!isVisible || !sourceNode || !targetNode) {
    return null;
  }

  const validation = FlowOrchestrationService.validateConnection(
    sourceNode,
    targetNode
  );

  const getAlertProps = () => {
    if (!validation.valid) {
      return {
        severity: 'error' as const,
        icon: <Error />,
        title: 'Invalid Connection'
      };
    } else if (validation.suggestion) {
      return {
        severity: 'success' as const,
        icon: <CheckCircle />,
        title: 'Great Connection!'
      };
    } else {
      return {
        severity: 'info' as const,
        icon: <Lightbulb />,
        title: 'Valid Connection'
      };
    }
  };

  const alertProps = getAlertProps();

  return (
    <Box
      sx={{
        position: 'fixed',
        top: position.y + 10,
        left: position.x + 10,
        zIndex: 1000,
        maxWidth: 300,
        pointerEvents: 'none'
      }}
    >
      <Alert {...alertProps} variant="filled">
        <AlertTitle>{alertProps.title}</AlertTitle>
        {validation.message && (
          <Box sx={{ mb: 1 }}>
            {validation.message}
          </Box>
        )}
        {validation.suggestion && (
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>            <Chip 
              label="Tip" 
              size="small" 
              color="primary" 
              variant="outlined" 
            />
            <span style={{ fontSize: '0.875rem' }}>
              {validation.suggestion}
            </span>
          </Box>
        )}
      </Alert>
    </Box>
  );
};

export default ConnectionValidation;
