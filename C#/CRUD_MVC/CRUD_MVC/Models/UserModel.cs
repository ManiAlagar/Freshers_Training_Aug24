using System.Data;
using System.Data.SqlClient;

namespace CRUD_MVC.Models
{
    public class UserModel
    {
        private readonly string connectionString;

        public UserModel(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        
        public string LoginCheck(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                var res= cmd.ExecuteScalar();
                con.Close();
                return res.ToString();
            }
        }
    }
    
}
