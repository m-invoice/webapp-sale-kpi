using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Sale.Kpi.Api.Controllers;

[ApiController]
[Route("api/kpi/revenue")]
public class KpiRevenueController : ControllerBase
{
    private readonly IConfiguration _config;
    public KpiRevenueController(IConfiguration config) => _config = config;

    [HttpGet("daily")]
    public async Task<IActionResult> Daily([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        await using var conn = new SqlConnection(_config.GetConnectionString("Default"));

        var data = await conn.QueryAsync(
            "dbo.sp_kpi_revenue_daily_kdvp",
            new { FromDate = from.ToString("yyyy-MM-dd"), ToDate = to.ToString("yyyy-MM-dd") },
            commandType: CommandType.StoredProcedure
        );

        return Ok(data);
    }
}
