import React, { useState, useEffect } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Box,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Switch,
  FormControlLabel,
  Alert,
  CircularProgress,
  Chip
} from '@mui/material';
import { PlayArrow, Close } from '@mui/icons-material';
import { PromptFlow, VariableNodeData, NodeType } from '../types/flow-types';

interface FlowExecutionDialogProps {
  open: boolean;
  onClose: () => void;
  flow: PromptFlow;
  onExecute: (variables: Record<string, any>) => Promise<void>;
}

interface FlowVariable {
  name: string;
  type: 'string' | 'number' | 'boolean' | 'json';
  defaultValue?: any;
  required: boolean;
  description?: string;
}

const FlowExecutionDialog: React.FC<FlowExecutionDialogProps> = ({
  open,
  onClose,
  flow,
  onExecute
}) => {
  const [variables, setVariables] = useState<FlowVariable[]>([]);
  const [variableValues, setVariableValues] = useState<Record<string, any>>({});
  const [isExecuting, setIsExecuting] = useState(false);
  const [validationErrors, setValidationErrors] = useState<Record<string, string>>({});

  useEffect(() => {
    if (open && flow) {
      extractVariablesFromFlow();
    }
  }, [open, flow]);

  const extractVariablesFromFlow = () => {
    const extractedVars: FlowVariable[] = [];
    const initialValues: Record<string, any> = {};

    // Extract variables from Variable nodes
    flow.nodes?.forEach(node => {
      if (node.type === NodeType.VARIABLE) {
        const varData = node.data as VariableNodeData;
        const variable: FlowVariable = {
          name: varData.name,
          type: varData.type,
          defaultValue: varData.defaultValue,
          required: !varData.defaultValue,
          description: varData.description
        };
        extractedVars.push(variable);
        initialValues[varData.name] = varData.defaultValue || getDefaultValueForType(varData.type);
      }
    });

    // Extract variables referenced in Prompt nodes
    flow.nodes?.forEach(node => {
      if (node.type === NodeType.PROMPT) {
        const promptData = node.data as any;
        promptData.variables?.forEach((varRef: any) => {
          if (!extractedVars.find(v => v.name === varRef.name)) {
            const variable: FlowVariable = {
              name: varRef.name,
              type: 'string',
              required: true,
              description: `Variable referenced in ${node.data.label}`
            };
            extractedVars.push(variable);
            initialValues[varRef.name] = '';
          }
        });
      }
    });

    // Parse flow content for {{variable}} syntax
    const flowDataStr = JSON.stringify(flow);
    const variableMatches = flowDataStr.match(/\{\{([^}]+)\}\}/g);
    if (variableMatches) {
      variableMatches.forEach(match => {
        const varName = match.replace(/[{}]/g, '');
        if (!extractedVars.find(v => v.name === varName)) {
          const variable: FlowVariable = {
            name: varName,
            type: 'string',
            required: true,
            description: `Template variable`
          };
          extractedVars.push(variable);
          initialValues[varName] = '';
        }
      });
    }

    setVariables(extractedVars);
    setVariableValues(initialValues);
    setValidationErrors({});
  };

  const getDefaultValueForType = (type: string) => {
    switch (type) {
      case 'number': return 0;
      case 'boolean': return false;
      case 'json': return {};
      default: return '';
    }
  };

  const handleVariableChange = (varName: string, value: any) => {
    setVariableValues(prev => ({
      ...prev,
      [varName]: value
    }));

    // Clear validation error when user starts typing
    if (validationErrors[varName]) {
      setValidationErrors(prev => {
        const newErrors = { ...prev };
        delete newErrors[varName];
        return newErrors;
      });
    }
  };

  const validateInputs = (): boolean => {
    const errors: Record<string, string> = {};

    variables.forEach(variable => {
      const value = variableValues[variable.name];
      
      if (variable.required && (value === undefined || value === null || value === '')) {
        errors[variable.name] = `${variable.name} is required`;
        return;
      }

      // Type validation
      if (value !== undefined && value !== null && value !== '') {
        switch (variable.type) {
          case 'number':
            if (isNaN(Number(value))) {
              errors[variable.name] = `${variable.name} must be a valid number`;
            }
            break;
          case 'json':
            if (typeof value === 'string') {
              try {
                JSON.parse(value);
              } catch {
                errors[variable.name] = `${variable.name} must be valid JSON`;
              }
            }
            break;
        }
      }
    });

    setValidationErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const handleExecute = async () => {
    if (!validateInputs()) {
      return;
    }

    setIsExecuting(true);
    try {
      // Convert values to appropriate types
      const processedValues: Record<string, any> = {};
      variables.forEach(variable => {
        let value = variableValues[variable.name];
        
        switch (variable.type) {
          case 'number':
            value = Number(value);
            break;
          case 'boolean':
            value = Boolean(value);
            break;
          case 'json':
            if (typeof value === 'string') {
              try {
                value = JSON.parse(value);
              } catch {
                value = {};
              }
            }
            break;
        }
        
        processedValues[variable.name] = value;
      });

      await onExecute(processedValues);
    } finally {
      setIsExecuting(false);
    }
  };

  const renderVariableInput = (variable: FlowVariable) => {
    const value = variableValues[variable.name];
    const error = validationErrors[variable.name];

    switch (variable.type) {
      case 'boolean':
        return (
          <FormControlLabel
            control={
              <Switch
                checked={Boolean(value)}
                onChange={(e) => handleVariableChange(variable.name, e.target.checked)}
              />
            }
            label={variable.name}
          />
        );

      case 'number':
        return (
          <TextField
            fullWidth
            label={variable.name}
            type="number"
            value={value || ''}
            onChange={(e) => handleVariableChange(variable.name, e.target.value)}
            error={!!error}
            helperText={error || variable.description}
            required={variable.required}
          />
        );

      case 'json':
        return (
          <TextField
            fullWidth
            label={variable.name}
            multiline
            rows={4}
            value={typeof value === 'string' ? value : JSON.stringify(value, null, 2)}
            onChange={(e) => handleVariableChange(variable.name, e.target.value)}
            error={!!error}
            helperText={error || variable.description || 'Enter valid JSON'}
            required={variable.required}
          />
        );

      default: // string
        return (
          <TextField
            fullWidth
            label={variable.name}
            value={value || ''}
            onChange={(e) => handleVariableChange(variable.name, e.target.value)}
            error={!!error}
            helperText={error || variable.description}
            required={variable.required}
            multiline={value && value.length > 100}
            rows={value && value.length > 100 ? 3 : 1}
          />
        );
    }
  };

  return (
    <Dialog 
      open={open} 
      onClose={onClose} 
      maxWidth="md" 
      fullWidth
      PaperProps={{
        sx: { minHeight: '400px' }
      }}
    >
      <DialogTitle>
        <Box display="flex" alignItems="center" justifyContent="space-between">
          <Typography variant="h6">Execute Flow: {flow.name}</Typography>
          <Button onClick={onClose} size="small">
            <Close />
          </Button>
        </Box>
      </DialogTitle>

      <DialogContent>
        {variables.length === 0 ? (
          <Alert severity="info">
            This flow doesn't require any input variables. Click Execute to run it.
          </Alert>
        ) : (
          <>
            <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
              Provide values for the following variables:
            </Typography>
            
            <Box display="flex" flexWrap="wrap" gap={1} mb={2}>
              {variables.map(variable => (
                <Chip 
                  key={variable.name}
                  label={`${variable.name} (${variable.type})`}
                  size="small"
                  color={variable.required ? "primary" : "default"}
                  variant={variable.required ? "filled" : "outlined"}
                />
              ))}
            </Box>

            <Box display="flex" flexDirection="column" gap={2}>
              {variables.map(variable => (
                <Box key={variable.name}>
                  {renderVariableInput(variable)}
                </Box>
              ))}
            </Box>
          </>
        )}
      </DialogContent>

      <DialogActions>
        <Button onClick={onClose} disabled={isExecuting}>
          Cancel
        </Button>
        <Button 
          onClick={handleExecute} 
          variant="contained"
          startIcon={isExecuting ? <CircularProgress size={16} /> : <PlayArrow />}
          disabled={isExecuting}
        >
          {isExecuting ? 'Executing...' : 'Execute Flow'}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default FlowExecutionDialog;
