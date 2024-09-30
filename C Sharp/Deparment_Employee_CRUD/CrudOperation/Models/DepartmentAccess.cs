using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation.Models
{
    public class DepartmentAccess
    {
        private readonly IConfiguration _configuration;

        public DepartmentAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Select operation in DepartmentTable
        public IEnumerable<Department> DepartmentDetails()
        {
            List<Department> DepartmentList = new List<Department>();

            string connectionString = _configuration.GetConnectionString("SQLConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ViewTable_Department", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Department department = new Department();

                    department.DepartmentID = Convert.ToInt32(rdr["DepartmentID"]);
                    department.DepartmentName = rdr["DepartmentName"].ToString();

                    DepartmentList.Add(department);
                }
            }
            return DepartmentList;
        }

            //Get the details of a particular Department records  
            public Department GetDepartmentData(int? id)
            {
                string connectionString = _configuration.GetConnectionString("SQLConnection");
                Department department = new Department();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM Department WHERE DepartmentID = " + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {   
                        department.DepartmentID = Convert.ToInt32(rdr["DepartmentID"]);
                        department.DepartmentName =  rdr["DepartmentName"].ToString();
                    }
                }
                return department;
            }



        //Insert into operation
        public string AddDepartment(Department department)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");

            string sql = "InsertInto_Department";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;
              
                cmd.Parameters.AddWithValue("@Department", department.DepartmentName);

                con.Open();
              
                var status = cmd.ExecuteScalar();

                return status.ToString();            
            }
        }

        //Update operation
        public string UpdateDepartment(Department department)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "Update_DepartmentDetails";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                cmd.Parameters.AddWithValue("@Department", department.DepartmentName);
                con.Open();
                var status = cmd.ExecuteScalar();

                return status.ToString();
            }
        }

        //Delete Operation
        public string DeleteDepartment(int? id)
        {
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "Delete_Department";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("@DepartmentID", id);

                con.Open();
                try
                {
                   var status =  cmd.ExecuteScalar().ToString();
                   return status;
                }
                catch (Exception)
                {
                    return "There is a relation for this department in employee table";
                }
            }
        }
    }
}

