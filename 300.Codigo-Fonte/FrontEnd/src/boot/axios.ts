import axios, { AxiosError } from 'axios';

import type { ApiError, ApiResponse } from '@/types/api/api';

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:7013',
  timeout: 30000,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('auth.token');

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

function extractErrorMessage(error: AxiosError<ApiError | ApiResponse<unknown>>): string {
  const payload = error.response?.data;

  if (payload && typeof payload === 'object') {
    if ('message' in payload && payload.message) {
      return payload.message;
    }

    if ('detail' in payload && payload.detail) {
      return payload.detail;
    }
  }

  return error.message || 'Erro ao processar a requisição.';
}

api.interceptors.response.use(
  (response) => response,
  (error: AxiosError<ApiError | ApiResponse<unknown>>) => {
    return Promise.reject(new Error(extractErrorMessage(error)));
  },
);
