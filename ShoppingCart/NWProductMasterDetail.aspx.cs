using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class NWProductMasterDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            this.ProductDS.SelectParameters["CategoryID"].DefaultValue = e.CommandArgument.ToString();
            this.Repeater2.DataBind();
        }
    }
}