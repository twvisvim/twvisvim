using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShoppingCart.Models;
using ShoppingCart.Models.ProductDataSourceTableAdapters;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ShoppingCart
{
    public partial class ProductForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProductsTableAdapter adapter = new ProductsTableAdapter();
                adapter.Connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);

                var ds = new ProductDataSource();
                adapter.Fill(ds.Products);

                if (ds.Products.Any())
                {
                    var rowQuery = ds.Products.Where(
                        row => row.Id == Convert.ToInt32(Request.QueryString["id"]));

                    if (rowQuery.Any())
                    {
                        var row = rowQuery.First();

                        this.txtProductName.Text = row.Name;
                        this.txtDescription.Text = row.Description;
                        this.txtPrice.Text = row.Price.ToString();
                        this.txtCost.Text = row.Cost.ToString();
                        this.txtQty.Text = row.Qty.ToString();
                    }
                }
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            ProductsTableAdapter adapter = new ProductsTableAdapter();
            SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            adapter.Connection = connection;

            var ds = new ProductDataSource();
            adapter.Fill(ds.Products);
            var rowQuery = ds.Products.Where(
                row => row.Id == Convert.ToInt32(Request.QueryString["id"]));
            
            if (rowQuery.Any())
            {
                var row = rowQuery.First();

                row.Name = this.txtProductName.Text;
                row.Description = this.txtDescription.Text;
                row.Price = Convert.ToDecimal(this.txtPrice.Text);
                row.Cost = Convert.ToDecimal(this.txtCost.Text);
                row.Qty = Convert.ToInt32(this.txtQty.Text);
                row.DateCreated = row.DateLastMod = DateTime.Now;

                adapter.Update(ds);

                // 檢查圖檔是否已存在
                if (this.CheckImageIsExists(row.Id))
                {
                    // 已存在，執行更新
                    command.CommandText = "UPDATE ProductImages SET Name = @name, Body = @body WHERE ProductId = @id";
                    command.CommandType = CommandType.Text;

                    if (this.fileProductPicture.HasFile)
                    {
                        command.Parameters.AddWithValue("@id", row.Id);
                        command.Parameters.AddWithValue("@name", this.fileProductPicture.FileName);
                        command.Parameters.AddWithValue("@body", this.fileProductPicture.FileBytes);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else
                {
                    // 不存在，執行插入
                    command.CommandText = "INSERT INTO ProductImages (ProductId, Name, Body) VALUES (@id, @name, @body)";
                    command.CommandType = CommandType.Text;
                    
                    if (this.fileProductPicture.HasFile)
                    {
                        command.Parameters.AddWithValue("@id", row.Id);
                        command.Parameters.AddWithValue("@name", this.fileProductPicture.FileName);
                        command.Parameters.AddWithValue("@body", this.fileProductPicture.FileBytes);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            else
            {
                // 新增一個資料列
                var row = ds.Products.NewProductsRow();

                // 設定欄位值
                row.Name = this.txtProductName.Text;
                row.Description = this.txtDescription.Text;
                row.Price = Convert.ToDecimal(this.txtPrice.Text);
                row.Cost = Convert.ToDecimal(this.txtCost.Text);
                row.Qty = Convert.ToInt32(this.txtQty.Text);
                row.DateCreated = row.DateLastMod = DateTime.Now;

                ds.Products.AddProductsRow(row);

                // 新資料原本就不存在，故直接執行插入。
                command.CommandText = "INSERT INTO ProductImages (ProductId, Name, Body) VALUES (@id, @name, @body)";
                command.CommandType = CommandType.Text;
                
                adapter.Update(ds);
                
                if (this.fileProductPicture.HasFile)
                {
                    command.Parameters.AddWithValue("@id", row.Id);
                    command.Parameters.AddWithValue("@name", this.fileProductPicture.FileName);

                    // SQL Server 的 Provider 實作可以這樣做：
                    command.Parameters.AddWithValue("@body", this.fileProductPicture.FileBytes);

                    // 如果是別的資料庫要這樣做:
                    // var blobParameter = new SqlParameter();
                    // blobParameter.ParameterName = "@body";
                    // blobParameter.DbType = DbType.Binary;
                    // blobParameter.Size = this.fileProductPicture.FileBytes.Length;
                    // blobParameter.Value = fileProductPicture.FileBytes;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            // adapter.Insert("text", "text", 0, 0, 0, DateTime.Now, DateTime.Now);

            Response.Redirect("~/ProductList.aspx");
        }

        private bool CheckImageIsExists(int ProductId)
        {
            SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
            SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT COUNT(*) FROM ProductImages WHERE ProductId = @id";
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue("@id", ProductId);

            connection.Open();
            object result = command.ExecuteScalar();
            connection.Close();

            if (result == null || result == DBNull.Value)
                return false;
            else
                return (Convert.ToInt32(result) > 0);
        }
    }
}