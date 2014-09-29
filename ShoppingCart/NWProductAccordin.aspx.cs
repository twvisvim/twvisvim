using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class NWProductAccordin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (e.Item.DataItem as DataRowView).Row;
                var repeater = e.Item.FindControl("Repeater2") as Repeater;

                this.ProductDS.SelectParameters["CategoryID"].DefaultValue = row["CategoryID"].ToString();
                repeater.DataSource = this.ProductDS.Select(new DataSourceSelectArguments());
                repeater.DataBind();
            }
        }

        protected void ProductDS_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
        }
    }
}