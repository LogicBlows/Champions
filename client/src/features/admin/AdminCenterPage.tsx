import { Link } from 'react-router-dom';

export default function AdminCenterPage() {
  return (
    <div className="stack-lg">
      <div className="page-heading">
        <div>
          <h2>Admin center</h2>
          <p className="muted">Manage employees, competencies, and competency heads</p>
        </div>
      </div>

      <section className="card stack">
        <h3>Management</h3>
        <div className="stack">
          <Link to="/admin/employees" className="btn btn-primary">
            Employees
          </Link>
          <Link to="/admin/competencies" className="btn btn-ghost">
            Competencies
          </Link>
        </div>
      </section>
    </div>
  );
}
