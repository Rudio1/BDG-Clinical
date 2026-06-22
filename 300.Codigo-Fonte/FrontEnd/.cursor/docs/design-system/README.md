# Design System — BGD Clinical

Sistema visual unificado para o front-end administrativo clínico. Objetivo: consistência, acessibilidade e suporte futuro a whitelabel.

## Documentação

| # | Tópico | Arquivo |
|---|--------|---------|
| 1 | Fundamentos e estrutura | [01-fundamentos.md](./01-fundamentos.md) |
| 2 | Cores e semânticas | [02-cores.md](./02-cores.md) |
| 3 | Tipografia | [03-tipografia.md](./03-tipografia.md) |
| 4 | Espaçamento e layout | [04-espacamento-layout.md](./04-espacamento-layout.md) |
| 5 | Componentes | [05-componentes.md](./05-componentes.md) |
| 6 | Padrões de página | [06-padroes-pagina.md](./06-padroes-pagina.md) |

## Implementação no código

```
src/
├── css/
│   ├── design-system/
│   │   ├── _tokens.scss      ← fonte da verdade (CSS variables)
│   │   ├── _typography.scss  ← estilos base de texto
│   │   └── _patterns.scss    ← padrões compostos (auth, header, etc.)
│   ├── quasar.variables.scss ← tema Quasar (SASS)
│   └── app.scss              ← entry point de estilos
├── constants/design-system/  ← tokens para TS/JS
└── components/
    ├── design-system/        ← AppBrand, AppAuthPanel, AppMetricCard
    └── shared/               ← AppPageHeader, AppEmptyState
```

## Identidade visual

- **Nome:** BGD Clinical
- **Ícone:** `health_and_safety` (Material Icons)
- **Tom:** Profissional, clínico, confiável — limpo e funcional
- **Primária:** Verde clínico `#059669`
- **Secundária:** Branco `#FFFFFF` (superfícies e contraste)
- **Fonte:** Plus Jakarta Sans

## Quick reference

```scss
// Correto
.card {
  background: var(--ds-bg-surface);
  border: 1px solid var(--ds-border-default);
  border-radius: var(--ds-radius-md);
  padding: var(--ds-space-6);
}

// Incorreto
.card {
  background: #fff;
  border: 1px solid #dde3ea;
  border-radius: 8px;
  padding: 24px;
}
```

```vue
<!-- Correto -->
<app-auth-panel subtitulo="Ambiente administrativo">
  <q-form class="form-stack">...</q-form>
</app-auth-panel>

<!-- Incorreto — duplicar estrutura auth manualmente -->
<q-page class="auth-page">
  <section style="padding: 32px">...</section>
</q-page>
```
