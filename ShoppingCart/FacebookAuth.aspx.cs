using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class FacebookAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection querystring = HttpUtility.ParseQueryString(Request.Url.Query);

            var code = querystring["code"].Split('#')[0];

            WebClient client = new WebClient();
            var accessTokenMessage = client.DownloadString(
                string.Format(
                "https://graph.facebook.com/oauth/access_token?client_id=783805638345245" +
                "&redirect_uri=http://localhost:50182/FacebookAuthorize.aspx" +
                "&client_secret=fb205dd280f0c2488694aab640b4acc7" +
                "&code={0}", code));

            Response.Write(accessTokenMessage);
        }
    }
}