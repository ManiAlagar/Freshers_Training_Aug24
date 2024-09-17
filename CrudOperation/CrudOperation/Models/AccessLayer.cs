using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Models

{
    public class AccessLayer
    {
        string connectionString = "Put Your Connection string here";

        //To View all employees details    
        public IEnumerable<Employee> ViewTable_Employee()
        {
            List<Employee> EmployeeList = new List<Employee>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("EXEC ViewTable_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Employee employee = new Employee();
     
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();

                    EmployeeList.Add(employee);
                }
            }
            return EmployeeList;
        }




        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }




        public void AddEmployee(Employee employee)
        {

            string sql = "EXEC InsertInto_Employee";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }




        //public IActionResult Delete(int id)
        //{
        //    string sql = "EXEC DeleteRecordIn_Employee " + id.ToString();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.ExecuteNonQuery();
        //    }
        //    return View(DataTable);
        //}

    }
}