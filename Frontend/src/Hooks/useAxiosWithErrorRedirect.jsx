import axios from 'axios';

import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import useSnackbar from './useSnackBar';

const API_ROUTE = import.meta.env.VITE_APP_API_ROUTE;
export const axiosWithErrorRedirect = axios.create({
  baseURL: API_ROUTE,
  headers: { 'Content-Type': 'application/json' },
});

const useAxiosWithErrorRedirect = () => {
  const navigate = useNavigate();
  const { openErrorSnackbar } = useSnackbar();

  useEffect(() => {
    const responseIntercept = axiosWithErrorRedirect.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.message && error.message.includes('Network Error')) {
          openErrorSnackbar('Network Error');
        }
        return Promise.reject(error);
      }
    );
    return () => {
      axiosWithErrorRedirect.interceptors.response.eject(responseIntercept);
    };
  }, [navigate]);

  return axiosWithErrorRedirect;
};

export default useAxiosWithErrorRedirect;
