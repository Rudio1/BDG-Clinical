# 01 — Fundamentos

## Princípios

1. **Clareza clínica** — interfaces legíveis, hierarquia visual óbvia, baixa carga cognitiva.
2. **Consistência** — mesmos padrões em auth, dashboard, formulários e listagens.
3. **Tokens first** — toda decisão visual passa por tokens; componentes consomem tokens, não valores soltos.
4. **Quasar como base** — componentes nativos do Quasar estilizados via tema; wrappers App* apenas quando há composição repetida.
5. **Whitelabel-ready** — cores de marca centralizadas em variáveis CSS sobrescrevíveis.

## Stack

| Tecnologia | Versão | Papel |
|------------|--------|-------|
| Vue 3 | 3.5+ | Framework reativo |
| Quasar | 2.18+ | UI kit (layout, inputs, feedback) |
| TypeScript | 5.9+ | Tipagem |
| SCSS | sass-embedded | Tokens e padrões |
| Vite | 7+ | Build |

## Estrutura de pastas (front-end)

```
src/
├── boot/                 # Registro global (axios, componentes)
├── components/
│   ├── design-system/    # Componentes visuais do DS
│   └── shared/           # Composição de página (header, empty state)
├── composables/          # Lógica reutilizável
├── constants/
│   └── design-system/    # Tokens TS
├── css/
│   └── design-system/    # Tokens SCSS
├── layouts/              # MainLayout, AuthLayout
├── pages/                # Rotas por domínio (auth/, dashboard/)
├── router/
├── services/
├── stores/
└── types/
```

## Fluxo de estilos

```
index.html (fonte Plus Jakarta Sans)
    ↓
main.ts → quasar CSS + app.scss
    ↓
app.scss → @use design-system/*
    ↓
quasar.variables.scss → tema Quasar compilado
```

## Nomenclatura

| Elemento | Padrão | Exemplo |
|----------|--------|---------|
| Token CSS | `--ds-{categoria}-{nome}` | `--ds-color-primary-500` |
| Classe utilitária DS | `ds-{nome}` | `ds-text-secondary` |
| Classe de padrão | nome descritivo | `auth-panel`, `form-stack` |
| Componente DS | `App` + substantivo | `AppBrand`, `AppAuthPanel` |
| Constante TS | `DesignSystem` + categoria | `DesignSystemColors` |

## Decisões de produto

- **Idioma da UI:** Português (pt-BR)
- **Densidade:** Confortável (não compacta) — adequada para uso prolongado em clínicas
- **Modo escuro:** Não implementado na v1; tokens preparados para extensão futura via `[data-theme="dark"]`
