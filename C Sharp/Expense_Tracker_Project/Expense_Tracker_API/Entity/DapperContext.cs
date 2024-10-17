using Microsoft.Data.SqlClient;
using System.Data;

namespace Expense_Tracker_API.Entity
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration)
        {
            configuration = configuration;
            connectionString = configuration.GetConnectionString("SQLConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }
}
