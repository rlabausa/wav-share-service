using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;
using System.Data.SqlClient;

namespace WavShareService.Health
{
    /// <summary>
    /// Health check for WavShareService database.
    /// </summary>
    public class WavShareDbHealthCheck : IHealthCheck
    {
        private string _dbConnectionString;
        private static string _SQL_QUERY = "SELECT 1;";

        /// <summary>
        /// Constructs a <see cref="WavShareDbHealthCheck"/> class.
        /// </summary>
        /// <param name="dbConnectionString"></param>
        public WavShareDbHealthCheck(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        async Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            try
            {
                using (var conn = new SqlConnection(_dbConnectionString))
                {
                    await conn.OpenAsync(cancellationToken);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = _SQL_QUERY;

                        await cmd.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            } catch (Exception exc)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: exc);
            }

            return HealthCheckResult.Healthy();
        }
    }
}
