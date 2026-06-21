# 03 — Tipografia

## Família tipográfica

| Token | Valor | Uso |
|-------|-------|-----|
| `--ds-font-family` | Plus Jakarta Sans, Segoe UI, system-ui | Toda a UI |
| `--ds-font-family-mono` | JetBrains Mono, Consolas | Código, IDs técnicos |

**Plus Jakarta Sans** é a fonte oficial do design system — moderna, legível e adequada para produtos de saúde. Carregada via Google Fonts em `index.html` com pesos 400, 500, 600 e 700.

## Escala tipográfica

Base: **16px** (`1rem`).

| Token | Tamanho | Line height | Uso |
|-------|---------|-------------|-----|
| `--ds-font-size-xs` | 12px | 1.5 | Captions, metadados do drawer |
| `--ds-font-size-sm` | 14px | 1.5 | Labels de métricas, texto auxiliar |
| `--ds-font-size-base` | 16px | 1.5 | Corpo padrão |
| `--ds-font-size-lg` | 18px | 1.625 | Destaques de corpo |
| `--ds-font-size-xl` | 20px | 1.4 | Títulos de empty state |
| `--ds-font-size-2xl` | 24px | 1.25 | Título de marca (auth) |
| `--ds-font-size-3xl` | 28px | 1.25 | Título de página (h1) |
| `--ds-font-size-4xl` | 32px | 1.25 | Páginas de erro |

## Pesos

| Token | Valor | Uso |
|-------|-------|-----|
| `--ds-font-weight-regular` | 400 | Corpo, parágrafos |
| `--ds-font-weight-medium` | 500 | Labels enfatizados |
| `--ds-font-weight-semibold` | 600 | Subtítulos, nav items |
| `--ds-font-weight-bold` | 700 | Títulos, valores de métricas |

## Hierarquia por contexto

### Páginas autenticadas

```
h1 (page-header)     → 28px / bold     → Título da página
p (subtítulo)        → 16px / regular  → Descrição (text-secondary)
metric-value         → 24px / bold     → Números de indicadores
metric-label         → 14px / regular  → Rótulo do indicador
```

### Páginas de auth

```
h1 (brand)           → 24px / bold     → "BGD Clinical"
p (brand subtítulo)  → 16px / regular  → "Ambiente administrativo"
```

### Empty state

```
h2                   → 20px / bold
p                    → 16px / regular / text-secondary
```

## Classes utilitárias

```html
<p class="ds-text-sm ds-text-secondary">Texto auxiliar</p>
<a class="ds-text-link" href="#">Link</a>
<span class="ds-text-muted">Placeholder hint</span>
```

## Quasar — mapeamento

Preferir classes DS para texto customizado. Para componentes Quasar nativos:

| Quasar | Equivalente DS |
|--------|----------------|
| `text-h4` | ~ `--ds-font-size-2xl` |
| `text-body1` | `--ds-font-size-base` |
| `text-body2` | `--ds-font-size-sm` |
| `text-caption` | `--ds-font-size-xs` |

## Regras

1. **Um h1 por página** — sempre via `AppPageHeader` ou `AppBrand`.
2. **Não usar font-size inline** — tokens ou classes utilitárias.
3. **Letter-spacing** — títulos usam `--ds-letter-spacing-tight` (-0.01em).
4. **Caixa** — botões com `no-caps` (sentence case, não ALL CAPS).
