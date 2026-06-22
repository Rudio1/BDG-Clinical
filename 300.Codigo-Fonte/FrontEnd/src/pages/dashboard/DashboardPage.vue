<script setup lang="ts">
import { computed } from 'vue';

import { useAuth } from '@/composables/useAuth';

const auth = useAuth();
const { usuario } = auth;

const indicadores = [
  { label: 'Atendimentos hoje', valor: '0', icon: 'favorite' },
  { label: 'Pacientes ativos', valor: '0', icon: 'groups' },
  { label: 'Agenda da semana', valor: '0', icon: 'event_available' },
];

const saudacao = computed(() => {
  const hora = new Date().getHours();
  const nome = usuario.value?.nome?.split(' ')[0] || 'Olá';

  if (hora < 12) return `Bom dia, ${nome}`;
  if (hora < 18) return `Boa tarde, ${nome}`;

  return `Boa noite, ${nome}`;
});
</script>

<template>
  <q-page class="dashboard-premium">
    <section class="dashboard-premium__hero ds-animate-fade-in-up">
      <div class="dashboard-premium__hero-orb dashboard-premium__hero-orb--1 ds-animate-float" />
      <div
        class="dashboard-premium__hero-orb dashboard-premium__hero-orb--2 ds-animate-float-delayed"
      />

      <div class="dashboard-premium__hero-content">
        <h1 class="dashboard-premium__hero-greeting">{{ saudacao }}</h1>
        <p class="dashboard-premium__hero-text">
          Tudo pronto para um dia produtivo. Aqui está o resumo da sua clínica.
        </p>
      </div>
    </section>

    <div class="dashboard-premium__metrics">
      <article
        v-for="(item, index) in indicadores"
        :key="item.label"
        class="premium-metric-card ds-animate-fade-in-up"
        :class="`ds-stagger-${index + 1}`"
      >
        <div class="premium-metric-card__body">
          <div class="premium-metric-card__icon" aria-hidden="true">
            <q-icon :name="item.icon" size="26px" />
          </div>
          <div>
            <div class="premium-metric-card__value">{{ item.valor }}</div>
            <div class="premium-metric-card__label">{{ item.label }}</div>
          </div>
        </div>
      </article>
    </div>

    <section class="dashboard-premium__section ds-animate-fade-in-up ds-stagger-4">
      <h2 class="dashboard-premium__section-title">Próximos passos</h2>

      <div class="premium-empty-state">
        <div class="premium-empty-state__icon" aria-hidden="true">
          <q-icon name="auto_awesome" size="32px" />
        </div>
        <h2>Sua clínica está começando</h2>
        <p>
          Em breve você verá atendimentos, pacientes e insights aqui. Por enquanto,
          explore e configure seu espaço.
        </p>
      </div>
    </section>
  </q-page>
</template>
