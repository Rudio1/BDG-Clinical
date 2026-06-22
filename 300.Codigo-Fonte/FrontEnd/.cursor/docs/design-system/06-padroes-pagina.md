# 06 — Padrões de página

Templates visuais para os tipos de tela do BGD Clinical.

## Auth (Login / Cadastro)

**Layout:** `AuthLayout` → `AppAuthPanel`

```
AppAuthPanel
├── AppBrand (título + subtítulo)
└── q-form.form-stack
    ├── q-input (campos)
    ├── q-btn primário (submit, full-width)
    └── q-btn flat (navegação alternativa, full-width)
```

### Checklist auth

- [ ] Usar `AppAuthPanel`, nunca montar `.auth-page` manualmente
- [ ] Formulário com classe `form-stack`
- [ ] Inputs `outlined` com ícone prepend
- [ ] Senha com toggle visibility (append icon button)
- [ ] Botão submit: `color="primary" unelevated no-caps full-width`
- [ ] Link alternativo: `flat no-caps color="primary" full-width`
- [ ] Loading no botão submit via `:loading="carregando"`

### Referência

- `src/pages/auth/LoginPage.vue`
- `src/pages/auth/RegisterPage.vue`

---

## Dashboard

**Layout:** `MainLayout` → `q-page.page-content`

```
q-page (padding)
├── AppPageHeader (titulo + subtitulo)
├── row q-col-gutter-md (métricas)
│   └── AppMetricCard × N
└── q-card (seção principal)
    └── AppEmptyState ou conteúdo
```

### Checklist dashboard

- [ ] `AppPageHeader` como primeiro elemento
- [ ] Métricas em grid responsivo (`col-12 col-sm-6 col-md-4`)
- [ ] Usar `AppMetricCard` para indicadores
- [ ] Seção principal em `q-card flat bordered`
- [ ] Empty state quando não há dados

### Referência

- `src/pages/dashboard/DashboardPage.vue`

---

## Listagem (CRUD index)

Template para telas de listagem futuras:

```
q-page.page-content (padding)
├── AppPageHeader
│   ├── titulo + subtitulo
│   └── q-btn "Novo" (slot direito)
├── q-card flat bordered (filtros — opcional)
│   └── row com q-input de busca
└── q-card flat bordered
    ├── q-table (dados)
    └── AppEmptyState (quando vazio)
```

### Convenções de tabela

- Paginação server-side quando > 50 registros
- Ações por linha: ícones `edit`, `delete` com `flat round dense`
- Status via `q-badge` com cores semânticas
- Loading via `:loading` do q-table

---

## Formulário (CRUD create/edit)

```
q-page.page-content (padding)
├── AppPageHeader (titulo = "Novo X" ou "Editar X")
├── q-card flat bordered
│   └── q-form.form-stack
│       ├── campos (max 2 colunas em md+)
│       └── row com botões (Salvar + Cancelar)
```

### Convenções de formulário

- Campos obrigatórios: validação via `:rules` do q-input
- Erros de API: `useTratarErroFormulario()` + `useNotificacao()`
- Botão salvar à esquerda, cancelar à direita (ou cancelar flat)
- Em mobile: botões full-width empilhados

---

## Detalhe (view)

```
q-page.page-content (padding)
├── AppPageHeader
│   ├── titulo (nome da entidade)
│   └── ações (Editar, Excluir)
├── q-card flat bordered (dados principais)
└── q-card flat bordered (seções relacionadas — tabs opcional)
```

---

## Erro 404

```
q-page (centralizado)
├── q-icon primary (64px)
├── h1.not-found-title
├── p.ds-text-secondary
└── q-btn primary → home
```

### Referência

- `src/pages/ErrorNotFound.vue`

---

## Feedback e estados

| Estado | Componente / Padrão |
|--------|---------------------|
| Loading global | `:loading` no botão ou `q-inner-loading` no card |
| Lista vazia | `AppEmptyState` |
| Erro de API | `useNotificacao().erro()` |
| Sucesso | `useNotificacao().sucesso()` |
| Confirmação de exclusão | `q-dialog` com botões Cancelar (flat) + Excluir (negative) |

---

## Checklist para nova página

1. Escolher layout (`MainLayout` ou `AuthLayout`)
2. Adicionar rota em `src/router/routes.ts`
3. Usar `AppPageHeader` (páginas internas)
4. Aplicar classe `page-content` no `q-page`
5. Consumir tokens — zero hex hardcoded
6. Reutilizar componentes App* existentes
7. Validar responsividade nos breakpoints sm e md
