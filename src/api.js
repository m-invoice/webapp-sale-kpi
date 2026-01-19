import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5005",
  timeout: 20000,
});

export const kpi = {
  revenueDaily: (from, to) =>
    api.get("/api/kpi/revenue/daily", { params: { from, to } }).then((r) => r.data),
};
