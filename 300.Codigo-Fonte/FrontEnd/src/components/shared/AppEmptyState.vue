<script setup lang="ts">
defineProps<{
  icon: string;
  titulo: string;
  texto: string;
}>();
</script>

<template>
  <div class="empty-state">
    <q-icon :name="icon" size="48px" color="grey-6" />
    <h2>{{ titulo }}</h2>
    <p>{{ texto }}</p>
  </div>
</template>
