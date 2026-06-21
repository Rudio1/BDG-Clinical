<script setup lang="ts">
const indicadores = [
  { label: 'Atendimentos', valor: '0', icon: 'assignment' },
  { label: 'Pacientes', valor: '0', icon: 'groups' },
  { label: 'Pendências', valor: '0', icon: 'task_alt' },
];
</script>

<template>
  <q-page padding class="dashboard-page">
    <app-page-header
      titulo="Dashboard"
      subtitulo="Visão inicial da operação"
    />

    <div class="row q-col-gutter-md q-mt-sm">
      <div
        v-for="item in indicadores"
        :key="item.label"
        class="col-12 col-sm-6 col-md-4"
      >
        <q-card flat bordered class="metric-card">
          <q-card-section class="row items-center no-wrap q-gutter-md">
            <q-avatar color="primary" text-color="white" :icon="item.icon" />
            <div>
              <div class="metric-value">{{ item.valor }}</div>
              <div class="metric-label">{{ item.label }}</div>
            </div>
          </q-card-section>
        </q-card>
      </div>
    </div>

    <q-card flat bordered class="q-mt-md">
      <q-card-section>
        <app-empty-state
          icon="inventory_2"
          titulo="Nenhum módulo configurado"
          texto="Adicione entidades, serviços e páginas conforme o produto final."
        />
      </q-card-section>
    </q-card>
  </q-page>
</template>
