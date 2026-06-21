<script setup lang="ts">
import { DesignSystemAuth, DesignSystemBrand } from '@/constants/design-system';

defineProps<{
  variant: 'login' | 'register';
  formTitle: string;
  formSubtitle: string;
}>();

const content = {
  login: DesignSystemAuth.login,
  register: DesignSystemAuth.register,
};
</script>

<template>
  <q-page class="auth-premium">
    <aside class="auth-premium__visual" aria-hidden="true">
      <div class="auth-premium__visual-bg" />
      <div
        class="auth-premium__visual-image"
        :style="{ backgroundImage: `url(${DesignSystemAuth.heroImage})` }"
      />
      <div class="auth-premium__visual-overlay" />
      <div class="auth-premium__orb auth-premium__orb--1 ds-animate-float" />
      <div class="auth-premium__orb auth-premium__orb--2 ds-animate-float-delayed" />

      <div class="auth-premium__visual-content ds-animate-fade-in">
        <div class="auth-premium__visual-logo">
          <span class="auth-premium__visual-logo-icon">
            <q-icon :name="DesignSystemBrand.icone" size="24px" />
          </span>
          {{ DesignSystemBrand.nome }}
        </div>

        <h2 class="auth-premium__visual-headline">
          {{ content[variant].headline }}
        </h2>
        <p class="auth-premium__visual-subline">
          {{ content[variant].subline }}
        </p>

        <div class="auth-premium__floating-cards">
          <div
            v-for="(stat, index) in DesignSystemAuth.stats"
            :key="stat.label"
            class="auth-premium__float-card"
            :class="index === 1 ? 'ds-animate-float-delayed' : 'ds-animate-float'"
          >
            <strong>{{ stat.value }}</strong>
            <span>{{ stat.label }}</span>
          </div>
        </div>
      </div>
    </aside>

    <section class="auth-premium__form-panel">
      <div class="auth-premium__form-inner ds-animate-fade-in-up">
        <header class="auth-premium__form-header">
          <h1>{{ formTitle }}</h1>
          <p>{{ formSubtitle }}</p>
        </header>

        <slot />
      </div>
    </section>
  </q-page>
</template>
