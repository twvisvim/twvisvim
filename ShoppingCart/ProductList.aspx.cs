using ShoppingCart.Models;
using ShoppingCart.Models.ProductDataSourceTableAdapters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ProductsTableAdapter adapter = new ProductsTableAdapter();

                adapter.Connection = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);

                var ds = new Models.ProductDataSource();
                adapter.Fill(ds.Products);
                
                this.gvProductList.DataSource = ds.Products;
                this.gvProductList.DataBind();

                this.LoadStockQty();
            }
        }

        private void LoadStockQty()
        {
            ProductsTableAdapter adapter = new ProductsTableAdapter();

            adapter.Connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);

            var ds = new Models.ProductDataSource();
            adapter.Fill(ds.Products);

            //this.lblStockQty.Text = Convert.ToInt32(ds.Products.Compute("SUM(Qty)", null)).ToString();
            this.lblStockQty.Text = ds.Products.Sum(c => c.Qty).ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ProductForm.aspx");
        }

        protected void gvProductList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "ProductUpdate":
                    Response.Redirect("~/ProductForm.aspx?id=" + id.ToString());
                    break;

                case "ProductDelete":
                    
                    ProductsTableAdapter adapter = new ProductsTableAdapter();
                    adapter.Connection = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);

                    adapter.Connection.Open();
                    var deleteTrans = adapter.Connection.BeginTransaction();

                    var ds = new ProductDataSource();
                    adapter.Transaction = deleteTrans;
                    adapter.Fill(ds.Products);

                    if (ds.Products.Any())
                    {
                        try
                        {
                            var rowQuery = ds.Products.Where(
                                row => row.Id == id);

                            if (rowQuery.Any())
                            {
                                var row = rowQuery.First();
                                row.Delete();
                            }
                            
                            // submit delete state to database.
                            adapter.Update(ds);

                            var deleteImageCmd = adapter.Connection.CreateCommand();
                            deleteImageCmd.CommandText = "DELETE FROM ProductImages WHERE ProductId = @id";
                            deleteImageCmd.CommandType = CommandType.Text;
                            deleteImageCmd.Transaction = deleteTrans;
                            deleteImageCmd.Parameters.AddWithValue("@id", id);
                            deleteImageCmd.ExecuteNonQuery();

                            // call DELETE directly
                            // adapter.Delete(id);

                            deleteTrans.Commit();
                        }
                        catch (SqlException)
                        {
                            deleteTrans.Rollback();
                            throw;
                        }
                        finally
                        {
                            adapter.Connection.Close();
                        }
                    }

                    // 接受刪除的變更，以利重新繫結資料到 GridView
                    ds.AcceptChanges();

                    this.gvProductList.DataSource = ds.Products;
                    this.gvProductList.DataBind();

                    this.LoadStockQty();

                    break;
            }
        }
    }
}