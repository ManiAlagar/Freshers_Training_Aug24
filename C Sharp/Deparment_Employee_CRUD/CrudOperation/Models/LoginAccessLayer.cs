using System.Data.SqlClient;
using System.Data;

namespace CrudOperation.Models
{
    public class LoginAccessLayer
    {
        private readonly IConfiguration _configuration;

        public LoginAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Check(Login user)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");

            string sql = "Login_Check";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                con.Open();
                var flag = cmd.ExecuteScalar();
                con.Close();

                return flag.ToString();
            }
        }
    }
}
