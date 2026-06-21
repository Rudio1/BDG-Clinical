import { computed } from 'vue';

import { useAuthStore } from '@/stores/auth.store';

export function usePermissao(permissao: string) {
  const authStore = useAuthStore();

  return computed(() => authStore.possuiPermissao(permissao));
}
