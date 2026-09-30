import { type FormEvent, useState } from 'react';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { apiRequest } from '../../api/client';
import type { ActivityType, MyActivity } from '../../api/types';
import { useAuth } from '../auth/AuthContext';

export default function MyActivitiesPage() {
  const { token } = useAuth();
  const queryClient = useQueryClient();
  const [mode, setMode] = useState<'submit' | 'propose'>('submit');
  const [activityTypeId, setActivityTypeId] = useState('');
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [proposedName, setProposedName] = useState('');
  const [proposedDescription, setProposedDescription] = useState('');
  const [suggestedPoints, setSuggestedPoints] = useState('');
  const [formError, setFormError] = useState<string | null>(null);

  const typesQuery = useQuery({
    queryKey: ['activity-types'],
    queryFn: () =>
      apiRequest<ActivityType[]>('/activity-types', { token }),
  });

  const submitMutation = useMutation({
    mutationFn: () =>
      apiRequest<MyActivity>('/activities', {
        method: 'POST',
        token,
        body: JSON.stringify({
          activityTypeId,
          title,
          description: description || null,
        }),
      }),
    onSuccess: async () => {
      setTitle('');
      setDescription('');
      setFormError(null);
      await queryClient.invalidateQueries({ queryKey: ['my-activities'] });
      await queryClient.invalidateQueries({ queryKey: ['dashboard'] });
    },
    onError: (error: Error) => setFormError(error.message),
  });

  const proposeMutation = useMutation({
    mutationFn: () =>
      apiRequest<ActivityType>('/activity-types/propose', {
        method: 'POST',
        token,
        body: JSON.stringify({
          name: proposedName,
          description: proposedDescription,
          suggestedPoints: suggestedPoints ? Number(suggestedPoints) : null,
        }),
      }),
    onSuccess: async () => {
      setProposedName('');
      setProposedDescription('');
      setSuggestedPoints('');
      setFormError(null);
      await queryClient.invalidateQueries({ queryKey: ['activity-types'] });
    },
    onError: (error: Error) => setFormError(error.message),
  });

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();
    setFormError(null);

    if (mode === 'submit') {
      await submitMutation.mutateAsync();
    } else {
      await proposeMutation.mutateAsync();
    }
  }

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>My Activity</h2>
          <p className="muted">Log work or propose a new activity category</p>
        </div>
      </div>

      <section className="card stack">
        <div className="toggle-row">
          <button
            type="button"
            className={mode === 'submit' ? 'tab active' : 'tab'}
            onClick={() => setMode('submit')}
          >
            Log activity
          </button>
          <button
            type="button"
            className={mode === 'propose' ? 'tab active' : 'tab'}
            onClick={() => setMode('propose')}
          >
            Propose category
          </button>
        </div>

        <form onSubmit={handleSubmit} className="stack">
          {mode === 'submit' ? (
            <>
              <label className="field">
                <span>Category</span>
                <select
                  value={activityTypeId}
                  onChange={(e) => setActivityTypeId(e.target.value)}
                  required
                >
                  <option value="">Select an approved category</option>
                  {(typesQuery.data ?? []).map((type) => (
                    <option key={type.id} value={type.id}>
                      {type.name} ({type.basePoints ?? 0} pts)
                    </option>
                  ))}
                </select>
              </label>
              <label className="field">
                <span>Title</span>
                <input value={title} onChange={(e) => setTitle(e.target.value)} required />
              </label>
              <label className="field">
                <span>Description</span>
                <textarea
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  rows={3}
                />
              </label>
            </>
          ) : (
            <>
              <label className="field">
                <span>Proposed name</span>
                <input
                  value={proposedName}
                  onChange={(e) => setProposedName(e.target.value)}
                  required
                />
              </label>
              <label className="field">
                <span>Description</span>
                <textarea
                  value={proposedDescription}
                  onChange={(e) => setProposedDescription(e.target.value)}
                  required
                  rows={3}
                />
              </label>
              <label className="field">
                <span>Suggested points</span>
                <input
                  type="number"
                  min="1"
                  value={suggestedPoints}
                  onChange={(e) => setSuggestedPoints(e.target.value)}
                />
              </label>
            </>
          )}

          {formError && <p className="error">{formError}</p>}

          <button
            type="submit"
            className="btn btn-primary"
            disabled={submitMutation.isPending || proposeMutation.isPending}
          >
            {mode === 'submit' ? 'Submit activity' : 'Propose category'}
          </button>
        </form>
      </section>
    </div>
  );
}
