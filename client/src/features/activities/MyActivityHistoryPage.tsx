import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { apiRequest } from '../../api/client';
import type { MyActivitiesResponse } from '../../api/types';
import ActivityHistoryTable from '../../components/ActivityHistoryTable';
import { useAuth } from '../auth/AuthContext';

export default function MyActivityHistoryPage() {
  const { token, employeeId } = useAuth();
  const queryClient = useQueryClient();

  const activitiesQuery = useQuery({
    queryKey: ['my-activities'],
    queryFn: () =>
      apiRequest<MyActivitiesResponse>('/me/activities', { token }),
  });

  const endorseMutation = useMutation({
    mutationFn: (activityId: string) =>
      apiRequest<void>(`/activities/${activityId}/endorse`, {
        method: 'POST',
        token,
        body: JSON.stringify({ comment: null }),
      }),
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['my-activities'] });
      await queryClient.invalidateQueries({ queryKey: ['dashboard'] });
    },
  });

  const activities = activitiesQuery.data?.activities ?? [];
  const totalPoints = activitiesQuery.data?.totalPoints ?? 0;

  if (activitiesQuery.isLoading) {
    return <p className="muted">Loading activity history…</p>;
  }
  if (activitiesQuery.error) {
    return <p className="error">Failed to load activity history.</p>;
  }

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>History</h2>
          <p className="muted">Your submitted activities and endorsements</p>
        </div>
        <div className="points-pill">
          <span>Running total</span>
          <strong>{totalPoints} pts</strong>
        </div>
      </div>

      <section className="card stack">
        <ActivityHistoryTable
          activities={activities}
          currentEmployeeId={employeeId ?? ''}
          onEndorse={(activityId) => endorseMutation.mutate(activityId)}
          endorsingActivityId={
            endorseMutation.isPending ? (endorseMutation.variables ?? null) : null
          }
        />
      </section>
    </div>
  );
}
