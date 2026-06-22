# 02 — Cores

## Paleta de marca

As cores principais do BGD Clinical são **verde** e **branco**. Toda a paleta (neutros, bordas, fundos, semânticas) deriva dessas duas cores.

### Primária — Verde clínico

Transmite saúde, crescimento e confiança. Usada em CTAs, links, ícones de destaque e navegação ativa.

| Token | Hex | Uso |
|-------|-----|-----|
| `--ds-color-primary-50` | `#ECFDF5` | Fundos sutis, hover leve |
| `--ds-color-primary-100` | `#D1FAE5` | Badges, tags |
| `--ds-color-primary-500` | `#10B981` | Verde vibrante — destaques |
| `--ds-color-primary-600` | `#059669` | **Cor principal** — botões, links |
| `--ds-color-primary-700` | `#047857` | Hover, links ativos |
| `--ds-color-primary-900` | `#064E3B` | Texto sobre fundo claro |

### Secundária — Branco

Base de superfícies, painéis e contraste com o verde.

| Token | Hex | Uso |
|-------|-----|-----|
| `--ds-color-secondary-500` | `#FFFFFF` | **Branco principal** — cards, modais |
| `--ds-color-secondary-100` | `#F6FBF8` | Fundo de página (branco esverdeado) |
| `--ds-color-secondary-200` | `#ECF5F0` | Seções alternadas |

### Accent — Verde escuro (derivado)

Destaques pontuais, headers alternativos, gráficos.

| Token | Hex |
|-------|-----|
| `--ds-color-accent-500` | `#047857` |
| `--ds-color-accent-600` | `#065F46` |

## Cores semânticas

Derivadas do verde onde possível; warning e error mantêm contraste funcional.

| Semântica | Token | Hex | Quasar |
|-----------|-------|-----|--------|
| Sucesso | `--ds-color-success-500` | `#059669` | `positive` |
| Aviso | `--ds-color-warning-500` | `#CA8A04` | `warning` |
| Erro | `--ds-color-error-500` | `#DC2626` | `negative` |
| Info | `--ds-color-info-500` | `#0D9488` | `info` |

### Uso em feedback

```vue
<!-- Notificações -->
notificacao.sucesso('Salvo com sucesso');
notificacao.erro('Falha ao salvar');

<!-- Badges -->
<q-badge color="positive" label="Ativo" />
<q-badge color="negative" label="Inativo" />
```

## Neutros (derivados do verde)

Escala de `--ds-color-neutral-0` (branco puro) a `--ds-color-neutral-900` (verde escuro quase preto). Todos os tons carregam subtom esverdeado.

| Token | Hex | Uso |
|-------|-----|-----|
| `--ds-color-neutral-0` | `#FFFFFF` | Superfícies |
| `--ds-color-neutral-50` | `#F6FBF8` | Fundo alternativo |
| `--ds-color-neutral-200` | `#D5E8DD` | Bordas |
| `--ds-color-neutral-500` | `#5C8572` | Texto secundário |
| `--ds-color-neutral-900` | `#142820` | Texto principal |

### Aliases de superfície

| Alias | Token base | Uso |
|-------|------------|-----|
| `--ds-bg-page` | secondary-100 | Fundo da aplicação |
| `--ds-bg-surface` | neutral-0 (branco) | Cards, modais, painéis |
| `--ds-bg-subtle` | primary-50 | Seções destacadas |
| `--ds-bg-muted` | neutral-200 | Separadores preenchidos |

### Aliases de texto

| Alias | Token base | Uso |
|-------|------------|-----|
| `--ds-text-primary` | neutral-900 | Títulos, corpo principal |
| `--ds-text-secondary` | neutral-500 | Subtítulos, captions |
| `--ds-text-muted` | neutral-400 | Placeholders, hints |
| `--ds-text-link` | primary-700 | Links clicáveis |

### Aliases de borda

| Alias | Uso |
|-------|-----|
| `--ds-border-default` | Cards, inputs, separadores |
| `--ds-border-strong` | Ênfase, divisores fortes |
| `--ds-border-focus` | Outline de foco (acessibilidade) |

## Mapeamento Quasar

Definido em `src/css/quasar.variables.scss`:

```scss
$primary:   #059669;
$secondary: #047857;
$accent:    #065f46;
$dark:      #142820;
$positive:  #059669;
$negative:  #dc2626;
$info:      #0d9488;
$warning:   #ca8a04;
```

## Contraste e acessibilidade

- Texto primário (`#142820`) sobre branco: ratio ≥ 12:1 ✓
- Botão primário (white on `#059669`): ratio ≥ 4.5:1 ✓
- Texto secundário sobre branco: ratio ≥ 4.5:1 ✓

## Whitelabel

Para customizar por tenant, sobrescrever apenas variáveis de marca:

```scss
.tenant-clinica-x {
  --ds-color-primary-600: #16a34a;
  --ds-color-primary-700: #15803d;
  --ds-brand-primary: var(--ds-color-primary-600);
}
```

Neutros e semânticas permanecem globais.

## TypeScript

```typescript
import { DesignSystemColors } from '@/constants/design-system';

const corGrafico = DesignSystemColors.primary[600];
```
