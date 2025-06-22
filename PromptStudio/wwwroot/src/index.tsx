import React from 'react';
import ReactDOM from 'react-dom/client';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import App from './App';
import './styles/global.css';

const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
    background: {
      default: '#f5f5f5',
    },
  },
  typography: {
    fontFamily: '"Roboto", "Helvetica", "Arial", sans-serif',
  },
});

// Global interface for integration with the main PromptStudio application
declare global {
  interface Window {
    PromptStudioFlowBuilder: {
      init: (containerId: string) => void;
      getCurrentFlow: () => any;
      loadFlow: (flow: any) => void;
      destroy: () => void;
    };
  }
}

let currentRoot: ReactDOM.Root | null = null;
let currentAppInstance: any = null;

// Component wrapper to expose methods
const AppWrapper = React.forwardRef<any>((props, ref) => {
  const appRef = React.useRef<any>(null);
  
  React.useImperativeHandle(ref, () => ({
    getCurrentFlow: () => {
      if (appRef.current && appRef.current.getCurrentFlow) {
        return appRef.current.getCurrentFlow();
      }
      return {
        id: null,
        name: 'Untitled Flow',
        description: '',
        variables: {},
        nodes: [],
        edges: [],
        tags: []
      };
    },
    loadFlow: (flow: any) => {
      if (appRef.current && appRef.current.loadFlow) {
        appRef.current.loadFlow(flow);
      }
    }
  }));

  return (
    <React.StrictMode>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <App ref={appRef} />
      </ThemeProvider>
    </React.StrictMode>
  );
});

// Global flow builder API
window.PromptStudioFlowBuilder = {
  init: (containerId: string) => {
    const container = document.getElementById(containerId);
    if (!container) {
      console.error(`Container with ID "${containerId}" not found`);
      return;
    }

    // Clean up previous instance
    if (currentRoot) {
      currentRoot.unmount();
    }

    // Create new React root and render the app
    currentRoot = ReactDOM.createRoot(container);
    currentRoot.render(<AppWrapper ref={(ref) => { currentAppInstance = ref; }} />);
  },

  getCurrentFlow: () => {
    if (currentAppInstance && currentAppInstance.getCurrentFlow) {
      return currentAppInstance.getCurrentFlow();
    }
    return {
      id: null,
      name: 'Untitled Flow',
      description: '',
      variables: {},
      nodes: [],
      edges: [],
      tags: []
    };
  },

  loadFlow: (flow: any) => {
    if (currentAppInstance && currentAppInstance.loadFlow) {
      currentAppInstance.loadFlow(flow);
    }
  },

  destroy: () => {
    if (currentRoot) {
      currentRoot.unmount();
      currentRoot = null;
      currentAppInstance = null;
    }
  }
};

// Auto-initialize if we're in standalone mode
const standaloneRoot = document.getElementById('root');
if (standaloneRoot) {
  const root = ReactDOM.createRoot(standaloneRoot);
  root.render(
    <React.StrictMode>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <App />
      </ThemeProvider>
    </React.StrictMode>
  );
}
