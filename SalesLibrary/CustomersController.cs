using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesLibrary
{
    public class CustomersController
    {

        private SqlConnection sqlConnection { get; set; }

        public List<Customer> GetBySalesRange(decimal low, decimal high)
        {
            string sqlStatement = "SELECT * From Customers " +
                                  $" Where Sales >= {low} and Sales <= {high};";
            SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            List<Customer> customers = new List<Customer>();
            while (sqlReader.Read())
            {
                Customer customer = new Customer();
                customer.Id = Convert.ToInt32(sqlReader["Id"]);
                customer.Name = Convert.ToString(sqlReader["Name"]);
                customer.City = Convert.ToString(sqlReader["City"]);
                customer.State = Convert.ToString(sqlReader["State"]);
                customer.Sales = Convert.ToDecimal(sqlReader["Sales"]);
                customer.Active = Convert.ToBoolean(sqlReader["Active"]);
                customers.Add(customer);
            }
            sqlReader.Close();
            return customers;
        }

        public List<Customer> GetAll()
        {
            string sqlStatement = "SELECT * From Customers;";
            SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            List<Customer> customers = new List<Customer>();
            while(sqlReader.Read())
            {
                Customer customer = new Customer();
                customer.Id = Convert.ToInt32(sqlReader["Id"]);
                customer.Name = Convert.ToString(sqlReader["Name"]);
                customer.City = Convert.ToString(sqlReader["City"]);
                customer.State = Convert.ToString(sqlReader["State"]);
                customer.Sales = Convert.ToDecimal(sqlReader["Sales"]);
                customer.Active = Convert.ToBoolean(sqlReader["Active"]);
                customers.Add(customer);
            }
            sqlReader.Close();
            return customers;
        }

        public Customer? GetById(int Id)
        {
            string sqlStatement = $"SELECT * From Customers Where Id = {Id};";
            SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            if (!sqlReader.HasRows)
            {
                sqlReader.Close();
                return null;
            }
            sqlReader.Read();
            Customer customer = new Customer();
            customer.Id = Convert.ToInt32(sqlReader["Id"]);
            customer.Name = Convert.ToString(sqlReader["Name"]);
            customer.City = Convert.ToString(sqlReader["City"]);
            customer.State = Convert.ToString(sqlReader["State"]);
            customer.Sales = Convert.ToDecimal(sqlReader["Sales"]);
            customer.Active = Convert.ToBoolean(sqlReader["Active"]);
            sqlReader.Close();
            return customer;
        }

        public bool AddCustomer(Customer customerInstance)
        {
            string sqlStatement = "INSERT Customers (Name, City, State, Sales, Active) VALUES " +
                                  $" ( '{customerInstance.Name}', '{customerInstance.City}', '{customerInstance.State}', {customerInstance.Sales}, {(customerInstance.Active ? 1 : 0)}); ";
            SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                return false;
            }

            return true;
        }

        public bool UpdateRow(Customer customerInstance)
        {
            string sqlStatement = " UPDATE Customers SET " +
                                  $" Name = '{customerInstance.Name}', " +
                                  $" City = '{customerInstance.City}', " +
                                  $" State = '{customerInstance.State}', " +
                                  $" Sales = '{customerInstance.Sales}', " +
                                  $" Active = {(customerInstance.Active ? 1 : 0)} " +
                                  $" Where Id = {customerInstance.Id}; ";
            SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteRow(int Id)
        {
            string sqlStatement = " DELETE Customers " +
                                  $" Where Id = {Id}; ";                               
            SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection);
            int rowsAffected = sqlCommand.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                return false;
            }

            return true;
        }

        public CustomersController(string server, string instance) 
        {
            string connectionString = $"server={server}\\{instance};" +
                                      "database=SalesDb;" +
                                      "trusted_connection=true;" +
                                      "trustServerCertificate=true;";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            if (sqlConnection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection failed!");
            }
        }

        public void CloseConnection()
        {
            if(sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

    }
}
