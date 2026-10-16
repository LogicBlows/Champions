import { useEffect, useState } from 'react';
import { NavLink, Navigate, Outlet, useLocation } from 'react-router-dom';
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
  const { isAdmin, isReviewer } = useAuth();
  if (!isAdmin && !isReviewer) {
    return <Navigate to="/home" replace />;
  }
  return <Outlet />;
}

export function AdminGuard() {
  const { isAdmin } = useAuth();
  if (!isAdmin) {
    return <Navigate to="/home" replace />;
  }
  return <Outlet />;
}

function sidebarLinkClass({ isActive }: { isActive: boolean }) {
  return isActive ? 'sidebar-link active' : 'sidebar-link';
}

export function AppLayout() {
  const { name, isReviewer, isAdmin } = useAuth();
  const canReview = isAdmin || isReviewer;
  const location = useLocation();
  const isActivityRoute = location.pathname.startsWith('/activities');
  const [activityOpen, setActivityOpen] = useState(isActivityRoute);

  useEffect(() => {
    if (isActivityRoute) {
      setActivityOpen(true);
    }
  }, [isActivityRoute]);

  return (
    <div className="app-shell">
      {/* Left column: brand + navigation */}
      <aside className="sidebar">
        <div className="sidebar-brand">
          <BrandMark homeLink />
        </div>

        <nav className="sidebar-nav">
          <NavLink to="/home" className={sidebarLinkClass} end>
            Home
          </NavLink>
          <NavLink to="/dashboard" className={sidebarLinkClass}>
            Dashboard
          </NavLink>
          <div className="sidebar-group">
            <button
              type="button"
              className={`sidebar-link sidebar-toggle${isActivityRoute ? ' active' : ''}`}
              onClick={() => setActivityOpen((open) => !open)}
              aria-expanded={activityOpen}
              aria-controls="sidebar-activity-subnav"
            >
              <span>Activity</span>
              <span className="sidebar-chevron" aria-hidden>
                {activityOpen ? '▾' : '▸'}
              </span>
            </button>
            {activityOpen && (
              <div id="sidebar-activity-subnav" className="sidebar-subnav">
                <NavLink to="/activities/my" className={sidebarLinkClass}>
                  My Activity
                </NavLink>
                <NavLink to="/activities/history" className={sidebarLinkClass}>
                  History
                </NavLink>
              </div>
            )}
          </div>
          {canReview && (
            <NavLink to="/review" className={sidebarLinkClass}>
              Review Queue
            </NavLink>
          )}
          {isAdmin && (
            <NavLink to="/admin" className={sidebarLinkClass}>
              Admin center
            </NavLink>
          )}
          <NavLink to="/settings" className={sidebarLinkClass}>
            Settings
          </NavLink>
        </nav>
      </aside>

      {/* Right column: top bar + page content */}
      <div className="app-content">
        <header className="top-bar">
          <div className="top-bar-actions">
            <span className="top-bar-name">{name}</span>
            {isReviewer && <span className="badge-reviewer">Reviewer</span>}
            {isAdmin && <span className="badge-reviewer">Admin</span>}
          </div>
        </header>

        <main className="app-main">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
