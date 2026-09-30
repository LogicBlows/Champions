import type { MyActivity } from '../api/types';

interface ActivityHistoryTableProps {
  activities: MyActivity[];
  currentEmployeeId: string;
  onEndorse: (activityId: string) => void;
  endorsingActivityId: string | null;
}

function endorsementLabel(count: number) {
  return `${count} endorsement${count === 1 ? '' : 's'}`;
}

export default function ActivityHistoryTable({
  activities,
  currentEmployeeId,
  onEndorse,
  endorsingActivityId,
}: ActivityHistoryTableProps) {
  return (
    <div className="table-wrap">
      <table>
        <thead>
          <tr>
            <th>Title</th>
            <th>Category</th>
            <th>Status</th>
            <th>Points</th>
            <th>Date</th>
            <th>Endorsements</th>
          </tr>
        </thead>
        <tbody>
          {activities.map((activity) => {
            const isOwnActivity = activity.employeeId === currentEmployeeId;
            return (
              <tr key={activity.id}>
                <td>{activity.title}</td>
                <td>{activity.categoryName}</td>
                <td>{activity.status}</td>
                <td>
                  {activity.isPendingCategoryReview
                    ? 'Pending category review'
                    : (activity.pointsAwarded ?? '—')}
                </td>
                <td>{new Date(activity.submittedOn).toLocaleDateString()}</td>
                <td>
                  <div className="endorse-cell">
                    <span className="muted">{endorsementLabel(activity.endorsementCount)}</span>
                    {!isOwnActivity && (
                      <button
                        type="button"
                        className="btn btn-ghost"
                        disabled={endorsingActivityId === activity.id}
                        onClick={() => onEndorse(activity.id)}
                      >
                        {endorsingActivityId === activity.id ? 'Endorsing…' : 'Endorse'}
                      </button>
                    )}
                  </div>
                </td>
              </tr>
            );
          })}
          {activities.length === 0 && (
            <tr>
              <td colSpan={6} className="muted">
                No activities submitted yet.
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
}
