import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { AuthProvider } from './features/auth/AuthContext';
import LoginPage from './features/auth/LoginPage';
import HomePage from './features/home/HomePage';
import DashboardPage from './features/dashboard/DashboardPage';
import MyActivitiesPage from './features/activities/MyActivitiesPage';
import MyActivityHistoryPage from './features/activities/MyActivityHistoryPage';
import EmployeeActivityHistoryPage from './features/activities/EmployeeActivityHistoryPage';
import ReviewQueuePage from './features/review/ReviewQueuePage';
import { AppLayout, AuthGuard, ReviewerGuard } from './components/AppShell';
import SettingsPage from './features/settings/SettingsPage';
import { ThemeProvider } from './features/settings/ThemeContext';

const queryClient = new QueryClient();

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ThemeProvider>
        <AuthProvider>
          <BrowserRouter>
            <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route element={<AuthGuard />}>
              <Route element={<AppLayout />}>
                <Route path="/home" element={<HomePage />} />
                <Route path="/dashboard" element={<DashboardPage />} />
                <Route path="/" element={<Navigate to="/home" replace />} />
                <Route path="/activities/my" element={<MyActivitiesPage />} />
                <Route path="/activities/history" element={<MyActivityHistoryPage />} />
                <Route
                  path="/activities/new"
                  element={<Navigate to="/activities/my" replace />}
                />
                <Route
                  path="/employees/:employeeId/activities"
                  element={<EmployeeActivityHistoryPage />}
                />
                <Route path="/settings" element={<SettingsPage />} />
                <Route element={<ReviewerGuard />}>
                  <Route path="/review" element={<ReviewQueuePage />} />
                </Route>
              </Route>
            </Route>
            <Route path="*" element={<Navigate to="/home" replace />} />
            </Routes>
          </BrowserRouter>
        </AuthProvider>
      </ThemeProvider>
    </QueryClientProvider>
  );
}
