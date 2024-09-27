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


        //Select operation
        public DataTable DepartmentDetails()

        {  
            string connectionString = _configuration.GetConnectionString("SQLConnection");
            string sql = "ViewTable_Department";

            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter data = new SqlDataAdapter(sql, conn);
                data.Fill(dataTable);
            }
            return dataTable;
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
        public void AddDepartment(Department department)
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
                try
                {
                    var res = cmd.ExecuteNonQuery();               
                    con.Close();
                }
                catch (Exception)
                { 
                    return;
                }
             
            }
        }

        //Update operation
        public void UpdateDepartment(Department department)
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
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return;
                }
                con.Close();
            }
        }

        //Delete Operation
        public void DeleteDepartment(int? id)
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
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}

