import { storeToRefs } from 'pinia';
import { useRouter } from 'vue-router';

import { useAuthStore } from '@/stores/auth.store';

export function useAuth() {
  const router = useRouter();
  const authStore = useAuthStore();
  const { usuario, carregando, isAutenticado, permissoes } = storeToRefs(authStore);

  async function login(email: string, senha: string): Promise<void> {
    await authStore.login({ email, senha });
    await router.push({ name: 'dashboard' });
  }

  async function logout(): Promise<void> {
    authStore.logout();
    await router.push({ name: 'login' });
  }

  return {
    usuario,
    carregando,
    isAutenticado,
    permissoes,
    login,
    logout,
    possuiPermissao: authStore.possuiPermissao,
  };
}
