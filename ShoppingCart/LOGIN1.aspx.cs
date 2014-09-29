using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class LOGIN1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(
                "https://www.facebook.com/dialog/oauth?client_id=416932221778835&redirect_uri=http://localhost:50182/FacebookAuthorize.aspx");
        }
    }
}