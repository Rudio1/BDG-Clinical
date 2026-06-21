<script setup lang="ts">
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';

import { useAuth } from '@/composables/useAuth';
import { useNotificacao } from '@/composables/useNotificacao';
import { useTratarErroFormulario } from '@/composables/useTratarErroFormulario';

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
  <q-page class="auth-page">
    <section class="auth-panel">
      <div class="brand">
        <q-icon name="health_and_safety" size="40px" color="primary" />
        <div>
          <h1>BGD Clinical</h1>
          <p>Ambiente administrativo</p>
        </div>
      </div>

      <q-form class="login-form" @submit.prevent="entrar">
        <q-input
          v-model="form.email"
          label="E-mail"
          type="email"
          outlined
          autocomplete="email"
          :rules="[(value: string) => Boolean(value) || 'Informe o e-mail']"
        >
          <template #prepend>
            <q-icon name="mail" />
          </template>
        </q-input>

        <q-input
          v-model="form.senha"
          label="Senha"
          :type="mostrarSenha ? 'text' : 'password'"
          outlined
          autocomplete="current-password"
          :rules="[(value: string) => Boolean(value) || 'Informe a senha']"
        >
          <template #prepend>
            <q-icon name="lock" />
          </template>
          <template #append>
            <q-btn
              flat
              round
              dense
              :icon="mostrarSenha ? 'visibility_off' : 'visibility'"
              @click="mostrarSenha = !mostrarSenha"
            />
          </template>
        </q-input>

        <q-btn
          color="primary"
          label="Entrar"
          type="submit"
          unelevated
          no-caps
          class="full-width"
          :loading="carregando"
        />
      </q-form>
    </section>
  </q-page>
</template>
