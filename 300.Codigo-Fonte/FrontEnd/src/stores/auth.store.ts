import { defineStore } from 'pinia';

import { authService, type LoginRequest, type RegistrarRequest } from '@/services/auth.service';
import type { UsuarioAutenticado } from '@/types/entidades/usuario';

interface AuthState {
  token: string | null;
  usuario: UsuarioAutenticado | null;
  carregando: boolean;
}

const tokenSalvo = localStorage.getItem('auth.token');
const usuarioSalvo = localStorage.getItem('auth.usuario');

export const useAuthStore = defineStore('auth', {
  state: (): AuthState => ({
    token: tokenSalvo,
    usuario: usuarioSalvo ? (JSON.parse(usuarioSalvo) as UsuarioAutenticado) : null,
    carregando: false,
  }),
  getters: {
    isAutenticado: (state) => Boolean(state.token),
    permissoes: (state) => state.usuario?.permissoes ?? [],
  },
  actions: {
    async login(payload: LoginRequest): Promise<void> {
      this.carregando = true;

      try {
        const response = await authService.login(payload);
        this.persistirSessao(response.token, response.usuario);
      } finally {
        this.carregando = false;
      }
    },
    async registrar(payload: RegistrarRequest): Promise<void> {
      this.carregando = true;

      try {
        const response = await authService.registrar(payload);
        this.persistirSessao(response.token, response.usuario);
      } finally {
        this.carregando = false;
      }
    },
    logout(): void {
      this.token = null;
      this.usuario = null;

      localStorage.removeItem('auth.token');
      localStorage.removeItem('auth.usuario');
    },
    possuiPermissao(permissao: string): boolean {
      return this.permissoes.includes(permissao);
    },
    persistirSessao(token: string, usuario: UsuarioAutenticado): void {
      this.token = token;
      this.usuario = usuario;

      localStorage.setItem('auth.token', token);
      localStorage.setItem('auth.usuario', JSON.stringify(usuario));
    },
  },
});
