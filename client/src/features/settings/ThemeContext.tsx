import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
  type ReactNode,
} from 'react';
import {
  applyThemeTokens,
  clearInlineThemeTokens,
  PRESET_THEMES,
  type CustomTheme,
  type PresetThemeId,
  type ThemeTokens,
} from './themePresets';

const STORAGE_KEY = 'champion-portal-theme';

type StoredThemeState = {
  presetId: PresetThemeId;
  activeCustomId: string | null;
  customThemes: CustomTheme[];
};

type ThemeContextValue = {
  presetId: PresetThemeId;
  activeCustomId: string | null;
  customThemes: CustomTheme[];
  activeLabel: string;
  setPreset: (id: PresetThemeId) => void;
  applyCustomTheme: (id: string) => void;
  saveCustomTheme: (name: string, tokens: ThemeTokens) => void;
  deleteCustomTheme: (id: string) => void;
};

const ThemeContext = createContext<ThemeContextValue | null>(null);

function loadState(): StoredThemeState {
  try {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (!raw) {
      return { presetId: 'default', activeCustomId: null, customThemes: [] };
    }
    const parsed = JSON.parse(raw) as StoredThemeState;
    if (!PRESET_THEMES[parsed.presetId]) {
      parsed.presetId = 'default';
    }
    parsed.customThemes = parsed.customThemes ?? [];
    return parsed;
  } catch {
    return { presetId: 'default', activeCustomId: null, customThemes: [] };
  }
}

function persistState(state: StoredThemeState) {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(state));
}

function resolveTokens(state: StoredThemeState): { tokens: ThemeTokens; label: string } {
  if (state.activeCustomId) {
    const custom = state.customThemes.find((t) => t.id === state.activeCustomId);
    if (custom) {
      return { tokens: custom.tokens, label: custom.name };
    }
  }
  const preset = PRESET_THEMES[state.presetId];
  return { tokens: preset.tokens, label: preset.label };
}

export function ThemeProvider({ children }: { children: ReactNode }) {
  const [state, setState] = useState<StoredThemeState>(loadState);

  const applyFromState = useCallback((next: StoredThemeState) => {
    const { tokens } = resolveTokens(next);
    clearInlineThemeTokens();
    applyThemeTokens(tokens);
    document.documentElement.dataset.theme = next.activeCustomId ? 'custom' : next.presetId;
  }, []);

  useEffect(() => {
    applyFromState(state);
  }, [state, applyFromState]);

  const setPreset = useCallback((id: PresetThemeId) => {
    setState((prev) => {
      const next = { ...prev, presetId: id, activeCustomId: null };
      persistState(next);
      return next;
    });
  }, []);

  const applyCustomTheme = useCallback((id: string) => {
    setState((prev) => {
      const next = { ...prev, activeCustomId: id };
      persistState(next);
      return next;
    });
  }, []);

  const saveCustomTheme = useCallback((name: string, tokens: ThemeTokens) => {
    setState((prev) => {
      const id = crypto.randomUUID();
      const custom: CustomTheme = { id, name: name.trim() || 'Custom theme', tokens };
      const next = {
        ...prev,
        customThemes: [...prev.customThemes, custom],
        activeCustomId: id,
      };
      persistState(next);
      return next;
    });
  }, []);

  const deleteCustomTheme = useCallback((id: string) => {
    setState((prev) => {
      const customThemes = prev.customThemes.filter((t) => t.id !== id);
      const activeCustomId = prev.activeCustomId === id ? null : prev.activeCustomId;
      const next = { ...prev, customThemes, activeCustomId };
      persistState(next);
      return next;
    });
  }, []);

  const activeLabel = useMemo(() => resolveTokens(state).label, [state]);

  const value = useMemo(
    () => ({
      presetId: state.presetId,
      activeCustomId: state.activeCustomId,
      customThemes: state.customThemes,
      activeLabel,
      setPreset,
      applyCustomTheme,
      saveCustomTheme,
      deleteCustomTheme,
    }),
    [
      state,
      activeLabel,
      setPreset,
      applyCustomTheme,
      saveCustomTheme,
      deleteCustomTheme,
    ],
  );

  return <ThemeContext.Provider value={value}>{children}</ThemeContext.Provider>;
}

export function useThemeSettings() {
  const ctx = useContext(ThemeContext);
  if (!ctx) {
    throw new Error('useThemeSettings must be used within ThemeProvider');
  }
  return ctx;
}
