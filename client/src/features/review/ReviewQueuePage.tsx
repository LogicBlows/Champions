import { useState } from 'react';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { apiRequest } from '../../api/client';
import type { PendingActivityType } from '../../api/types';
import { useAuth } from '../auth/AuthContext';

export default function ReviewQueuePage() {
  const { token } = useAuth();
  const queryClient = useQueryClient();
  const [activeId, setActiveId] = useState<string | null>(null);
  const [points, setPoints] = useState('');

  const pendingQuery = useQuery({
    queryKey: ['pending-activity-types'],
    queryFn: () =>
      apiRequest<PendingActivityType[]>('/activity-types/pending', { token }),
  });

  const reviewMutation = useMutation({
    mutationFn: ({
      id,
      approved,
      pointsValue,
    }: {
      id: string;
      approved: boolean;
      pointsValue?: number;
    }) =>
      apiRequest<void>(`/activity-types/${id}/review`, {
        method: 'POST',
        token,
        body: JSON.stringify({
          approved,
          points: pointsValue ?? null,
        }),
      }),
    onSuccess: async () => {
      setActiveId(null);
      setPoints('');
      await queryClient.invalidateQueries({ queryKey: ['pending-activity-types'] });
      await queryClient.invalidateQueries({ queryKey: ['activity-types'] });
      await queryClient.invalidateQueries({ queryKey: ['my-activities'] });
      await queryClient.invalidateQueries({ queryKey: ['dashboard'] });
    },
  });

  async function handleApprove(id: string) {
    const parsed = Number(points);
    if (!parsed || parsed <= 0) {
      return;
    }
    await reviewMutation.mutateAsync({ id, approved: true, pointsValue: parsed });
  }

  async function handleReject(id: string) {
    await reviewMutation.mutateAsync({ id, approved: false });
  }

  if (pendingQuery.isLoading) return <p className="muted">Loading review queue…</p>;
  if (pendingQuery.error) return <p className="error">Failed to load review queue.</p>;

  const pending = pendingQuery.data ?? [];

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>Review Queue</h2>
          <p className="muted">Approve or reject proposed activity categories</p>
        </div>
      </div>

      <section className="stack">
        {pending.map((item) => (
          <article key={item.id} className="card stack">
            <div className="review-row">
              <div>
                <h3>{item.name}</h3>
                <p className="muted">{item.description}</p>
                <p className="meta">
                  Proposed by {item.proposedByName ?? 'Unknown'}
                  {item.suggestedPoints ? ` · Suggested ${item.suggestedPoints} pts` : ''}
                </p>
              </div>
              <div className="review-actions">
                <button
                  type="button"
                  className="btn btn-ghost"
                  onClick={() =>
                    setActiveId((current) => (current === item.id ? null : item.id))
                  }
                >
                  Approve
                </button>
                <button
                  type="button"
                  className="btn btn-danger"
                  onClick={() => handleReject(item.id)}
                  disabled={reviewMutation.isPending}
                >
                  Reject
                </button>
              </div>
            </div>

            {activeId === item.id && (
              <div className="approve-panel stack">
                <label className="field">
                  <span>Approved points</span>
                  <input
                    type="number"
                    min="1"
                    value={points}
                    onChange={(e) => setPoints(e.target.value)}
                    placeholder={item.suggestedPoints?.toString() ?? '40'}
                  />
                </label>
                <button
                  type="button"
                  className="btn btn-primary"
                  onClick={() => handleApprove(item.id)}
                  disabled={reviewMutation.isPending}
                >
                  Confirm approval
                </button>
              </div>
            )}
          </article>
        ))}

        {pending.length === 0 && (
          <section className="card">
            <p className="muted">No categories waiting for review.</p>
          </section>
        )}
      </section>
    </div>
  );
}
