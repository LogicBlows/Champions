import {
  createContext,
  useCallback,
  useContext,
  useMemo,
  useState,
  type ReactNode,
} from 'react';
import { apiRequest } from '../../api/client';
import type { LoginResponse } from '../../api/types';

interface AuthState {
  token: string | null;
  employeeId: string | null;
  name: string | null;
  isReviewer: boolean;
  isAuthenticated: boolean;
  login: (employeeCode: string) => Promise<void>;
  logout: () => void;
}

const STORAGE_KEY = 'championPortal.auth';

interface StoredAuth {
  token: string;
  employeeId: string;
  name: string;
  isReviewer: boolean;
}

function readStoredAuth(): StoredAuth | null {
  const raw = sessionStorage.getItem(STORAGE_KEY);
  if (!raw) return null;

  try {
    return JSON.parse(raw) as StoredAuth;
  } catch {
    return null;
  }
}

function writeStoredAuth(auth: StoredAuth | null) {
  if (auth) {
    sessionStorage.setItem(STORAGE_KEY, JSON.stringify(auth));
  } else {
    sessionStorage.removeItem(STORAGE_KEY);
  }
}

const AuthContext = createContext<AuthState | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const stored = readStoredAuth();
  const [token, setToken] = useState<string | null>(stored?.token ?? null);
  const [employeeId, setEmployeeId] = useState<string | null>(stored?.employeeId ?? null);
  const [name, setName] = useState<string | null>(stored?.name ?? null);
  const [isReviewer, setIsReviewer] = useState(stored?.isReviewer ?? false);

  const login = useCallback(async (employeeCode: string) => {
    const result = await apiRequest<LoginResponse>('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ employeeCode }),
    });

    writeStoredAuth({
      token: result.token,
      employeeId: result.employeeId,
      name: result.name,
      isReviewer: result.isReviewer,
    });

    setToken(result.token);
    setEmployeeId(result.employeeId);
    setName(result.name);
    setIsReviewer(result.isReviewer);
  }, []);

  const logout = useCallback(() => {
    writeStoredAuth(null);
    setToken(null);
    setEmployeeId(null);
    setName(null);
    setIsReviewer(false);
  }, []);

  const value = useMemo(
    () => ({
      token,
      employeeId,
      name,
      isReviewer,
      isAuthenticated: token !== null,
      login,
      logout,
    }),
    [token, employeeId, name, isReviewer, login, logout],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider');
  }
  return context;
}
