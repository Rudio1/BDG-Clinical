<script setup lang="ts">
import { ref } from 'vue';

import { useAuth } from '@/composables/useAuth';
import { DesignSystemBrand, DesignSystemLayout } from '@/constants/design-system';

const drawer = ref(true);
const auth = useAuth();
const { usuario, logout } = auth;
</script>

<template>
  <q-layout view="hHh Lpr lFf" class="shell-premium">
    <q-header class="text-dark">
      <q-toolbar :style="{ minHeight: DesignSystemLayout.headerHeight }">
        <q-btn
          flat
          round
          dense
          icon="menu"
          aria-label="Abrir menu"
          @click="drawer = !drawer"
        />
        <q-toolbar-title class="text-weight-bold">
          {{ DesignSystemBrand.nome }}
        </q-toolbar-title>
        <q-btn flat round icon="account_circle" aria-label="Menu da conta">
          <q-menu>
            <q-list style="min-width: 240px">
              <q-item>
                <q-item-section>
                  <q-item-label class="text-weight-medium">
                    {{ usuario?.nome || 'Usuário' }}
                  </q-item-label>
                  <q-item-label caption>{{ usuario?.email }}</q-item-label>
                </q-item-section>
              </q-item>
              <q-separator />
              <q-item clickable v-close-popup @click="logout">
                <q-item-section avatar>
                  <q-icon name="logout" />
                </q-item-section>
                <q-item-section>Sair</q-item-section>
              </q-item>
            </q-list>
          </q-menu>
        </q-btn>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="drawer"
      show-if-above
      :width="DesignSystemLayout.drawerWidth"
    >
      <div class="drawer-brand">
        <q-icon :name="DesignSystemBrand.icone" color="primary" size="28px" />
        <div>
          <strong>{{ DesignSystemBrand.nome }}</strong>
          <span>{{ DesignSystemBrand.taglinePadrao }}</span>
        </div>
      </div>

      <q-list padding>
        <q-item clickable v-ripple to="/" exact>
          <q-item-section avatar>
            <q-icon name="space_dashboard" />
          </q-item-section>
          <q-item-section>Início</q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>
