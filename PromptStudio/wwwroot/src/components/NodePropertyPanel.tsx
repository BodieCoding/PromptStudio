import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  TextField,
  Button,
  Paper,
  IconButton,
  Divider,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Chip,
  Slider,
  Switch,
  FormControlLabel
} from '@mui/material';
import { Close, Save } from '@mui/icons-material';
import { FlowNode, NodeType, PromptNodeData, VariableNodeData, ConditionalNodeData, NodePropertyPanelProps } from '../types/flow-types';

const NodePropertyPanel: React.FC<NodePropertyPanelProps> = ({ node, onUpdate, onClose }) => {
  const [localData, setLocalData] = useState(node.data);
  const [hasChanges, setHasChanges] = useState(false);

  useEffect(() => {
    setLocalData(node.data);
    setHasChanges(false);
  }, [node]);

  const handleDataChange = (field: string, value: any) => {
    const newData = { ...localData, [field]: value };
    setLocalData(newData);
    setHasChanges(true);
  };

  const handleNestedDataChange = (parentField: string, field: string, value: any) => {
    const newData = {
      ...localData,
      [parentField]: {
        ...(localData as any)[parentField],
        [field]: value
      }
    };
    setLocalData(newData);
    setHasChanges(true);
  };

  const handleSave = () => {
    const updatedNode: FlowNode = {
      ...node,
      data: localData
    };
    onUpdate(updatedNode);
    setHasChanges(false);
  };

  const renderPromptNodeFields = (data: PromptNodeData) => (
    <>
      <TextField
        label="Content"
        multiline
        rows={4}
        value={data.content || ''}
        onChange={(e) => handleDataChange('content', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />

      <FormControl fullWidth sx={{ mb: 2 }}>
        <InputLabel>Model</InputLabel>
        <Select
          value={data.model || ''}
          label="Model"
          onChange={(e) => handleDataChange('model', e.target.value)}
        >
          <MenuItem value="gpt-4">GPT-4</MenuItem>
          <MenuItem value="gpt-4o">GPT-4o</MenuItem>
          <MenuItem value="gpt-3.5-turbo">GPT-3.5 Turbo</MenuItem>
          <MenuItem value="claude-3-opus">Claude 3 Opus</MenuItem>
          <MenuItem value="claude-3-sonnet">Claude 3 Sonnet</MenuItem>
          <MenuItem value="claude-3-haiku">Claude 3 Haiku</MenuItem>
        </Select>
      </FormControl>

      <TextField
        label="System Message"
        multiline
        rows={2}
        value={data.systemMessage || ''}
        onChange={(e) => handleDataChange('systemMessage', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />

      <Typography variant="subtitle2" sx={{ mb: 1 }}>Model Parameters</Typography>
      
      <Box sx={{ mb: 2 }}>
        <Typography variant="body2" gutterBottom>
          Temperature: {data.parameters?.temperature || 0.7}
        </Typography>
        <Slider
          value={data.parameters?.temperature || 0.7}
          onChange={(_, value) => handleNestedDataChange('parameters', 'temperature', value)}
          min={0}
          max={2}
          step={0.1}
          marks
          valueLabelDisplay="auto"
        />
      </Box>

      <TextField
        label="Max Tokens"
        type="number"
        value={data.parameters?.maxTokens || 1000}
        onChange={(e) => handleNestedDataChange('parameters', 'maxTokens', parseInt(e.target.value))}
        fullWidth
        sx={{ mb: 2 }}
      />

      <Box sx={{ mb: 2 }}>
        <Typography variant="body2" gutterBottom>
          Top P: {data.parameters?.topP || 1}
        </Typography>
        <Slider
          value={data.parameters?.topP || 1}
          onChange={(_, value) => handleNestedDataChange('parameters', 'topP', value)}
          min={0}
          max={1}
          step={0.05}
          marks
          valueLabelDisplay="auto"
        />
      </Box>
    </>
  );

  const renderVariableNodeFields = (data: VariableNodeData) => (
    <>
      <TextField
        label="Variable Name"
        value={data.name || ''}
        onChange={(e) => handleDataChange('name', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />

      <FormControl fullWidth sx={{ mb: 2 }}>
        <InputLabel>Type</InputLabel>
        <Select
          value={data.type || 'string'}
          label="Type"
          onChange={(e) => handleDataChange('type', e.target.value)}
        >
          <MenuItem value="string">String</MenuItem>
          <MenuItem value="number">Number</MenuItem>
          <MenuItem value="boolean">Boolean</MenuItem>
          <MenuItem value="json">JSON</MenuItem>
        </Select>
      </FormControl>

      <TextField
        label="Default Value"
        value={data.defaultValue || ''}
        onChange={(e) => handleDataChange('defaultValue', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />

      <TextField
        label="Description"
        multiline
        rows={2}
        value={data.description || ''}
        onChange={(e) => handleDataChange('description', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />
    </>
  );

  const renderConditionalNodeFields = (data: ConditionalNodeData) => (
    <>
      <TextField
        label="Left Operand"
        value={typeof data.condition?.leftOperand === 'string' ? data.condition.leftOperand : data.condition?.leftOperand?.name || ''}
        onChange={(e) => handleNestedDataChange('condition', 'leftOperand', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />

      <FormControl fullWidth sx={{ mb: 2 }}>
        <InputLabel>Operator</InputLabel>
        <Select
          value={data.condition?.operator || 'equals'}
          label="Operator"
          onChange={(e) => handleNestedDataChange('condition', 'operator', e.target.value)}
        >
          <MenuItem value="equals">Equals</MenuItem>
          <MenuItem value="contains">Contains</MenuItem>
          <MenuItem value="greater_than">Greater Than</MenuItem>
          <MenuItem value="less_than">Less Than</MenuItem>
          <MenuItem value="exists">Exists</MenuItem>
        </Select>
      </FormControl>

      <TextField
        label="Right Operand"
        value={typeof data.condition?.rightOperand === 'string' ? data.condition.rightOperand : data.condition?.rightOperand?.name || ''}
        onChange={(e) => handleNestedDataChange('condition', 'rightOperand', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />
    </>
  );

  const renderGenericFields = () => (
    <>
      <TextField
        label="Label"
        value={localData.label || ''}
        onChange={(e) => handleDataChange('label', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />

      <TextField
        label="Description"
        multiline
        rows={2}
        value={localData.description || ''}
        onChange={(e) => handleDataChange('description', e.target.value)}
        fullWidth
        sx={{ mb: 2 }}
      />
    </>
  );

  const renderNodeSpecificFields = () => {
    switch (node.type) {
      case NodeType.PROMPT:
        return renderPromptNodeFields(localData as PromptNodeData);
      case NodeType.VARIABLE:
        return renderVariableNodeFields(localData as VariableNodeData);
      case NodeType.CONDITIONAL:
        return renderConditionalNodeFields(localData as ConditionalNodeData);
      default:
        return null;
    }
  };

  return (
    <Paper className="node-property-panel" elevation={2} sx={{ borderRadius: 0 }}>
      <Box className="panel-header">
        <Typography variant="h6" component="div">
          {node.type.charAt(0).toUpperCase() + node.type.slice(1)} Properties
        </Typography>
        <IconButton onClick={onClose} size="small">
          <Close />
        </IconButton>
      </Box>

      <Box className="panel-content">
        <Box sx={{ mb: 2 }}>
          <Chip
            label={node.type}
            size="small"
            color="primary"
            variant="outlined"
          />
        </Box>

        <Divider sx={{ mb: 2 }} />

        {renderGenericFields()}
        
        <Divider sx={{ mb: 2 }} />
        
        {renderNodeSpecificFields()}
      </Box>

      <Box className="panel-actions">
        <Button
          variant="outlined"
          onClick={onClose}
          size="small"
        >
          Cancel
        </Button>
        <Button
          variant="contained"
          onClick={handleSave}
          disabled={!hasChanges}
          startIcon={<Save />}
          size="small"
        >
          Save
        </Button>
      </Box>
    </Paper>
  );
};

export default NodePropertyPanel;
