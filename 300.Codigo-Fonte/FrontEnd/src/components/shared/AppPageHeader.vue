<script setup lang="ts">
defineProps<{
  titulo: string;
  subtitulo?: string;
}>();
</script>

<template>
  <header class="page-header">
    <div>
      <h1>{{ titulo }}</h1>
      <p v-if="subtitulo">{{ subtitulo }}</p>
    </div>
    <slot />
  </header>
</template>
