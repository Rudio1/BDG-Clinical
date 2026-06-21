# Front-end BGD Clinical — Guia para IA

Documentação de arquitetura visual, design system e convenções do front-end. Leia antes de implementar telas ou componentes.

## Arquivos

| Arquivo | Conteúdo |
|---------|----------|
| [CHANGELOG.md](./CHANGELOG.md) | Histórico de mudanças no design system e regras |
| [rules/00-design-system.mdc](./rules/00-design-system.mdc) | Regra obrigatória: uso de tokens e componentes |
| [docs/design-system/README.md](./docs/design-system/README.md) | Índice do design system |
| [docs/design-system/01-fundamentos.md](./docs/design-system/01-fundamentos.md) | Princípios, stack e estrutura de pastas |
| [docs/design-system/02-cores.md](./docs/design-system/02-cores.md) | Paleta, semânticas e whitelabel |
| [docs/design-system/03-tipografia.md](./docs/design-system/03-tipografia.md) | Fontes, escala e hierarquia |
| [docs/design-system/04-espacamento-layout.md](./docs/design-system/04-espacamento-layout.md) | Grid, breakpoints e layout |
| [docs/design-system/05-componentes.md](./docs/design-system/05-componentes.md) | Quasar, componentes App* e padrões |
| [docs/design-system/06-padroes-pagina.md](./docs/design-system/06-padroes-pagina.md) | Auth, dashboard, listagens e formulários |

## Código-fonte

| Caminho | Responsabilidade |
|---------|------------------|
| `src/css/design-system/` | Tokens CSS (`--ds-*`), tipografia e padrões visuais |
| `src/css/quasar.variables.scss` | Variáveis SASS do Quasar (espelham tokens) |
| `src/constants/design-system/` | Tokens TypeScript para uso programático |
| `src/components/design-system/` | Componentes visuais reutilizáveis |
| `src/components/shared/` | Componentes de composição de página |

## Ao alterar o design system

1. Atualizar tokens em `src/css/design-system/_tokens.scss`
2. Espelhar mudanças em `quasar.variables.scss` e `src/constants/design-system/`
3. Documentar em `docs/design-system/` e registrar em `CHANGELOG.md`
4. Nunca usar cores ou espaçamentos hardcoded em componentes novos
