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
    public class CartRepository : IRepository<CartItem>
    {
        private const string SessionId = "123";

        public void Create(CartItem Model)
        {
            var connection = this.GetConnection();
            var cmd = connection.CreateCommand();

            cmd.CommandText = "INSERT INTO CartItems VALUES (@cartId, @id, @qty)";
            cmd.Parameters.AddWithValue("@cartId", SessionId);
            cmd.Parameters.AddWithValue("@id", Model.Id);
            cmd.Parameters.AddWithValue("@qty", Model.Qty);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(CartItem Model)
        {
            var connection = this.GetConnection();
            var cmd = connection.CreateCommand();

            cmd.CommandText = "UPDATE CartItems SET Qty = @qty WHERE CartId = @cartId AND Id = @id";
            cmd.Parameters.AddWithValue("@cartId", SessionId);
            cmd.Parameters.AddWithValue("@id", Model.Id);
            cmd.Parameters.AddWithValue("@qty", Model.Qty);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Delete(CartItem Model)
        {
            var connection = this.GetConnection();
            var cmd = connection.CreateCommand();

            cmd.CommandText = "DELETE FROM CartItems WHERE CartId = @cartId AND Id = @id";
            cmd.Parameters.AddWithValue("@cartId", SessionId);
            cmd.Parameters.AddWithValue("@id", Model.Id);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public IEnumerable<CartItem> Select()
        {
            var connection = this.GetConnection();
            var cmd = connection.CreateCommand();

            cmd.CommandText = "SELECT * FROM CartItems WHERE CartId = @cartId";
            cmd.Parameters.AddWithValue("@cartId", SessionId);

            connection.Open();
            var reader = cmd.ExecuteReader();
            var items = new List<CartItem>();

            while (reader.Read())
            {
                items.Add(new CartItem()
                    {
                        Id = reader.GetValue(1).ToString(),
                        Qty = Convert.ToInt32(reader.GetValue(2))
                    });
            }

            connection.Close();
            return items;
        }

        public int GetLatestIdentity()
        {
            return 0;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
        }
    }
}