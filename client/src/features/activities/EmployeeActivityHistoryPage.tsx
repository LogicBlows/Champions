import { useParams } from 'react-router-dom';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { apiRequest } from '../../api/client';
import type { EmployeeActivitiesResponse } from '../../api/types';
import ActivityHistoryTable from '../../components/ActivityHistoryTable';
import { useAuth } from '../auth/AuthContext';

export default function EmployeeActivityHistoryPage() {
  const { employeeId = '' } = useParams();
  const { token, employeeId: currentEmployeeId } = useAuth();
  const queryClient = useQueryClient();

  const activitiesQuery = useQuery({
    queryKey: ['employee-activities', employeeId],
    queryFn: () =>
      apiRequest<EmployeeActivitiesResponse>(`/employees/${employeeId}/activities`, { token }),
    enabled: Boolean(employeeId),
  });

  const endorseMutation = useMutation({
    mutationFn: (activityId: string) =>
      apiRequest<void>(`/activities/${activityId}/endorse`, {
        method: 'POST',
        token,
        body: JSON.stringify({ comment: null }),
      }),
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['employee-activities', employeeId] });
      await queryClient.invalidateQueries({ queryKey: ['dashboard'] });
      await queryClient.invalidateQueries({ queryKey: ['my-activities'] });
    },
  });

  if (activitiesQuery.isLoading) return <p className="muted">Loading activity history…</p>;
  if (activitiesQuery.error) return <p className="error">Failed to load activity history.</p>;
  if (!activitiesQuery.data) return null;

  const { employeeName, totalPoints, activities } = activitiesQuery.data;

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>{employeeName}&apos;s Activities</h2>
          <p className="muted">Read-only activity history</p>
        </div>
        <div className="points-pill">
          <span>Total points</span>
          <strong>{totalPoints} pts</strong>
        </div>
      </div>

      <section className="card stack">
        <h3>History</h3>
        <ActivityHistoryTable
          activities={activities}
          currentEmployeeId={currentEmployeeId ?? ''}
          onEndorse={(activityId) => endorseMutation.mutate(activityId)}
          endorsingActivityId={
            endorseMutation.isPending ? (endorseMutation.variables ?? null) : null
          }
        />
      </section>
    </div>
  );
}
