using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace CrudOperation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult EmployeeDetails()
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "EXEC ViewTable_Employee";

            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter data = new SqlDataAdapter(sql, conn);
                data.Fill(dataTable);
            }
            return View(dataTable);
        }

    }
}
