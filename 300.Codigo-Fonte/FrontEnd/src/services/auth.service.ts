import { api } from '@/boot/axios';
import type { ApiResponse } from '@/types/api/api';
import type { UsuarioAutenticado } from '@/types/entidades/usuario';

export interface LoginRequest {
  email: string;
  senha: string;
}

export interface LoginResponse {
  token: string;
  usuario: UsuarioAutenticado;
}

export const authService = {
  async login(payload: LoginRequest): Promise<LoginResponse> {
    const { data } = await api.post<ApiResponse<LoginResponse>>('/api/auth/login', payload);

    return data.data;
  },
  async me(): Promise<UsuarioAutenticado> {
    const { data } = await api.get<ApiResponse<UsuarioAutenticado>>('/api/auth/me');

    return data.data;
  },
};
