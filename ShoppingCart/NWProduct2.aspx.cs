using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class NWProduct2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void repCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var row = (e.Item.DataItem as DataRowView).Row;
            var repProduct = e.Item.FindControl("repProduct") as Repeater;

            this.ProductDS.SelectParameters.Clear();
            this.ProductDS.SelectParameters.Add("id", row["CategoryID"].ToString());
            repProduct.DataSource = this.ProductDS.Select(new DataSourceSelectArguments());
            repProduct.DataBind();            
        }
    }
}