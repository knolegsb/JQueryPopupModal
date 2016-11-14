using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JQueryPopupModal.Entities
{
    public class CustomerDB
    {
        // declare connection string
        string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // Retrun list of all employees
        public List<Customer> ListAll()
        {
            List<Customer> list = new List<Customer>();
            using(SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand sqlCom = new SqlCommand("SelectCustomers", con);
                sqlCom.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlReader = sqlCom.ExecuteReader();
                while (sqlReader.Read())
                {
                    list.Add(new Customer
                    {
                        CustomerId = Convert.ToInt32(sqlReader["CustomerId"]),
                        Name = sqlReader["Name"].ToString(),
                        Age = Convert.ToInt32(sqlReader["Age"]),
                        State = sqlReader["State"].ToString(),
                        Country = sqlReader["Country"].ToString(),
                    });
                }
                return list;
            }            
        }

        // Method for Adding an Employee
        public int Add(Customer cus)
        {
            int i;
            using(SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateCustomers", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", cus.CustomerId);
                com.Parameters.AddWithValue("@Name", cus.Name);
                com.Parameters.AddWithValue("@Age", cus.Age);
                com.Parameters.AddWithValue("@State", cus.State);
                com.Parameters.AddWithValue("@Country", cus.Country);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        // Method for Updating Employee record
        public int Update(Customer cus)
        {
            int i;
            using(SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateCustomers", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", cus.CustomerId);
                com.Parameters.AddWithValue("@Name", cus.Name);
                com.Parameters.AddWithValue("@Age", cus.Age);
                com.Parameters.AddWithValue("@State", cus.State);
                com.Parameters.AddWithValue("@Country", cus.Country);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        // Method for Deleting an Employees
        public int Delete(int Id)
        {
            int i;
            using(SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteCustomers", con);
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}