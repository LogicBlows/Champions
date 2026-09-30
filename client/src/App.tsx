import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { AuthProvider } from './features/auth/AuthContext';
import LoginPage from './features/auth/LoginPage';
import DashboardPage from './features/dashboard/DashboardPage';
import MyActivitiesPage from './features/activities/MyActivitiesPage';
import EmployeeActivityHistoryPage from './features/activities/EmployeeActivityHistoryPage';
import ReviewQueuePage from './features/review/ReviewQueuePage';
import { AppLayout, AuthGuard, ReviewerGuard } from './components/AppShell';

const queryClient = new QueryClient();

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/login" element={<LoginPage />} />
            <Route element={<AuthGuard />}>
              <Route element={<AppLayout />}>
                <Route path="/dashboard" element={<DashboardPage />} />
                <Route path="/activities/new" element={<MyActivitiesPage />} />
                <Route
                  path="/employees/:employeeId/activities"
                  element={<EmployeeActivityHistoryPage />}
                />
                <Route element={<ReviewerGuard />}>
                  <Route path="/review" element={<ReviewQueuePage />} />
                </Route>
              </Route>
            </Route>
            <Route path="*" element={<Navigate to="/dashboard" replace />} />
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </QueryClientProvider>
  );
}
