import { NavLink, Navigate, Outlet } from 'react-router-dom';
import BrandMark from './BrandMark';
import { useAuth } from '../features/auth/AuthContext';

export function AuthGuard() {
  const { isAuthenticated } = useAuth();
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }
  return <Outlet />;
}

export function ReviewerGuard() {
  const { isReviewer } = useAuth();
  if (!isReviewer) {
    return <Navigate to="/dashboard" replace />;
  }
  return <Outlet />;
}

function sidebarLinkClass({ isActive }: { isActive: boolean }) {
  return isActive ? 'sidebar-link active' : 'sidebar-link';
}

export function AppLayout() {
  const { name, isReviewer, logout } = useAuth();

  return (
    <div className="app-shell">
      {/* Left column: brand + navigation */}
      <aside className="sidebar">
        <p className="sidebar-tagline">GenAI Activity Recognition</p>

        <nav className="sidebar-nav">
          <NavLink to="/dashboard" className={sidebarLinkClass} end>
            Dashboard
          </NavLink>
          <NavLink to="/activities/new" className={sidebarLinkClass}>
            My Activities
          </NavLink>
          {isReviewer && (
            <NavLink to="/review" className={sidebarLinkClass}>
              Review Queue
            </NavLink>
          )}
        </nav>
      </aside>

      {/* Right column: top bar + page content */}
      <div className="app-content">
        <header className="top-bar">
          <BrandMark />
          <div className="top-bar-actions">
            <span className="muted">Welcome {name}</span>
            {isReviewer && <span className="badge-reviewer">Reviewer</span>}
            <button type="button" className="btn btn-ghost" onClick={logout}>
              Sign out
            </button>
          </div>
        </header>

        <main className="app-main">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
