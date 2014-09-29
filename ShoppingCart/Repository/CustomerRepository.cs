using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCart.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        public void Create(Customer Model)
        {
            var connection = this.GetConnection();
            var command = connection.CreateCommand();

            command.CommandText = 
                "INSERT INTO Customers (Id, Name, Cellphone, Gender, BillAddress, ShipAddress, Email, Credit, DateCreated, DateLastMod) " +
                "VALUES (@id, @name, @cellphone, @gender, @billAddress, @shipAddress, @email, @credit, GETDATE(), GETDATE())";
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@id", Model.Id);
            command.Parameters.AddWithValue("@name", Model.Name);
            command.Parameters.AddWithValue("@cellphone", Model.Cellphone);
            command.Parameters.AddWithValue("@gender", Model.Gender);
            command.Parameters.AddWithValue("@billAddress", Model.BillAddress);
            command.Parameters.AddWithValue("@shipAddress", Model.ShipAddress);
            command.Parameters.AddWithValue("@email", Model.Email);
            command.Parameters.AddWithValue("@credit", Model.Credit);

            connection.Open();

            try
            {
                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected == 0)
                {
                    // handling data errors.
                }
            }
            catch (SqlException sqle)
            {
                // handling error in SQL Server.
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(Customer Model)
        {
            var connection = this.GetConnection();
            var command = connection.CreateCommand();

            command.CommandText =
                "UPDATE Customers SET Name = @name, Cellphone = @cellphone, Gender = @gender, " +
                "                     BillAddress = @billAddress, ShipAddress = @shipAddress, " +
                "                     Email = @email, Credit = @credit, DateLastMod = GETDATE() " +
                "WHERE Id = @id";
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@id", Model.Id);
            command.Parameters.AddWithValue("@name", Model.Name);
            command.Parameters.AddWithValue("@cellphone", Model.Cellphone);
            command.Parameters.AddWithValue("@gender", Model.Gender);
            command.Parameters.AddWithValue("@billAddress", Model.BillAddress);
            command.Parameters.AddWithValue("@shipAddress", Model.ShipAddress);
            command.Parameters.AddWithValue("@email", Model.Email);
            command.Parameters.AddWithValue("@credit", Model.Credit);

            connection.Open();

            try
            {
                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected == 0)
                {
                    // handling data errors.
                }
            }
            catch (SqlException sqle)
            {
                // handling error in SQL Server.
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(Customer Model)
        {
            var connection = this.GetConnection();
            var command = connection.CreateCommand();

            command.CommandText = "DELETE FROM Customers WHERE Id = @id";
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@id", Model.Id);

            connection.Open();

            try
            {
                int rowAffected = command.ExecuteNonQuery();

                if (rowAffected == 0)
                {
                    // handling data errors.
                }
            }
            catch (SqlException sqle)
            {
                // handling error in SQL Server.
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<Customer> Select()
        {
            var connection = this.GetConnection();
            var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Customers ORDER BY DateCreated DESC";
            command.CommandType = CommandType.Text;

            connection.Open();
            IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Customer> customers = new List<Customer>();

            while (reader.Read())
            {
                var customer = new Customer()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Cellphone = reader.GetString(reader.GetOrdinal("Cellphone")),
                    BillAddress = reader.GetString(reader.GetOrdinal("BillAddress")),
                    ShipAddress = reader.GetString(reader.GetOrdinal("ShipAddress")),
                    Gender = reader.GetBoolean(reader.GetOrdinal("Gender")),
                    Credit = reader.GetInt32(reader.GetOrdinal("Credit")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                    DateLastMod = reader.GetDateTime(reader.GetOrdinal("DateLastMod"))
                };

                customers.Add(customer);
            }

            reader.Close();

            return customers;
        }

        public int GetLatestIdentity()
        {
            var connection = this.GetConnection();
            var command = connection.CreateCommand();
            
            // use this if Id column is IDENTITY.
            //command.CommandText = "SELECT ISNULL(SCOPE_IDENTITY(), 0) + 1";
            command.CommandText = "SELECT ISNULL(MAX(Id), 0) + 1 FROM Customers";
            command.CommandType = CommandType.Text;
            
            connection.Open();
            int id = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return id;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
        }
    }
}