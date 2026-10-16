import { type FormEvent, useState } from 'react';
import { Link } from 'react-router-dom';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { ApiError, apiRequest } from '../../api/client';
import type { AdminCompetencyListItem, AdminEmployeeListItem } from '../../api/types';
import { useAuth } from '../auth/AuthContext';

const emptyCreate = {
  employeeCode: '',
  name: '',
  email: '',
  competencyId: '',
  isReviewer: false,
  isAdmin: false,
};

export default function AdminEmployeesPage() {
  const { token } = useAuth();
  const queryClient = useQueryClient();
  const [includeInactive, setIncludeInactive] = useState(false);
  const [createForm, setCreateForm] = useState(emptyCreate);
  const [editingId, setEditingId] = useState<string | null>(null);
  const [editForm, setEditForm] = useState({
    name: '',
    email: '',
    competencyId: '',
    isReviewer: false,
    isAdmin: false,
    isActive: true,
  });
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const competenciesQuery = useQuery({
    queryKey: ['admin-competencies', true],
    enabled: Boolean(token),
    queryFn: () =>
      apiRequest<AdminCompetencyListItem[]>('/admin/competencies?includeInactive=true', {
        token,
      }),
  });

  const employeesQuery = useQuery({
    queryKey: ['admin-employees', includeInactive],
    enabled: Boolean(token),
    queryFn: () =>
      apiRequest<AdminEmployeeListItem[]>(
        `/admin/employees?includeInactive=${includeInactive}`,
        { token },
      ),
  });

  const competencies = competenciesQuery.data?.filter((c) => c.isActive) ?? [];
  const allCompetencies = competenciesQuery.data ?? [];

  function showApiError(err: unknown) {
    setError(err instanceof ApiError ? err.message : 'Something went wrong.');
  }

  const createMutation = useMutation({
    mutationFn: () =>
      apiRequest('/admin/employees', {
        method: 'POST',
        token,
        body: JSON.stringify({
          employeeCode: createForm.employeeCode.trim(),
          name: createForm.name.trim(),
          email: createForm.email.trim(),
          competencyId: createForm.competencyId,
          isReviewer: createForm.isReviewer,
          isAdmin: createForm.isAdmin,
        }),
      }),
    onSuccess: async () => {
      setCreateForm(emptyCreate);
      setMessage('Employee created.');
      setError(null);
      await queryClient.invalidateQueries({ queryKey: ['admin-employees'] });
    },
    onError: showApiError,
  });

  const updateMutation = useMutation({
    mutationFn: (id: string) =>
      apiRequest(`/admin/employees/${id}`, {
        method: 'PUT',
        token,
        body: JSON.stringify({
          name: editForm.name.trim(),
          email: editForm.email.trim(),
          competencyId: editForm.competencyId,
          isReviewer: editForm.isReviewer,
          isAdmin: editForm.isAdmin,
          isActive: editForm.isActive,
        }),
      }),
    onSuccess: async () => {
      setEditingId(null);
      setMessage('Employee updated.');
      setError(null);
      await queryClient.invalidateQueries({ queryKey: ['admin-employees'] });
      await queryClient.invalidateQueries({ queryKey: ['admin-competencies'] });
    },
    onError: showApiError,
  });

  const deactivateMutation = useMutation({
    mutationFn: (id: string) =>
      apiRequest(`/admin/employees/${id}/deactivate`, { method: 'POST', token }),
    onSuccess: async () => {
      setMessage('Employee deactivated.');
      setError(null);
      await queryClient.invalidateQueries({ queryKey: ['admin-employees'] });
      await queryClient.invalidateQueries({ queryKey: ['admin-competencies'] });
    },
    onError: showApiError,
  });

  function startEdit(row: AdminEmployeeListItem) {
    setEditingId(row.id);
    setEditForm({
      name: row.name,
      email: row.email,
      competencyId: row.competencyId,
      isReviewer: row.isReviewer,
      isAdmin: row.isAdmin,
      isActive: row.isActive,
    });
    setMessage(null);
    setError(null);
  }

  function handleCreate(event: FormEvent) {
    event.preventDefault();
    if (!createForm.competencyId) {
      setError('Select a competency.');
      return;
    }
    createMutation.mutate();
  }

  const loadError = employeesQuery.error ?? competenciesQuery.error;
  const isLoading =
    employeesQuery.isLoading || competenciesQuery.isLoading || !token;

  if (isLoading) {
    return <p className="muted">Loading employees…</p>;
  }

  if (loadError) {
    const detail =
      loadError instanceof ApiError
        ? loadError.message
        : 'Failed to load admin data.';
    return (
      <p className="error">
        {detail}
        {loadError instanceof ApiError && loadError.status === 404
          ? ' Restart the API so it includes the latest admin endpoints.'
          : ''}
      </p>
    );
  }

  const employees = employeesQuery.data ?? [];

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>Employees</h2>
          <p className="muted">
            <Link to="/admin">Admin center</Link> · Create and manage portal users
          </p>
        </div>
      </div>

      {message && <p className="muted">{message}</p>}
      {error && <p className="error">{error}</p>}

      <section className="card stack">
        <h3>Add employee</h3>
        <form className="stack" onSubmit={handleCreate}>
          <div className="toolbar">
            <label className="field">
              <span>Code</span>
              <input
                value={createForm.employeeCode}
                onChange={(e) => setCreateForm((f) => ({ ...f, employeeCode: e.target.value }))}
                required
              />
            </label>
            <label className="field grow">
              <span>Name</span>
              <input
                value={createForm.name}
                onChange={(e) => setCreateForm((f) => ({ ...f, name: e.target.value }))}
                required
              />
            </label>
            <label className="field grow">
              <span>Email</span>
              <input
                type="email"
                value={createForm.email}
                onChange={(e) => setCreateForm((f) => ({ ...f, email: e.target.value }))}
                required
              />
            </label>
            <label className="field">
              <span>Competency</span>
              <select
                value={createForm.competencyId}
                onChange={(e) => setCreateForm((f) => ({ ...f, competencyId: e.target.value }))}
                required
              >
                <option value="">Select…</option>
                {competencies.map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.name}
                  </option>
                ))}
              </select>
            </label>
          </div>
          <div className="toolbar">
            <label className="field checkbox-field">
              <input
                type="checkbox"
                checked={createForm.isReviewer}
                onChange={(e) => setCreateForm((f) => ({ ...f, isReviewer: e.target.checked }))}
              />
              <span>Reviewer</span>
            </label>
            <label className="field checkbox-field">
              <input
                type="checkbox"
                checked={createForm.isAdmin}
                onChange={(e) => setCreateForm((f) => ({ ...f, isAdmin: e.target.checked }))}
              />
              <span>Admin</span>
            </label>
            <button type="submit" className="btn btn-primary" disabled={createMutation.isPending}>
              Create
            </button>
          </div>
        </form>
      </section>

      <section className="card stack">
        <div className="toolbar">
          <h3>All employees</h3>
          <label className="field checkbox-field">
            <input
              type="checkbox"
              checked={includeInactive}
              onChange={(e) => setIncludeInactive(e.target.checked)}
            />
            <span>Include inactive</span>
          </label>
        </div>

        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>Code</th>
                <th>Name</th>
                <th>Email</th>
                <th>Competency</th>
                <th>Roles</th>
                <th>Status</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {employees.map((row) => (
                <tr key={row.id}>
                  <td>{row.employeeCode}</td>
                  <td>{row.name}</td>
                  <td>{row.email}</td>
                  <td>{row.competencyName}</td>
                  <td>
                    {row.isAdmin && <span className="badge-reviewer">Admin</span>}
                    {row.isReviewer && <span className="badge-reviewer">Reviewer</span>}
                  </td>
                  <td>{row.isActive ? 'Active' : 'Inactive'}</td>
                  <td>
                    <button type="button" className="btn btn-ghost btn-sm" onClick={() => startEdit(row)}>
                      Edit
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>

      {editingId && (
        <section className="card stack">
          <h3>Edit employee</h3>
          <form
            className="stack"
            onSubmit={(e) => {
              e.preventDefault();
              updateMutation.mutate(editingId);
            }}
          >
            <div className="toolbar">
              <label className="field grow">
                <span>Name</span>
                <input
                  value={editForm.name}
                  onChange={(e) => setEditForm((f) => ({ ...f, name: e.target.value }))}
                  required
                />
              </label>
              <label className="field grow">
                <span>Email</span>
                <input
                  type="email"
                  value={editForm.email}
                  onChange={(e) => setEditForm((f) => ({ ...f, email: e.target.value }))}
                  required
                />
              </label>
              <label className="field">
                <span>Competency</span>
                <select
                  value={editForm.competencyId}
                  onChange={(e) => setEditForm((f) => ({ ...f, competencyId: e.target.value }))}
                  required
                >
                  {allCompetencies
                    .filter((c) => c.isActive || c.id === editForm.competencyId)
                    .map((c) => (
                      <option key={c.id} value={c.id}>
                        {c.name}
                      </option>
                    ))}
                </select>
              </label>
            </div>
            <div className="toolbar">
              <label className="field checkbox-field">
                <input
                  type="checkbox"
                  checked={editForm.isReviewer}
                  onChange={(e) => setEditForm((f) => ({ ...f, isReviewer: e.target.checked }))}
                />
                <span>Reviewer</span>
              </label>
              <label className="field checkbox-field">
                <input
                  type="checkbox"
                  checked={editForm.isAdmin}
                  onChange={(e) => setEditForm((f) => ({ ...f, isAdmin: e.target.checked }))}
                />
                <span>Admin</span>
              </label>
              <label className="field checkbox-field">
                <input
                  type="checkbox"
                  checked={editForm.isActive}
                  onChange={(e) => setEditForm((f) => ({ ...f, isActive: e.target.checked }))}
                />
                <span>Active</span>
              </label>
            </div>
            <div className="toolbar">
              <button type="submit" className="btn btn-primary" disabled={updateMutation.isPending}>
                Save
              </button>
              <button type="button" className="btn btn-ghost" onClick={() => setEditingId(null)}>
                Cancel
              </button>
              {editForm.isActive && (
                <button
                  type="button"
                  className="btn btn-danger"
                  disabled={deactivateMutation.isPending}
                  onClick={() => deactivateMutation.mutate(editingId)}
                >
                  Deactivate
                </button>
              )}
            </div>
          </form>
        </section>
      )}
    </div>
  );
}
