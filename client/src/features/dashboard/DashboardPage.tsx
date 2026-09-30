import {useEffect, useMemo, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { keepPreviousData, useQuery } from '@tanstack/react-query';
import { apiRequest } from '../../api/client';
import type { DashboardResponse } from '../../api/types';
import { useAuth } from '../auth/AuthContext';

type SortKey = 'rank' | 'name' | 'totalPoints' | 'activityCount' | 'lastActivity';

export default function DashboardPage() {
  const { token } = useAuth();
  const navigate = useNavigate();
  // What the user types (updates every keystroke)
  const [searchInput, setSearchInput] = useState('');
  // What actually hits the API (updates after debounce)
  const [debouncedSearch, setDebouncedSearch] = useState('');
  const [sortKey, setSortKey] = useState<SortKey>('totalPoints');
  const [sortAsc, setSortAsc] = useState(false);
  // Debounce: wait 400ms after user stops typing
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedSearch(searchInput.trim());
    }, 400);
    return () => clearTimeout(timer);
  }, [searchInput]);
  const { data, isLoading, isFetching, error } = useQuery({
    queryKey: ['dashboard', debouncedSearch],
    queryFn: () =>
      apiRequest<DashboardResponse>(
        `/dashboard${debouncedSearch ? `?search=${encodeURIComponent(debouncedSearch)}` : ''}`,
        { token },
      ),
    placeholderData: keepPreviousData,
  });


  const sortedLeaderboard = useMemo(() => {
    if (!data) return [];
    const rows = [...data.leaderboard];
    rows.sort((a, b) => {
      const left = a[sortKey];
      const right = b[sortKey];
      if (left === null || left === undefined) return 1;
      if (right === null || right === undefined) return -1;
      if (left < right) return sortAsc ? -1 : 1;
      if (left > right) return sortAsc ? 1 : -1;
      return 0;
    });
    return rows;
  }, [data, sortKey, sortAsc]);

  function toggleSort(key: SortKey) {
    if (sortKey === key) {
      setSortAsc((value) => !value);
    } else {
      setSortKey(key);
      setSortAsc(key === 'name');
    }
  }

if (isLoading && !data) return <p className="muted">Loading dashboard…</p>;
  if (error) return <p className="error">Failed to load dashboard.</p>;
  if (!data) return null;

  const { summary } = data;

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>Dashboard</h2>
          <p className="muted">Team activity and leaderboard</p>
        </div>
        <Link to="/activities/my" className="btn btn-primary">
          Log an Activity
        </Link>
      </div>

      <section className="summary-strip">
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
          <strong>{summary.topCategory}</strong>
        </article>
      </section>

      <section className="card stack">
        <div className="toolbar">
          <label className="field grow">
  <span>Search</span>
  <input
    value={searchInput}
    onChange={(e) => setSearchInput(e.target.value)}
    placeholder="Name, title, or category"
  />
  {isFetching && data && (
    <span className="muted" style={{ fontSize: '0.85rem' }}>Updating…</span>
  )}
</label>
        </div>

        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>
                  <button type="button" className="sort-btn" onClick={() => toggleSort('rank')}>
                    Rank
                  </button>
                </th>
                <th>
                  <button type="button" className="sort-btn" onClick={() => toggleSort('name')}>
                    Name
                  </button>
                </th>
                <th>
                  <button
                    type="button"
                    className="sort-btn"
                    onClick={() => toggleSort('totalPoints')}
                  >
                    Total Points
                  </button>
                </th>
                <th>
                  <button
                    type="button"
                    className="sort-btn"
                    onClick={() => toggleSort('activityCount')}
                  >
                    Activities
                  </button>
                </th>
                <th>
                  <button
                    type="button"
                    className="sort-btn"
                    onClick={() => toggleSort('lastActivity')}
                  >
                    Last Activity
                  </button>
                </th>
              </tr>
            </thead>
            <tbody>
              {sortedLeaderboard.map((row) => (
                <tr
                  key={`${row.rank}-${row.employeeId}`}
                  className="clickable-row"
                  onClick={() => navigate(`/employees/${row.employeeId}/activities`)}
                >
                  <td>{row.rank}</td>
                  <td>{row.name}</td>
                  <td>{row.totalPoints}</td>
                  <td>{row.activityCount}</td>
                  <td>
                    {row.lastActivity
                      ? new Date(row.lastActivity).toLocaleDateString()
                      : '—'}
                  </td>
                </tr>
              ))}
              {sortedLeaderboard.length === 0 && (
                <tr>
                  <td colSpan={5} className="muted">
                    No activity yet.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
}
