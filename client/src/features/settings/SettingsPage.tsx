import { type FormEvent, useState } from 'react';
import { PRESET_THEMES, type PresetThemeId, type ThemeTokens } from './themePresets';
import { useThemeSettings } from './ThemeContext';
import { useAuth } from '../auth/AuthContext';

const CUSTOM_FIELDS: { key: keyof ThemeTokens; label: string }[] = [
  { key: 'brand', label: 'Brand / accent' },
  { key: 'paper', label: 'Page background' },
  { key: 'surface', label: 'Cards & panels' },
  { key: 'brass', label: 'Highlight' },
];

function tokensFromPreset(id: PresetThemeId): ThemeTokens {
  return { ...PRESET_THEMES[id].tokens };
}

export default function SettingsPage() {
  const { name, logout } = useAuth();
  const {
    presetId,
    activeCustomId,
    customThemes,
    activeLabel,
    setPreset,
    applyCustomTheme,
    saveCustomTheme,
    deleteCustomTheme,
  } = useThemeSettings();

  const [customName, setCustomName] = useState('My theme');
  const [draftTokens, setDraftTokens] = useState<ThemeTokens>(() =>
    tokensFromPreset('default'),
  );

  function handlePresetSelect(id: PresetThemeId) {
    setPreset(id);
    setDraftTokens(tokensFromPreset(id));
  }

  function handleDraftColor(key: keyof ThemeTokens, value: string) {
    setDraftTokens((prev) => ({ ...prev, [key]: value }));
  }

  function handleSaveCustom(event: FormEvent) {
    event.preventDefault();
    const merged: ThemeTokens = {
      ...PRESET_THEMES.default.tokens,
      ...draftTokens,
    };
    saveCustomTheme(customName, merged);
  }

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>Settings</h2>
          <p className="muted">Appearance and account</p>
        </div>
      </div>

      <section className="card stack">
        <h3>Account</h3>
        <p className="muted">Signed in as <strong>{name}</strong></p>
        <p className="muted">Active theme: {activeLabel}</p>
        <button type="button" className="btn btn-ghost settings-signout" onClick={logout}>
          Sign out
        </button>
      </section>

      <section className="card stack">
        <h3>Color themes</h3>
        <p className="muted">Choose a preset for the portal. Changes apply immediately.</p>
        <div className="theme-preset-grid">
          {(Object.keys(PRESET_THEMES) as PresetThemeId[]).map((id) => {
            const { label, tokens } = PRESET_THEMES[id];
            const selected = !activeCustomId && presetId === id;
            return (
              <button
                key={id}
                type="button"
                className={`theme-preset-card${selected ? ' selected' : ''}`}
                onClick={() => handlePresetSelect(id)}
              >
                <span className="theme-swatch-row">
                  <span style={{ background: tokens.brand }} />
                  <span style={{ background: tokens.paper }} />
                  <span style={{ background: tokens.brass }} />
                </span>
                <span className="theme-preset-label">{label}</span>
              </button>
            );
          })}
        </div>
      </section>

      {customThemes.length > 0 && (
        <section className="card stack">
          <h3>Your custom themes</h3>
          <ul className="custom-theme-list">
            {customThemes.map((theme) => (
              <li key={theme.id} className="custom-theme-row">
                <button
                  type="button"
                  className={`theme-preset-card compact${activeCustomId === theme.id ? ' selected' : ''}`}
                  onClick={() => applyCustomTheme(theme.id)}
                >
                  <span className="theme-swatch-row">
                    <span style={{ background: theme.tokens.brand }} />
                    <span style={{ background: theme.tokens.paper }} />
                  </span>
                  {theme.name}
                </button>
                <button
                  type="button"
                  className="btn btn-ghost btn-sm"
                  onClick={() => deleteCustomTheme(theme.id)}
                >
                  Remove
                </button>
              </li>
            ))}
          </ul>
        </section>
      )}

      <section className="card stack">
        <h3>Create custom theme</h3>
        <p className="muted">Pick colors and save a theme to this browser.</p>
        <form onSubmit={handleSaveCustom} className="stack">
          <label className="field">
            <span>Theme name</span>
            <input
              value={customName}
              onChange={(e) => setCustomName(e.target.value)}
              required
            />
          </label>
          <div className="theme-color-grid">
            {CUSTOM_FIELDS.map(({ key, label }) => (
              <label key={key} className="field theme-color-field">
                <span>{label}</span>
                <div className="theme-color-input">
                  <input
                    type="color"
                    value={draftTokens[key]}
                    onChange={(e) => handleDraftColor(key, e.target.value)}
                  />
                  <input
                    type="text"
                    value={draftTokens[key]}
                    onChange={(e) => handleDraftColor(key, e.target.value)}
                    pattern="^#[0-9A-Fa-f]{6}$"
                  />
                </div>
              </label>
            ))}
          </div>
          <button type="submit" className="btn btn-primary">Save custom theme</button>
        </form>
      </section>
    </div>
  );
}
