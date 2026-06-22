<script setup lang="ts">
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';

import { useAuth } from '@/composables/useAuth';
import { useNotificacao } from '@/composables/useNotificacao';
import { useTratarErroFormulario } from '@/composables/useTratarErroFormulario';
import { DesignSystemAuth } from '@/constants/design-system';

const router = useRouter();
const auth = useAuth();
const notificacao = useNotificacao();
const { obterMensagem } = useTratarErroFormulario();
const { carregando } = auth;

const form = reactive({
  email: '',
  senha: '',
});

const mostrarSenha = ref(false);

async function entrar(): Promise<void> {
  try {
    await auth.login(form.email, form.senha);

    const redirect = router.currentRoute.value.query.redirect;

    if (typeof redirect === 'string') {
      await router.push(redirect);
    }
  } catch (error) {
    notificacao.erro(obterMensagem(error));
  }
}
</script>

<template>
  <app-auth-shell
    variant="login"
    :form-title="DesignSystemAuth.login.formTitle"
    :form-subtitle="DesignSystemAuth.login.formSubtitle"
  >
    <q-form class="auth-premium__form-stack" @submit.prevent="entrar">
      <q-input
        v-model="form.email"
        label="E-mail"
        type="email"
        outlined
        autocomplete="email"
        class="ds-animate-fade-in-up ds-stagger-1"
        :rules="[(value: string) => Boolean(value) || 'Informe o e-mail']"
      />

      <q-input
        v-model="form.senha"
        label="Senha"
        :type="mostrarSenha ? 'text' : 'password'"
        outlined
        autocomplete="current-password"
        class="ds-animate-fade-in-up ds-stagger-2"
        :rules="[(value: string) => Boolean(value) || 'Informe a senha']"
      >
        <template #append>
          <q-btn
            flat
            round
            dense
            :icon="mostrarSenha ? 'visibility_off' : 'visibility'"
            :aria-label="mostrarSenha ? 'Ocultar senha' : 'Mostrar senha'"
            @click="mostrarSenha = !mostrarSenha"
          />
        </template>
      </q-input>

      <q-btn
        color="primary"
        label="Continuar"
        type="submit"
        unelevated
        no-caps
        class="full-width auth-premium__submit ds-animate-fade-in-up ds-stagger-3"
        :loading="carregando"
      />
    </q-form>

    <p class="auth-premium__footer-link ds-animate-fade-in-up ds-stagger-4">
      Ainda não tem conta?
      <router-link :to="{ name: 'cadastro' }">Cadastre-se grátis</router-link>
    </p>
  </app-auth-shell>
</template>
