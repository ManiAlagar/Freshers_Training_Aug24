
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace CRUD_MVC.Models
{
    public class EmployeeCrud
    {
        private readonly  string connectionString;
        public EmployeeCrud(IConfiguration configuration)//constructor
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
    
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from EmpDetailsWithDept", con);
                cmd.CommandType = CommandType.Text;


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();

                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.EmployeeName = rdr["EmployeeName"].ToString();
                    employee.DepartmentName = rdr["DepartmentName"].ToString();
                    employee.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                    employee.PhoneNumber = Convert.ToInt64(rdr["PhoneNumber"]);
                    employee.City = rdr["City"].ToString();

                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        public string UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@City", employee.City);

                con.Open();
                var res = cmd.ExecuteScalar();
                con.Close();
                return res.ToString();
            }
        }


        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();
            
            using (SqlConnection con = new SqlConnection(connectionString)) // sql connection this class represents the connection to sql database and cannot be inherited.
            {
                string sqlQuery = "SELECT * FROM EmployeeDetails WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con); //sql command class allows you to query and send commands to a database.

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader(); //sqlDataReader - to read data from the SQL Server database in the most efficient manner. 

                while (rdr.Read()) //read -read the next character from the standard input stream. 
                {
                   
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.EmployeeName = rdr["EmployeeName"].ToString();
                    employee.PhoneNumber = Convert.ToInt64(rdr["PhoneNumber"]);
                    employee.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                    employee.City = rdr["City"].ToString();
                }
            }
            return employee;
        }

        public void DeleteEmployee(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        public string AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddEmployeeDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@PhoneNumber",employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@City", employee.City);

                con.Open();
                var res= cmd.ExecuteScalar();
                con.Close();
                return res.ToString();
            }
        }


        
    }
}
