<script setup lang="ts">
import { ref } from 'vue';

import { useAuth } from '@/composables/useAuth';

const drawer = ref(true);
const auth = useAuth();
const { usuario, logout } = auth;
</script>

<template>
  <q-layout view="hHh Lpr lFf">
    <q-header bordered class="bg-white text-dark">
      <q-toolbar>
        <q-btn flat round dense icon="menu" @click="drawer = !drawer" />
        <q-toolbar-title>BGD Clinical</q-toolbar-title>
        <q-btn flat round icon="account_circle">
          <q-menu>
            <q-list style="min-width: 220px">
              <q-item>
                <q-item-section>
                  <q-item-label>{{ usuario?.nome || 'Usuário' }}</q-item-label>
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

    <q-drawer v-model="drawer" show-if-above bordered :width="260">
      <div class="drawer-brand">
        <q-icon name="health_and_safety" color="primary" size="32px" />
        <div>
          <strong>BGD Clinical</strong>
          <span>Whitelabel</span>
        </div>
      </div>

      <q-list padding>
        <q-item clickable v-ripple to="/" exact>
          <q-item-section avatar>
            <q-icon name="dashboard" />
          </q-item-section>
          <q-item-section>Dashboard</q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>
