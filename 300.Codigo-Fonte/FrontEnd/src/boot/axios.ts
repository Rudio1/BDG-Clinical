import axios, { AxiosError } from 'axios';

import type { ApiError } from '@/types/api/api';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:7001',
  timeout: 30000,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('auth.token');

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error: AxiosError<ApiError>) => {
    const detail = error.response?.data?.detail || error.response?.data?.message;
    const message = detail || error.message || 'Erro ao processar a requisição.';

    return Promise.reject(new Error(message));
  },
);
