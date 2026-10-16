import { type FormEvent, useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { ApiError, apiRequest } from '../../api/client';
import type { AdminCompetencyListItem, AdminEmployeeListItem } from '../../api/types';
import { useAuth } from '../auth/AuthContext';

const emptyCreate = {
  code: '',
  name: '',
  description: '',
  sortOrder: 0,
};

export default function AdminCompetenciesPage() {
  const { token } = useAuth();
  const queryClient = useQueryClient();
  const [includeInactive, setIncludeInactive] = useState(false);
  const [createForm, setCreateForm] = useState(emptyCreate);
  const [editingId, setEditingId] = useState<string | null>(null);
  const [editForm, setEditForm] = useState({
    name: '',
    description: '',
    sortOrder: 0,
    isActive: true,
  });
  const [headSelections, setHeadSelections] = useState<Record<string, string>>({});
  const [message, setMessage] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const competenciesQuery = useQuery({
    queryKey: ['admin-competencies', includeInactive],
    enabled: Boolean(token),
    queryFn: () =>
      apiRequest<AdminCompetencyListItem[]>(
        `/admin/competencies?includeInactive=${includeInactive}`,
        { token },
      ),
  });

  const employeesQuery = useQuery({
    queryKey: ['admin-employees', true],
    enabled: Boolean(token),
    queryFn: () =>
      apiRequest<AdminEmployeeListItem[]>('/admin/employees?includeInactive=false', { token }),
  });

  const employeesByCompetencyId = useMemo(() => {
    const map = new Map<string, AdminEmployeeListItem[]>();
    for (const emp of employeesQuery.data ?? []) {
      const list = map.get(emp.competencyId) ?? [];
      list.push(emp);
      map.set(emp.competencyId, list);
    }
    return map;
  }, [employeesQuery.data]);

  function showApiError(err: unknown) {
    setError(err instanceof ApiError ? err.message : 'Something went wrong.');
  }

  const createMutation = useMutation({
    mutationFn: () =>
      apiRequest('/admin/competencies', {
        method: 'POST',
        token,
        body: JSON.stringify({
          code: createForm.code.trim(),
          name: createForm.name.trim(),
          description: createForm.description.trim(),
          sortOrder: createForm.sortOrder,
        }),
      }),
    onSuccess: async () => {
      setCreateForm(emptyCreate);
      setMessage('Competency created.');
      setError(null);
      await queryClient.invalidateQueries({ queryKey: ['admin-competencies'] });
    },
    onError: showApiError,
  });

  const updateMutation = useMutation({
    mutationFn: (id: string) =>
      apiRequest(`/admin/competencies/${id}`, {
        method: 'PUT',
        token,
        body: JSON.stringify({
          name: editForm.name.trim(),
          description: editForm.description.trim(),
          sortOrder: editForm.sortOrder,
          isActive: editForm.isActive,
        }),
      }),
    onSuccess: async () => {
      setEditingId(null);
      setMessage('Competency updated.');
      setError(null);
      await queryClient.invalidateQueries({ queryKey: ['admin-competencies'] });
    },
    onError: showApiError,
  });

  const setHeadMutation = useMutation({
    mutationFn: ({ id, employeeId }: { id: string; employeeId: string | null }) =>
      apiRequest(`/admin/competencies/${id}/head`, {
        method: 'PUT',
        token,
        body: JSON.stringify({ employeeId }),
      }),
    onSuccess: async () => {
      setMessage('Competency head updated.');
      setError(null);
      await queryClient.invalidateQueries({ queryKey: ['admin-competencies'] });
      await queryClient.invalidateQueries({ queryKey: ['admin-employees'] });
    },
    onError: showApiError,
  });

  function startEdit(row: AdminCompetencyListItem) {
    setEditingId(row.id);
    setEditForm({
      name: row.name,
      description: row.description,
      sortOrder: row.sortOrder,
      isActive: row.isActive,
    });
    setMessage(null);
    setError(null);
  }

  function handleCreate(event: FormEvent) {
    event.preventDefault();
    createMutation.mutate();
  }

  const loadError = competenciesQuery.error ?? employeesQuery.error;
  const isLoading = competenciesQuery.isLoading || employeesQuery.isLoading || !token;

  if (isLoading) {
    return <p className="muted">Loading competencies…</p>;
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

  const competencies = competenciesQuery.data ?? [];

  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>Competencies</h2>
          <p className="muted">
            <Link to="/admin">Admin center</Link> · Categories and competency heads
          </p>
        </div>
      </div>

      {message && <p className="muted">{message}</p>}
      {error && <p className="error">{error}</p>}

      <section className="card stack">
        <h3>Add competency</h3>
        <form className="stack" onSubmit={handleCreate}>
          <div className="toolbar">
            <label className="field">
              <span>Code</span>
              <input
                value={createForm.code}
                onChange={(e) => setCreateForm((f) => ({ ...f, code: e.target.value }))}
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
            <label className="field">
              <span>Sort order</span>
              <input
                type="number"
                min={0}
                value={createForm.sortOrder}
                onChange={(e) =>
                  setCreateForm((f) => ({ ...f, sortOrder: Number(e.target.value) }))
                }
              />
            </label>
          </div>
          <label className="field">
            <span>Description</span>
            <textarea
              value={createForm.description}
              onChange={(e) => setCreateForm((f) => ({ ...f, description: e.target.value }))}
              required
              rows={2}
            />
          </label>
          <button type="submit" className="btn btn-primary" disabled={createMutation.isPending}>
            Create
          </button>
        </form>
      </section>

      <section className="card stack">
        <div className="toolbar">
          <h3>All competencies</h3>
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
                <th>Sort</th>
                <th>Head</th>
                <th>Status</th>
                <th />
              </tr>
            </thead>
            <tbody>
              {competencies.map((row) => {
                const candidates = employeesByCompetencyId.get(row.id) ?? [];
                const headValue =
                  headSelections[row.id] ??
                  (row.headEmployeeId ? row.headEmployeeId : '');
                return (
                  <tr key={row.id}>
                    <td>{row.code}</td>
                    <td>{row.name}</td>
                    <td>{row.sortOrder}</td>
                    <td>
                      <div className="stack">
                        <span className="muted">{row.headEmployeeName ?? '—'}</span>
                        {row.isActive && (
                          <div className="toolbar">
                            <select
                              value={headValue}
                              onChange={(e) =>
                                setHeadSelections((s) => ({ ...s, [row.id]: e.target.value }))
                              }
                            >
                              <option value="">No head</option>
                              {candidates.map((emp) => (
                                <option key={emp.id} value={emp.id}>
                                  {emp.name} ({emp.employeeCode})
                                </option>
                              ))}
                            </select>
                            <button
                              type="button"
                              className="btn btn-ghost btn-sm"
                              disabled={setHeadMutation.isPending}
                              onClick={() =>
                                setHeadMutation.mutate({
                                  id: row.id,
                                  employeeId: headValue || null,
                                })
                              }
                            >
                              Set head
                            </button>
                          </div>
                        )}
                      </div>
                    </td>
                    <td>{row.isActive ? 'Active' : 'Inactive'}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-ghost btn-sm"
                        onClick={() => startEdit(row)}
                      >
                        Edit
                      </button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      </section>

      {editingId && (
        <section className="card stack">
          <h3>Edit competency</h3>
          <form
            className="stack"
            onSubmit={(e) => {
              e.preventDefault();
              updateMutation.mutate(editingId);
            }}
          >
            <label className="field">
              <span>Name</span>
              <input
                value={editForm.name}
                onChange={(e) => setEditForm((f) => ({ ...f, name: e.target.value }))}
                required
              />
            </label>
            <label className="field">
              <span>Description</span>
              <textarea
                value={editForm.description}
                onChange={(e) => setEditForm((f) => ({ ...f, description: e.target.value }))}
                required
                rows={2}
              />
            </label>
            <div className="toolbar">
              <label className="field">
                <span>Sort order</span>
                <input
                  type="number"
                  min={0}
                  value={editForm.sortOrder}
                  onChange={(e) =>
                    setEditForm((f) => ({ ...f, sortOrder: Number(e.target.value) }))
                  }
                />
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
            </div>
          </form>
        </section>
      )}
    </div>
  );
}
