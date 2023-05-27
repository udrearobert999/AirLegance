import React from 'react';
import ReactDOM from 'react-dom/client';
import {
  StyledEngineProvider,
  ThemeProvider,
  createTheme,
} from '@mui/material/styles';

import './index.css';

import { AuthProvider } from './Contexts/AuthProvider';

import App from './App';

import { BrowserRouter } from 'react-router-dom';
import { SnackbarProvider } from './Contexts/SnackbarProvider';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <StyledEngineProvider injectFirst>
      <ThemeProvider theme={createTheme()}>
        <SnackbarProvider>
          <BrowserRouter>
            <AuthProvider>
              <App />
            </AuthProvider>
          </BrowserRouter>
        </SnackbarProvider>
      </ThemeProvider>
    </StyledEngineProvider>
  </React.StrictMode>
);
