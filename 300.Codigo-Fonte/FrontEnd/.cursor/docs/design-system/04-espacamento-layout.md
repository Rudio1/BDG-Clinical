# 04 — Espaçamento e Layout

## Escala de espaçamento

Grid base de **4px**. Todos os paddings, margins e gaps devem usar múltiplos desta escala.

| Token | Valor | px | Uso típico |
|-------|-------|----|------------|
| `--ds-space-1` | 0.25rem | 4px | Gap mínimo |
| `--ds-space-2` | 0.5rem | 8px | Gap interno compacto |
| `--ds-space-3` | 0.75rem | 12px | Gap entre campos de formulário |
| `--ds-space-4` | 1rem | 16px | Padding interno padrão |
| `--ds-space-5` | 1.25rem | 20px | Padding drawer |
| `--ds-space-6` | 1.5rem | 24px | Padding de página, auth panel mobile |
| `--ds-space-8` | 2rem | 32px | Padding auth panel desktop |
| `--ds-space-10` | 2.5rem | 40px | Seções grandes |
| `--ds-space-12` | 3rem | 48px | Separadores verticais |
| `--ds-space-16` | 4rem | 64px | Hero sections |

### Quasar equivalente

| DS | Quasar |
|----|--------|
| space-2 | `q-pa-xs`, `q-gutter-xs` |
| space-3 | `q-pa-sm`, `q-gutter-sm` |
| space-4 | `q-pa-md`, `q-gutter-md` |
| space-6 | `q-pa-lg`, `q-gutter-lg` |
| space-8 | `q-pa-xl` |

## Border radius

| Token | Valor | Uso |
|-------|-------|-----|
| `--ds-radius-sm` | 4px | Chips, badges |
| `--ds-radius-md` | 8px | **Padrão** — cards, inputs, painéis |
| `--ds-radius-lg` | 12px | Modais, popovers |
| `--ds-radius-xl` | 16px | Containers especiais |
| `--ds-radius-full` | 9999px | Avatares, pills |

Quasar: `$generic-border-radius: 8px` e `$button-border-radius: 8px`.

## Sombras

| Token | Uso |
|-------|-----|
| `--ds-shadow-xs` | Cards flat com leve elevação |
| `--ds-shadow-sm` | Dropdowns, menus |
| `--ds-shadow-md` | Modais |
| `--ds-shadow-lg` | Painel de auth (destaque) |
| `--ds-shadow-focus` | Anel de foco acessível |

## Layout da aplicação

### Dimensões fixas

| Token | Valor | Elemento |
|-------|-------|----------|
| `--ds-layout-header-height` | 56px | Toolbar do header |
| `--ds-layout-drawer-width` | 260px | Menu lateral |
| `--ds-layout-content-max` | 1280px | Largura máxima do conteúdo |
| `--ds-layout-auth-panel-max` | 420px | Painel login/cadastro |
| `--ds-layout-page-padding` | 24px | Padding externo de páginas |

### Breakpoints (Quasar)

| Nome | Min-width | Comportamento |
|------|-----------|---------------|
| xs | 0 | Mobile — drawer overlay, header stack |
| sm | 600px | Tablet — grid 2 colunas |
| md | 1024px | Desktop — drawer fixo, grid 3 colunas |
| lg | 1440px | Desktop largo |
| xl | 1920px | Telas ultra-wide |

### MainLayout

```
┌─────────────────────────────────────────────┐
│ Header (56px, bg-white, bordered)           │
├──────────┬──────────────────────────────────┤
│ Drawer   │ Page content (padding)           │
│ (260px)  │ max-width: 1280px                │
│          │                                  │
└──────────┴──────────────────────────────────┘
```

- Header: `bg-white text-dark`, botão menu + título + avatar
- Drawer: `show-if-above`, `bordered`, brand no topo
- Conteúdo: `<q-page padding class="page-content">`

### AuthLayout

```
┌─────────────────────────────────────────────┐
│                                             │
│         ┌─────────────────┐               │
│         │  AppAuthPanel   │               │
│         │  (max 420px)    │               │
│         └─────────────────┘               │
│                                             │
└─────────────────────────────────────────────┘
```

- Fundo: gradiente sutil primário + secundário sobre `--ds-bg-page`
- Painel centralizado vertical e horizontalmente

## Grid de conteúdo

Dashboard e listagens usam grid Quasar:

```html
<div class="row q-col-gutter-md">
  <div class="col-12 col-sm-6 col-md-4">...</div>
</div>
```

| Layout | Colunas |
|--------|---------|
| Mobile (xs) | 1 coluna (`col-12`) |
| Tablet (sm) | 2 colunas (`col-sm-6`) |
| Desktop (md+) | 3 colunas (`col-md-4`) |

## Z-index

| Token | Valor | Camada |
|-------|-------|--------|
| `--ds-z-dropdown` | 100 | Menus, selects |
| `--ds-z-sticky` | 200 | Header sticky |
| `--ds-z-drawer` | 300 | Drawer lateral |
| `--ds-z-modal` | 400 | Dialogs |
| `--ds-z-toast` | 500 | Notificações |

## Transições

| Token | Duração | Uso |
|-------|---------|-----|
| `--ds-transition-fast` | 120ms | Hover, toggle |
| `--ds-transition-base` | 200ms | Abertura de menu |
| `--ds-transition-slow` | 320ms | Drawer, modais |
