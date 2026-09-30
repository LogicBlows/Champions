export interface LoginResponse {
  token: string;
  employeeId: string;
  name: string;
  isReviewer: boolean;
}

export interface ActivityType {
  id: string;
  name: string;
  basePoints: number | null;
  description: string;
  sortOrder: number;
}

export interface PendingActivityType {
  id: string;
  name: string;
  description: string;
  suggestedPoints: number | null;
  proposedByName: string | null;
}

export interface MyActivity {
  id: string;
  employeeId: string;
  title: string;
  categoryName: string;
  status: string;
  pointsAwarded: number | null;
  isPendingCategoryReview: boolean;
  submittedOn: string;
  endorsementCount: number;
}

export interface MyActivitiesResponse {
  totalPoints: number;
  activities: MyActivity[];
}

export interface EmployeeActivitiesResponse {
  employeeId: string;
  employeeName: string;
  totalPoints: number;
  activities: MyActivity[];
}

export interface DashboardSummary {
  totalActivities: number;
  totalPoints: number;
  participatingEmployees: number;
  totalActiveEmployees: number;
  topCategory: string;
}

export interface DashboardLeaderboardEntry {
  rank: number;
  employeeId: string;
  name: string;
  totalPoints: number;
  activityCount: number;
  lastActivity: string | null;
}

export interface DashboardResponse {
  summary: DashboardSummary;
  leaderboard: DashboardLeaderboardEntry[];
}
