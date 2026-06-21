# Changelog — Design System Front-end BGD Clinical

## 2025-06-21 (c)

- Redesign premium visual-first: auth split-screen (55% visual), dashboard hero, micro-animações.
- Novo componente `AppAuthShell` substitui `AppAuthPanel` para login/cadastro.
- Padrão documentado: abordagem SaaS moderno (Stripe/Airbnb), não ERP.

## 2025-06-21 (b)

- Paleta alterada para **verde + branco** como cores principais da marca.
- Verde primário `#059669`, superfícies brancas, neutros e semânticas derivados do verde.
- Fonte oficial alterada de Inter para **Plus Jakarta Sans**.

## 2025-06-21

- Design system inicial criado com tokens CSS (`--ds-*`), variáveis Quasar e constants TypeScript.
- Paleta clínica: azul primário `#0B7BC2`, teal secundário `#0D9488`, neutros Slate.
- Tipografia Inter com escala tipográfica padronizada.
- Componentes base: `AppBrand`, `AppAuthPanel`, `AppMetricCard`.
- Refatoração de login, cadastro, layout principal e dashboard para usar tokens.
- Documentação completa em `.cursor/docs/design-system/`.
