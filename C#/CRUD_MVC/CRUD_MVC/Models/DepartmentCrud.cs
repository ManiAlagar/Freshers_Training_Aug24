using System.Data.SqlClient;
using System.Data;
using CRUD_MVC.Models;

namespace CRUD_MVC.Models
{
    public class DepartmentCrud
    {
        private readonly string connectionString;
        public DepartmentCrud(IConfiguration configuration)//constructor
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public IEnumerable<Department> GetAllDepartments()
        {
            List<Department> dept = new List<Department>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ViewDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Department department = new Department();

                    department.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                    department.DepartmentName = rdr["DepartmentName"].ToString();

                    dept.Add(department);
                }
                con.Close();
            }
            return dept;
        }
        public void  UpdateDepartment(Department department)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Department GetDepartmentData(int? id) 
        {
            Department department= new Department();

            using (SqlConnection con = new SqlConnection(connectionString)) // sql connection this class represents the connection to sql database and cannot be inherited.
            {
                string sqlQuery = "SELECT * FROM Department WHERE DepartmentId= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con); //sql command class allows you to query and send commands to a database.

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader(); //sqlDataReader - to read data from the SQL Server database in the most efficient manner. 

                while (rdr.Read()) //read -read the next character from the standard input stream. 
                {
                    department.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                    department.DepartmentName = rdr["DepartmentName"].ToString();
                    
                }
            }
            return department;
        }

        public void DeleteDepartment(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentId", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string AddDepartment(Department department)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                
                con.Open();
                var res = cmd.ExecuteScalar();
                con.Close();
                return res.ToString();
            }
        }

    }
}
