# 05 — Componentes

## Camadas de componentes

```
Quasar (q-*)
    ↓ estilizado via quasar.variables.scss
Componentes App* (design-system/)
    ↓ composição de Quasar + tokens
Componentes shared/ (AppPageHeader, AppEmptyState)
    ↓ padrões de página
Pages (pages/)
```

## Quasar — configuração padrão

### Botões

| Variante | Props | Uso |
|----------|-------|-----|
| Primário | `color="primary" unelevated no-caps` | Ação principal (Salvar, Entrar) |
| Secundário | `outline color="primary" no-caps` | Ação alternativa |
| Texto | `flat color="primary" no-caps` | Links de navegação (Cadastre-se) |
| Ícone | `flat round dense` | Toggle senha, menu |

```vue
<q-btn color="primary" label="Salvar" unelevated no-caps />
<q-btn flat no-caps color="primary" label="Cancelar" />
```

### Inputs

Sempre `outlined`. Ícone prepend para contexto visual.

```vue
<q-input v-model="email" label="E-mail" outlined>
  <template #prepend>
    <q-icon name="mail" />
  </template>
</q-input>
```

### Cards

```vue
<q-card flat bordered>
  <q-card-section>...</q-card-section>
</q-card>
```

### Avatares (métricas)

```vue
<q-avatar color="primary" text-color="white" icon="groups" />
```

### Notificações

Via composable `useNotificacao()`:

```typescript
notificacao.sucesso('Operação concluída');
notificacao.erro('Falha na operação');
notificacao.aviso('Atenção');
notificacao.info('Informação');
```

## Componentes App* (design-system/)

### AppBrand

Logo + título + subtítulo opcional.

```vue
<app-brand subtitulo="Ambiente administrativo" />
<app-brand titulo="Minha Clínica" subtitulo="Painel" icone="local_hospital" />
```

| Prop | Tipo | Default |
|------|------|---------|
| `titulo` | string | `DesignSystemBrand.nome` |
| `subtitulo` | string | — |
| `icone` | string | `health_and_safety` |
| `tamanhoIcone` | string | `40px` |

### AppAuthPanel

Wrapper completo para páginas de autenticação. Inclui `AppBrand` + slot.

```vue
<app-auth-panel subtitulo="Ambiente administrativo">
  <q-form class="form-stack" @submit.prevent="entrar">
    <!-- campos -->
  </q-form>
</app-auth-panel>
```

### AppMetricCard

Card de indicador numérico para dashboards.

```vue
<app-metric-card label="Pacientes" valor="128" icon="groups" />
```

| Prop | Tipo | Descrição |
|------|------|-----------|
| `label` | string | Rótulo do indicador |
| `valor` | string \| number | Valor exibido |
| `icon` | string | Material icon name |

## Componentes shared/

### AppPageHeader

Cabeçalho de página com slot para ações à direita.

```vue
<app-page-header titulo="Pacientes" subtitulo="Gerencie os pacientes da clínica">
  <q-btn color="primary" unelevated no-caps label="Novo paciente" />
</app-page-header>
```

### AppEmptyState

Estado vazio para listas e seções sem dados.

```vue
<app-empty-state
  icon="inventory_2"
  titulo="Nenhum registro"
  texto="Cadastre o primeiro item para começar."
/>
```

## Classes de padrão (SCSS)

| Classe | Descrição |
|--------|-----------|
| `.auth-page` | Container full-viewport para auth |
| `.auth-panel` | Painel centralizado de auth |
| `.form-stack` | Grid vertical com gap entre campos |
| `.page-header` | Flex header título + ações |
| `.page-content` | Max-width do conteúdo |
| `.metric-card` | Card de métrica |
| `.empty-state` | Container de estado vazio |
| `.drawer-brand` | Brand no drawer lateral |
| `.ds-surface-card` | Card genérico com borda e sombra |
| `.ds-text-*` | Utilitários tipográficos |

## Registro global

Componentes App* são registrados em `src/boot/components.ts` e disponíveis em toda a aplicação sem import.

## Quando criar novo componente

Criar em `design-system/` quando:
- O padrão se repete em **3+ telas**
- Há composição Quasar + tokens específica do produto
- O componente encapsula layout (não lógica de negócio)

Criar em `shared/` quando:
- É composição de página (header, empty state, filtros)
- Pode conter slot para ações customizadas

**Não criar** componente para wrappers triviais de um único `q-btn`.
