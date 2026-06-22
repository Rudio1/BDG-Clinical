<script setup lang="ts">
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';

import { useAuth } from '@/composables/useAuth';
import { useNotificacao } from '@/composables/useNotificacao';
import { useTratarErroFormulario } from '@/composables/useTratarErroFormulario';
import { DesignSystemAuth } from '@/constants/design-system';
import { IdentidadeConstants } from '@/constants/identidade';

const router = useRouter();
const auth = useAuth();
const notificacao = useNotificacao();
const { obterMensagem } = useTratarErroFormulario();
const { carregando } = auth;

const form = reactive({
  nomeEmpresa: '',
  nome: '',
  email: '',
  senha: '',
  cnpj: '',
});

const mostrarSenha = ref(false);

async function cadastrar(): Promise<void> {
  try {
    await auth.registrar({
      nomeEmpresa: form.nomeEmpresa,
      nome: form.nome,
      email: form.email,
      senha: form.senha,
      cnpj: form.cnpj || undefined,
    });

    await router.push({ name: 'dashboard' });
  } catch (error) {
    notificacao.erro(obterMensagem(error));
  }
}
</script>

<template>
  <app-auth-shell
    variant="register"
    :form-title="DesignSystemAuth.register.formTitle"
    :form-subtitle="DesignSystemAuth.register.formSubtitle"
  >
    <q-form
      class="auth-premium__form-stack auth-premium__form-stack--compact"
      @submit.prevent="cadastrar"
    >
      <q-input
        v-model="form.nomeEmpresa"
        label="Nome da clínica"
        outlined
        autocomplete="organization"
        class="ds-animate-fade-in-up ds-stagger-1"
        :rules="[(value: string) => Boolean(value) || 'Informe o nome da clínica']"
      />

      <q-input
        v-model="form.nome"
        label="Seu nome"
        outlined
        autocomplete="name"
        class="ds-animate-fade-in-up ds-stagger-2"
        :rules="[(value: string) => Boolean(value) || 'Informe o seu nome']"
      />

      <q-input
        v-model="form.email"
        label="E-mail"
        type="email"
        outlined
        autocomplete="email"
        class="ds-animate-fade-in-up ds-stagger-3"
        :rules="[(value: string) => Boolean(value) || 'Informe o e-mail']"
      />

      <q-input
        v-model="form.cnpj"
        label="CNPJ (opcional)"
        outlined
        mask="##.###.###/####-##"
        unmasked-value
        class="ds-animate-fade-in-up ds-stagger-4"
      />

      <q-input
        v-model="form.senha"
        label="Senha"
        :type="mostrarSenha ? 'text' : 'password'"
        outlined
        autocomplete="new-password"
        class="ds-animate-fade-in-up ds-stagger-5"
        :rules="[
          (value: string) => Boolean(value) || 'Informe a senha',
          (value: string) =>
            value.length >= IdentidadeConstants.senhaMinimaCaracteres ||
            `Mínimo de ${IdentidadeConstants.senhaMinimaCaracteres} caracteres`,
        ]"
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
        label="Começar agora"
        type="submit"
        unelevated
        no-caps
        class="full-width auth-premium__submit ds-animate-fade-in-up ds-stagger-6"
        :loading="carregando"
      />
    </q-form>

    <p class="auth-premium__footer-link">
      Já tem conta?
      <router-link :to="{ name: 'login' }">Entrar</router-link>
    </p>
  </app-auth-shell>
</template>
