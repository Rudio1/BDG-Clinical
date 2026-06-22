import { createRouter, createWebHistory } from 'vue-router';

import { useAuthStore } from '@/stores/auth.store';

import { routes } from './routes';

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to) => {
  const authStore = useAuthStore();
  const permissao = to.meta.permissao;

  if (to.meta.publica) {
    if (authStore.isAutenticado && (to.name === 'login' || to.name === 'cadastro')) {
      return { name: 'dashboard' };
    }

    return true;
  }

  if (!authStore.isAutenticado) {
    return {
      name: 'login',
      query: { redirect: to.fullPath },
    };
  }

  if (permissao && !authStore.possuiPermissao(permissao)) {
    return { name: 'dashboard' };
  }

  return true;
});

export default router;
