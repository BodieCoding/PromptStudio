import React from 'react';
import { Box, Typography, Chip, List, ListItem, ListItemText, Paper } from '@mui/material';
import { Info, Mouse, KeyboardCommandKey, TouchApp } from '@mui/icons-material';

const SmartFeaturesHelp: React.FC = () => {
  return (
    <Paper 
      elevation={1} 
      sx={{ 
        position: 'absolute', 
        top: 16, 
        right: 16, 
        width: 300, 
        p: 2, 
        zIndex: 1000,
        backgroundColor: 'rgba(255, 255, 255, 0.95)',
        backdropFilter: 'blur(8px)'
      }}
    >
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
        <Info color="primary" sx={{ mr: 1 }} />
        <Typography variant="h6" component="div">
          Smart Features Guide
        </Typography>
      </Box>

      <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
        Get intelligent workflow suggestions:
      </Typography>

      <List dense>
        <ListItem>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <Mouse fontSize="small" />
            <ListItemText 
              primary="Right-click node"
              secondary="Smart context menu"
            />
          </Box>
        </ListItem>

        <ListItem>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <KeyboardCommandKey fontSize="small" />
            <ListItemText 
              primary="Ctrl/Cmd + Click"
              secondary="Alternative smart menu"
            />
          </Box>
        </ListItem>

        <ListItem>
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            <TouchApp fontSize="small" />
            <ListItemText 
              primary="Double-click node"
              secondary="Quick suggestions"
            />
          </Box>
        </ListItem>
      </List>

      <Box sx={{ mt: 2, pt: 2, borderTop: '1px solid #e0e0e0' }}>
        <Typography variant="caption" color="text.secondary">
          💡 Property changes now save automatically!
        </Typography>
      </Box>
    </Paper>
  );
};

export default SmartFeaturesHelp;
