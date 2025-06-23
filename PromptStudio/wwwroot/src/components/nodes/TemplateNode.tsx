import React, { useState, useEffect } from 'react';
import { Handle, Position } from 'reactflow';
import { 
  Paper, 
  Typography, 
  Box, 
  Chip, 
  Tooltip,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
  TextField,
  IconButton,
  Collapse
} from '@mui/material';
import { 
  Description, 
  ExpandMore, 
  ExpandLess,
  Edit,
  PlayArrow
} from '@mui/icons-material';
import { TemplateNodeData } from '../../types/flow-types';

interface TemplateNodeProps {
  data: TemplateNodeData;
  selected: boolean;
}

interface PromptTemplate {
  id: number;
  name: string;
  description?: string;
  content: string;
  variables: string[];
}

const TemplateNode: React.FC<TemplateNodeProps> = ({ data, selected }) => {
  const [templates, setTemplates] = useState<PromptTemplate[]>([]);
  const [selectedTemplate, setSelectedTemplate] = useState<PromptTemplate | null>(null);
  const [expanded, setExpanded] = useState(false);
  const [variableValues, setVariableValues] = useState<Record<string, string>>({});

  useEffect(() => {
    // Load available templates
    loadTemplates();
  }, []);

  useEffect(() => {
    // Update selected template when data changes
    if (data.templateId) {
      const template = templates.find(t => t.id.toString() === data.templateId);
      setSelectedTemplate(template || null);
      
      // Initialize variable values from data
      if (template && data.variables) {
        setVariableValues(data.variables);
      }
    }
  }, [data.templateId, templates, data.variables]);

  const loadTemplates = async () => {
    try {
      // TODO: Replace with actual API call
      const mockTemplates: PromptTemplate[] = [
        {
          id: 1,
          name: "Customer Support Response",
          description: "Generate helpful customer support responses",
          content: "You are a helpful customer support agent. Respond to the customer's question: {{question}} with empathy and provide a solution.",
          variables: ["question"]
        },
        {
          id: 2,
          name: "Code Review",
          description: "Review code and provide feedback",
          content: "Please review the following {{language}} code and provide constructive feedback:\n\n{{code}}\n\nFocus on: {{focus_areas}}",
          variables: ["language", "code", "focus_areas"]
        },
        {
          id: 3,
          name: "Content Summarizer",
          description: "Summarize long content",
          content: "Please summarize the following content in {{length}} sentences:\n\n{{content}}",
          variables: ["content", "length"]
        }
      ];
      setTemplates(mockTemplates);
    } catch (error) {
      console.error('Error loading templates:', error);
    }
  };

  const handleTemplateChange = (templateId: string) => {
    const template = templates.find(t => t.id.toString() === templateId);
    setSelectedTemplate(template || null);
    
    // Initialize variable values
    if (template) {
      const initialValues: Record<string, string> = {};
      template.variables.forEach(varName => {
        initialValues[varName] = data.variables?.[varName] || '';
      });
      setVariableValues(initialValues);
      
      // Update node data
      if (data.onUpdate) {
        data.onUpdate({
          ...data,
          templateId: templateId,
          variables: initialValues
        });
      }
    }
  };

  const handleVariableChange = (varName: string, value: string) => {
    const newValues = { ...variableValues, [varName]: value };
    setVariableValues(newValues);
    
    // Update node data
    if (data.onUpdate) {
      data.onUpdate({
        ...data,
        variables: newValues
      });
    }
  };

  const getPreviewContent = () => {
    if (!selectedTemplate) return '';
    
    let content = selectedTemplate.content;
    Object.entries(variableValues).forEach(([varName, value]) => {
      const regex = new RegExp(`\\{\\{${varName}\\}\\}`, 'g');
      content = content.replace(regex, value || `[${varName}]`);
    });
    
    return content;
  };

  const truncateText = (text: string, maxLength: number = 80) => {
    return text.length > maxLength ? `${text.substring(0, maxLength)}...` : text;
  };

  return (
    <Tooltip title="Template Node - Uses a predefined prompt template" placement="top">
      <Paper
        elevation={selected ? 4 : 2}
        className={`custom-node node-template ${selected ? 'selected' : ''}`}
        sx={{
          minWidth: 250,
          maxWidth: 400,
          border: selected ? '2px solid #1976d2' : '2px solid transparent',
          transition: 'all 0.2s ease',
          cursor: 'pointer',
          '&:hover': {
            elevation: 3,
            borderColor: '#1976d2'
          }
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
            <Description sx={{ fontSize: 18, color: '#4caf50' }} />
          </Box>
          <Typography variant="subtitle2" component="div">
            {data.label || 'Template Node'}
          </Typography>
          
          <IconButton
            size="small"
            onClick={() => setExpanded(!expanded)}
            sx={{ ml: 'auto', p: 0.5 }}
          >
            {expanded ? <ExpandLess /> : <ExpandMore />}
          </IconButton>
        </Box>

        <Box className="custom-node-content">
          <FormControl fullWidth size="small" sx={{ mb: 1 }}>
            <InputLabel>Select Template</InputLabel>
            <Select
              value={data.templateId || ''}
              onChange={(e) => handleTemplateChange(e.target.value)}
              label="Select Template"
            >
              {templates.map((template) => (
                <MenuItem key={template.id} value={template.id.toString()}>
                  {template.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>

          {selectedTemplate && (
            <>
              <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mb: 1 }}>
                {selectedTemplate.description}
              </Typography>

              <Collapse in={expanded}>
                <Box sx={{ mt: 1 }}>
                  {selectedTemplate.variables.length > 0 && (
                    <>
                      <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mb: 1 }}>
                        Variables:
                      </Typography>
                      {selectedTemplate.variables.map((varName) => (
                        <TextField
                          key={varName}
                          fullWidth
                          size="small"
                          label={varName}
                          value={variableValues[varName] || ''}
                          onChange={(e) => handleVariableChange(varName, e.target.value)}
                          sx={{ mb: 1 }}
                        />
                      ))}
                    </>
                  )}

                  <Typography variant="caption" color="text.secondary" sx={{ display: 'block', mb: 0.5 }}>
                    Preview:
                  </Typography>
                  <Paper 
                    variant="outlined" 
                    sx={{ 
                      p: 1, 
                      bgcolor: '#f5f5f5', 
                      fontSize: '0.7rem',
                      maxHeight: 100,
                      overflow: 'auto'
                    }}
                  >
                    <Typography variant="body2" sx={{ fontSize: '0.7rem', whiteSpace: 'pre-wrap' }}>
                      {truncateText(getPreviewContent(), 200)}
                    </Typography>
                  </Paper>
                </Box>
              </Collapse>

              {!expanded && (
                <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5, mt: 1 }}>
                  {selectedTemplate.variables.map((varName) => (
                    <Chip
                      key={varName}
                      label={`${varName}: ${variableValues[varName] ? '✓' : '⚬'}`}
                      size="small"
                      variant="outlined"
                      color={variableValues[varName] ? 'primary' : 'default'}
                      sx={{ fontSize: '0.6rem', height: 20 }}
                    />
                  ))}
                </Box>
              )}

              <Box sx={{ display: 'flex', gap: 0.5, mt: 1 }}>
                <Chip
                  label={`${selectedTemplate.variables.length} vars`}
                  size="small"
                  variant="outlined"
                  sx={{ fontSize: '0.6rem', height: 20 }}
                />
                <Chip
                  label="Template"
                  size="small"
                  color="primary"
                  sx={{ fontSize: '0.6rem', height: 20 }}
                />
              </Box>
            </>
          )}
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

export default TemplateNode;
