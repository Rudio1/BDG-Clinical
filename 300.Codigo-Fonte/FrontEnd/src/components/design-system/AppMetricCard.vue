<script setup lang="ts">
defineProps<{
  label: string;
  icon: string;
  valor: string | number;
}>();
</script>

<template>
  <q-card flat bordered class="metric-card">
    <q-card-section class="row items-center no-wrap q-gutter-md">
      <q-avatar color="primary" text-color="white" :icon="icon" />
      <div>
        <div class="metric-value">{{ valor }}</div>
        <div class="metric-label">{{ label }}</div>
      </div>
    </q-card-section>
  </q-card>
</template>
