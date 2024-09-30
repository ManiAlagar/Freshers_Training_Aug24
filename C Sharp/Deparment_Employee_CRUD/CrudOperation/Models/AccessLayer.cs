using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<Employee> EmployeeDetails()
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            List<Employee> EmployeeList = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ViewTable_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.DepartmentName = rdr["DepartmentName"].ToString();
                    employee.MobileNumber = rdr["MobileNumber"].ToString();

                    EmployeeList.Add(employee);
                }
            }
            return EmployeeList;
        }



        public string AddEmployee(Employee employee)
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
                cmd.Parameters.AddWithValue("@DepartmentID", employee.DepartmentName);
                cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);

                con.Open();


                var status = cmd.ExecuteScalar();
               
                return status.ToString();
               
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
                    employee.ID = Convert.ToInt32(rdr["ID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.DepartmentName = rdr["DepartmentID"].ToString();
                    employee.MobileNumber = rdr["MobileNumber"].ToString();
                }
            }
            return employee;
        }

        //Update Operation
        public string UpdateEmployee(Employee employee)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "Update_EmployeeDetails";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@ID", employee.ID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@DepartmentID", employee.DepartmentName);
                cmd.Parameters.AddWithValue("@MobileNumber", employee.MobileNumber);

                con.Open();
                try
                {
                    var status = cmd.ExecuteScalar();
                    return status.ToString();
                }
                catch
                { 
                    var status = "Number";
                    return status;
                }

               
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