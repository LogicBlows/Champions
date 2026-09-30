export type ThemeTokens = {
  paper: string;
  surface: string;
  ink: string;
  inkSoft: string;
  line: string;
  brand: string;
  brandHover: string;
  brandSoft: string;
  brass: string;
  brassSoft: string;
};

export type PresetThemeId = 'default' | 'midnight' | 'forest' | 'ocean';

export type CustomTheme = {
  id: string;
  name: string;
  tokens: ThemeTokens;
};

export const PRESET_THEMES: Record<PresetThemeId, { label: string; tokens: ThemeTokens }> = {
  default: {
    label: 'Champion (default)',
    tokens: {
      paper: '#F7F5EF',
      surface: '#FFFFFF',
      ink: '#161B2E',
      inkSoft: '#5B6070',
      line: '#E4E0D6',
      brand: '#3D3FC9',
      brandHover: '#3234a8',
      brandSoft: '#ECEBFB',
      brass: '#9C7A3C',
      brassSoft: '#F2EBDA',
    },
  },
  midnight: {
    label: 'Midnight',
    tokens: {
      paper: '#0F1219',
      surface: '#181C27',
      ink: '#E8EAEF',
      inkSoft: '#9AA3B5',
      line: '#2A3142',
      brand: '#7B8CFF',
      brandHover: '#9AA8FF',
      brandSoft: '#252B45',
      brass: '#C9A962',
      brassSoft: '#2A2618',
    },
  },
  forest: {
    label: 'Forest',
    tokens: {
      paper: '#F2F6F3',
      surface: '#FFFFFF',
      ink: '#14231A',
      inkSoft: '#4D5E54',
      line: '#D8E4DC',
      brand: '#1F7A4C',
      brandHover: '#18633D',
      brandSoft: '#E3F2EA',
      brass: '#8B6914',
      brassSoft: '#F0E8D4',
    },
  },
  ocean: {
    label: 'Ocean',
    tokens: {
      paper: '#F0F6FA',
      surface: '#FFFFFF',
      ink: '#0F2433',
      inkSoft: '#4A6275',
      line: '#D4E4EE',
      brand: '#0B7EA5',
      brandHover: '#096688',
      brandSoft: '#E0F2F8',
      brass: '#B8860B',
      brassSoft: '#F5ECD6',
    },
  },
};

export const TOKEN_CSS_VARS: Record<keyof ThemeTokens, string> = {
  paper: '--paper',
  surface: '--surface',
  ink: '--ink',
  inkSoft: '--ink-soft',
  line: '--line',
  brand: '--brand',
  brandHover: '--brand-hover',
  brandSoft: '--brand-soft',
  brass: '--brass',
  brassSoft: '--brass-soft',
};

export function applyThemeTokens(tokens: ThemeTokens) {
  const root = document.documentElement;
  for (const [key, cssVar] of Object.entries(TOKEN_CSS_VARS)) {
    root.style.setProperty(cssVar, tokens[key as keyof ThemeTokens]);
  }
}

export function clearInlineThemeTokens() {
  const root = document.documentElement;
  for (const cssVar of Object.values(TOKEN_CSS_VARS)) {
    root.style.removeProperty(cssVar);
  }
}
