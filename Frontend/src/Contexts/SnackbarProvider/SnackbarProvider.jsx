import React, { createContext, useContext, useState, useCallback } from 'react';
import { Snackbar, Alert } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import IconButton from '@mui/material/IconButton';

import { v4 as uuidv4 } from 'uuid';

export const SnackbarContext = createContext();

export const SnackbarProvider = ({ children }) => {
  const [snackbars, setSnackbars] = useState([]);

  const openSnackbar = useCallback((message, variant) => {
    setSnackbars((oldSnackbars) => [
      ...oldSnackbars,
      { id: uuidv4(), message, variant },
    ]);
  }, []);

  const closeSnackbar = useCallback((id) => {
    setSnackbars((oldSnackbars) =>
      oldSnackbars.filter((snackbar) => snackbar.id !== id)
    );
  }, []);

  const openSuccessSnackbar = useCallback(
    (message) => openSnackbar(message, 'success'),
    [openSnackbar]
  );
  const openErrorSnackbar = useCallback(
    (message) => openSnackbar(message, 'error'),
    [openSnackbar]
  );

  return (
    <SnackbarContext.Provider
      value={{ openSuccessSnackbar, openErrorSnackbar }}
    >
      {children}
      {snackbars.map((snackbar) => (
        <Snackbar
          id="snackbar"
          key={snackbar.id}
          anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
          open={true}
          autoHideDuration={3000}
          onClose={() => closeSnackbar(snackbar.id)}
          action={
            <IconButton
              size='small'
              color='inherit'
              onClick={() => closeSnackbar(snackbar.id)}
            >
              <CloseIcon fontSize='small' />
            </IconButton>
          }
        >
          <Alert
            onClose={() => closeSnackbar(snackbar.id)}
            severity={snackbar.variant}
            variant='filled'
          >
            {snackbar.message}
          </Alert>
        </Snackbar>
      ))}
    </SnackbarContext.Provider>
  );
};
