import { useEffect, useState } from "react";
import { kpi } from "./api";

export default function App() {
  const [from, setFrom] = useState("2026-01-01");
  const [to, setTo] = useState("2026-01-17");
  const [rows, setRows] = useState([]);
  const [err, setErr] = useState("");
  const [loading, setLoading] = useState(false);

  const load = async () => {
    setErr("");
    setLoading(true);
    try {
      const data = await kpi.revenueDaily(from, to);
      setRows(data);
    } catch (e) {
      setErr(e?.message || String(e));
      console.error(e);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
  }, []);

  return (
    <div style={{ padding: 16, fontFamily: "Segoe UI, Arial" }}>
      <h2>WebApp-Sale — KPI #5 Revenue Daily</h2>

      <div style={{ display: "flex", gap: 8, alignItems: "center", marginBottom: 12 }}>
        <label>
          From{" "}
          <input value={from} onChange={(e) => setFrom(e.target.value)} />
        </label>
        <label>
          To{" "}
          <input value={to} onChange={(e) => setTo(e.target.value)} />
        </label>
        <button onClick={load} disabled={loading}>
          {loading ? "Loading..." : "Refresh"}
        </button>
        {err && <span style={{ color: "crimson" }}>Error: {err}</span>}
      </div>

      <SimpleTable rows={rows} />
    </div>
  );
}

function SimpleTable({ rows }) {
  if (!rows || rows.length === 0) return <div style={{ color: "#666" }}>No data</div>;
  const cols = Object.keys(rows[0]);

  return (
    <div style={{ overflowX: "auto" }}>
      <table border="1" cellPadding="6" style={{ borderCollapse: "collapse", width: "100%" }}>
        <thead>
          <tr>{cols.map((c) => <th key={c}>{c}</th>)}</tr>
        </thead>
        <tbody>
          {rows.map((r, i) => (
            <tr key={i}>
              {cols.map((c) => <td key={c}>{String(r[c] ?? "")}</td>)}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
