<script setup lang="ts">
import { DesignSystemBrand } from '@/constants/design-system';

withDefaults(
  defineProps<{
    titulo?: string;
    subtitulo?: string;
    icone?: string;
    tamanhoIcone?: string;
  }>(),
  {
    titulo: DesignSystemBrand.nome,
    icone: DesignSystemBrand.icone,
    tamanhoIcone: '40px',
  },
);
</script>

<template>
  <div class="brand">
    <q-icon :name="icone" :size="tamanhoIcone" color="primary" />
    <div>
      <h1>{{ titulo }}</h1>
      <p v-if="subtitulo">{{ subtitulo }}</p>
    </div>
  </div>
</template>
