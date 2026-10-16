import { Link } from 'react-router-dom';
import { useQuery } from '@tanstack/react-query';
import { apiRequest } from '../../api/client';
import type {
  DashboardResponse,
  MyActivitiesResponse,
  PendingActivityType,
} from '../../api/types';
import { useAuth } from '../auth/AuthContext';

function formatDate(iso: string) {
  return new Date(iso).toLocaleDateString(undefined, {
    month: 'short',
    day: 'numeric',
    year: 'numeric',
  });
}

export default function HomePage() {
  const { token, name, employeeId, isReviewer, isAdmin } = useAuth();
  const canReview = isAdmin || isReviewer;

  const myQuery = useQuery({
    queryKey: ['my-activities'],
    queryFn: () => apiRequest<MyActivitiesResponse>('/me/activities', { token }),
  });

  const dashboardQuery = useQuery({
    queryKey: ['dashboard', ''],
    queryFn: () => apiRequest<DashboardResponse>('/dashboard', { token }),
  });

  const pendingQuery = useQuery({
    queryKey: ['pending-activity-types'],
    queryFn: () =>
      apiRequest<PendingActivityType[]>('/activity-types/pending', { token }),
    enabled: canReview,
  });

  const loading = myQuery.isLoading || dashboardQuery.isLoading;
  if (loading) {
    return <p className="muted">Loading your home…</p>;
  }
  if (myQuery.error || dashboardQuery.error) {
    return <p className="error">Failed to load home page.</p>;
  }

  const activities = myQuery.data?.activities ?? [];
  const totalPoints = myQuery.data?.totalPoints ?? 0;
  const activityCount = activities.length;
  const latestActivity = [...activities].sort(
    (a, b) => new Date(b.submittedOn).getTime() - new Date(a.submittedOn).getTime(),
  )[0];

  const leaderboard = dashboardQuery.data?.leaderboard ?? [];
  const myRank = employeeId
    ? leaderboard.find((row) => row.employeeId === employeeId)?.rank
    : undefined;
  const summary = dashboardQuery.data?.summary;
  const pendingCount = pendingQuery.data?.length ?? 0;

  return (
    <div className="stack-lg home-page">
      <header className="home-hero card">
        <div>
          <p className="home-eyebrow">Champion Portal</p>
          <h2>Welcome, {name}</h2>
          <p className="muted home-lead">
            Log GenAI adoption work, earn recognition points, and see how the competency
            group is progressing.
          </p>
        </div>
        <Link to="/activities/my" className="btn btn-primary">
          Log an activity
        </Link>
      </header>

      <section>
        <h3 className="home-section-title">Your snapshot</h3>
        <div className="summary-strip">
          <article className="stat-card">
            <span className="stat-label">Your points</span>
            <strong>{totalPoints}</strong>
          </article>
          <article className="stat-card">
            <span className="stat-label">Activities logged</span>
            <strong>{activityCount}</strong>
          </article>
          <article className="stat-card">
            <span className="stat-label">Leaderboard rank</span>
            <strong>{myRank != null ? `#${myRank}` : '—'}</strong>
          </article>
          <article className="stat-card">
            <span className="stat-label">Latest activity</span>
            <strong className="home-stat-text">
              {latestActivity ? latestActivity.title : 'None yet'}
            </strong>
            {latestActivity && (
              <span className="muted home-stat-meta">
                {formatDate(latestActivity.submittedOn)}
              </span>
            )}
          </article>
        </div>
      </section>

      <section>
        <h3 className="home-section-title">Quick actions</h3>
        <div className="home-action-grid">
          <Link to="/activities/my" className="home-action-card">
            <span className="home-action-title">Log activity</span>
            <span className="muted">Submit work under an approved category</span>
          </Link>
          <Link to="/activities/history" className="home-action-card">
            <span className="home-action-title">View history</span>
            <span className="muted">Track submissions, points, and endorsements</span>
          </Link>
          <Link to="/dashboard" className="home-action-card">
            <span className="home-action-title">Team dashboard</span>
            <span className="muted">Search the leaderboard and team totals</span>
          </Link>
          {canReview && (
            <Link to="/review" className="home-action-card home-action-card--review">
              <span className="home-action-title">
                Review queue
                {pendingCount > 0 && (
                  <span className="home-badge">{pendingCount}</span>
                )}
              </span>
              <span className="muted">Approve or reject proposed activity categories</span>
            </Link>
          )}
        </div>
      </section>

      {summary && (
        <section>
          <h3 className="home-section-title">Team pulse</h3>
          <div className="summary-strip">
            <article className="stat-card">
              <span className="stat-label">Total activities</span>
              <strong>{summary.totalActivities}</strong>
            </article>
            <article className="stat-card">
              <span className="stat-label">Total points</span>
              <strong>{summary.totalPoints}</strong>
            </article>
            <article className="stat-card">
              <span className="stat-label">Participating</span>
              <strong>
                {summary.participatingEmployees} of {summary.totalActiveEmployees}
              </strong>
            </article>
            <article className="stat-card">
              <span className="stat-label">Top category</span>
              <strong className="home-stat-text">{summary.topCategory}</strong>
            </article>
          </div>
        </section>
      )}

      <section className="card stack home-how">
        <h3>How recognition works</h3>
        <ol className="home-steps">
          <li>Choose an approved category and log what you did with GenAI.</li>
          <li>Peers endorse your activity when they can vouch for the work.</li>
          <li>Points count toward your total and the team leaderboard on the dashboard.</li>
        </ol>
      </section>
    </div>
  );
}
