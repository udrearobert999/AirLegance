import { useContext } from 'react';
import { SnackbarContext } from 'Contexts/SnackbarProvider';

const useSnackbar = () => {
  return useContext(SnackbarContext);
};

export default useSnackbar;
