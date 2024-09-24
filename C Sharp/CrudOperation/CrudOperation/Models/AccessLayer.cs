using System.Data.SqlClient;
using System.Data;

namespace CrudOperation.Models

{
    public class AccessLayer
    {
        private readonly IConfiguration _configuration;

        public AccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //To View all employees details    
        //public IEnumerable<Employee> ViewTable_Employee()
        //{
        //    string connectionString = _configuration.GetConnectionString("SQLConnection");
        //    List<Employee> EmployeeList = new List<Employee>();

        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("EXEC ViewTable_Employee", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            Employee employee = new Employee();

        //            employee.Name = rdr["Name"].ToString();
        //            employee.Gender = rdr["Gender"].ToString();
        //            employee.Department = rdr["Department"].ToString();

        //            EmployeeList.Add(employee);
        //        }
        //    }
        //    return EmployeeList;
        //}

        public void AddEmployee(Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");

            string sql = "InsertInto_Employee";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);

                con.Open();
                var res = cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        //Get the details of a particular employee  
        public Employee GetEmployeeData(int? id)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employee WHERE ID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.Id = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                }
            }
            return employee;
        }

        //Update Operation
        public void UpdateEmployee(Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "Update_EmployeeDetails";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@ID", employee.Id);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Delete Operation
        public void DeleteEmployee(int? id)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "DeleteRecordIn_Employee";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}