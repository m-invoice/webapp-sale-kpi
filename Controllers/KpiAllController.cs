using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sale.Kpi.Api.Controllers;

[ApiController]
[Route("api/kpi")]
public class KpiAllController : ControllerBase
{
    private readonly IConfiguration _config;
    public KpiAllController(IConfiguration config) => _config = config;

    private SqlConnection NewConn() => new SqlConnection(_config.GetConnectionString("Default"));

    // KPI #6
    [HttpGet("revenue/by-source")]
    public async Task<IActionResult> RevenueBySource([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        await using var conn = NewConn();
        var data = await conn.QueryAsync(
            "dbo.sp_kpi_revenue_by_source_kdvp",
            new { FromDate = from.ToString("yyyy-MM-dd"), ToDate = to.ToString("yyyy-MM-dd") },
            commandType: CommandType.StoredProcedure
        );
        return Ok(data);
    }

    // KPI #7
    [HttpGet("funnel/by-stage")]
    public async Task<IActionResult> FunnelByStage([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        await using var conn = NewConn();
        var data = await conn.QueryAsync(
            "dbo.sp_kpi_funnel_by_stage_kdvp",
            new { FromDate = from.ToString("yyyy-MM-dd"), ToDate = to.ToString("yyyy-MM-dd") },
            commandType: CommandType.StoredProcedure
        );
        return Ok(data);
    }

    // KPI #8
    [HttpGet("funnel/age-bucket")]
    public async Task<IActionResult> AgeBucket([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        await using var conn = NewConn();
        var data = await conn.QueryAsync(
            "dbo.sp_kpi_lead_age_bucket_kdvp",
            new { FromDate = from.ToString("yyyy-MM-dd"), ToDate = to.ToString("yyyy-MM-dd") },
            commandType: CommandType.StoredProcedure
        );
        return Ok(data);
    }

    // KPI #9
    [HttpGet("sales/leaderboard")]
    public async Task<IActionResult> Leaderboard([FromQuery] DateOnly from, [FromQuery] DateOnly to, [FromQuery] int top = 20)
    {
        await using var conn = NewConn();
        var data = await conn.QueryAsync(
            "dbo.sp_kpi_sales_leaderboard_kdvp",
            new { FromDate = from.ToString("yyyy-MM-dd"), ToDate = to.ToString("yyyy-MM-dd"), Top = top },
            commandType: CommandType.StoredProcedure
        );
        return Ok(data);
    }

    // KPI #10
    [HttpGet("quality/summary")]
    public async Task<IActionResult> QualitySummary([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        await using var conn = NewConn();
        var data = await conn.QueryFirstOrDefaultAsync(
            "dbo.sp_kpi_data_quality_kdvp",
            new { FromDate = from.ToString("yyyy-MM-dd"), ToDate = to.ToString("yyyy-MM-dd") },
            commandType: CommandType.StoredProcedure
        );
        return Ok(data);
    }
}
