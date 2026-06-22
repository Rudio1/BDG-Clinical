export const DesignSystemColors = {
  primary: {
    50: '#ecfdf5',
    100: '#d1fae5',
    200: '#a7f3d0',
    300: '#6ee7b7',
    400: '#34d399',
    500: '#10b981',
    600: '#059669',
    700: '#047857',
    800: '#065f46',
    900: '#064e3b',
  },
  secondary: {
    50: '#ffffff',
    100: '#f6fbf8',
    200: '#ecf5f0',
    300: '#d5e8dd',
    400: '#b8d4c4',
    500: '#ffffff',
    600: '#ecf5f0',
    700: '#d5e8dd',
    800: '#b8d4c4',
    900: '#7fa892',
  },
  accent: {
    500: '#047857',
    600: '#065f46',
  },
  semantic: {
    success: '#059669',
    warning: '#ca8a04',
    error: '#dc2626',
    info: '#0d9488',
  },
  neutral: {
    0: '#ffffff',
    50: '#f6fbf8',
    100: '#ecf5f0',
    200: '#d5e8dd',
    300: '#b8d4c4',
    400: '#7fa892',
    500: '#5c8572',
    600: '#456658',
    700: '#334d42',
    800: '#1f3329',
    900: '#142820',
  },
} as const;

export const DesignSystemTypography = {
  fontFamily: "'Plus Jakarta Sans', 'Segoe UI', system-ui, -apple-system, sans-serif",
  fontFamilyMono: "'JetBrains Mono', 'Cascadia Code', 'Consolas', monospace",
  fontSize: {
    xs: '0.75rem',
    sm: '0.875rem',
    base: '1rem',
    lg: '1.125rem',
    xl: '1.25rem',
    '2xl': '1.5rem',
    '3xl': '1.75rem',
    '4xl': '2rem',
  },
  fontWeight: {
    regular: 400,
    medium: 500,
    semibold: 600,
    bold: 700,
  },
} as const;

export const DesignSystemSpacing = {
  0: '0',
  1: '0.25rem',
  2: '0.5rem',
  3: '0.75rem',
  4: '1rem',
  5: '1.25rem',
  6: '1.5rem',
  8: '2rem',
  10: '2.5rem',
  12: '3rem',
  16: '4rem',
} as const;

export const DesignSystemLayout = {
  headerHeight: '56px',
  drawerWidth: 260,
  contentMaxWidth: 1280,
  authPanelMaxWidth: 420,
  breakpoints: {
    xs: 0,
    sm: 600,
    md: 1024,
    lg: 1440,
    xl: 1920,
  },
} as const;

export const DesignSystemRadius = {
  sm: '4px',
  md: '8px',
  lg: '12px',
  xl: '16px',
  full: '9999px',
} as const;

export const DesignSystemShadow = {
  xs: '0 1px 2px rgba(20, 40, 32, 0.05)',
  sm: '0 2px 8px rgba(20, 40, 32, 0.06)',
  md: '0 8px 24px rgba(20, 40, 32, 0.08)',
  lg: '0 18px 48px rgba(20, 40, 32, 0.1)',
} as const;

export const DesignSystemBrand = {
  nome: 'BGD Clinical',
  icone: 'health_and_safety',
  taglinePadrao: 'Gestão clínica inteligente',
} as const;

export const DesignSystemAuth = {
  heroImage:
    'https://images.unsplash.com/photo-1579684385127-1ef15a5089a2?auto=format&fit=crop&w=1400&q=85',
  login: {
    headline: 'Cuidado que inspira confiança.',
    subline: 'A forma mais simples de gerenciar sua clínica com excelência.',
    formTitle: 'Bem-vindo de volta',
    formSubtitle: 'Entre para continuar de onde parou.',
  },
  register: {
    headline: 'Sua clínica, elevada.',
    subline: 'Configure tudo em minutos e foque no que realmente importa.',
    formTitle: 'Crie sua conta',
    formSubtitle: 'Sem cartão. Sem burocracia. Comece agora.',
  },
  stats: [
    { value: '2.4k+', label: 'Clínicas ativas' },
    { value: '98%', label: 'Satisfação' },
    { value: '24/7', label: 'Suporte' },
  ],
} as const;
