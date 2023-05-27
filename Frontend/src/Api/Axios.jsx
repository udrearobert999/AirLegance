import axios from 'axios';

export const API_ROUTE = import.meta.env.VITE_APP_API_ROUTE;

export default axios.create({
  baseURL: API_ROUTE,
});

export const axiosPrivate = axios.create({
  baseURL: API_ROUTE,
  headers: { 'Content-Type': 'application/json' },
  withCredentials: true,
});
