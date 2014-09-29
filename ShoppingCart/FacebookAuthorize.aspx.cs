using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class FacebookAuthorize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request.QueryString["code"].Split('#')[0];

            // get facebook access token.
            var client = new WebClient();
            string accessToken = client.DownloadString(
                string.Format(
                "https://graph.facebook.com/oauth/access_token?client_id=783805638345245" +
                "&redirect_uri=http://localhost:50182/FacebookAuthorize.aspx" +
                "&client_secret=fb205dd280f0c2488694aab640b4acc7" +
                "&code={0}", code));

            NameValueCollection queryStringItems = HttpUtility.ParseQueryString(accessToken);
            
            // get user profile.
            string profile = client.DownloadString("https://graph.facebook.com/me?access_token=" + queryStringItems["access_token"]);
            var o = JObject.Parse(profile);
            
            // check user has registered.
            SqlDataSource ds = new SqlDataSource(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString, 
                "SELECT UserId FROM Accounts WHERE Email = @email");
            ds.SelectParameters.Add("email", (o.SelectToken("email") as JValue).Value<string>());
            ds.DataSourceMode = SqlDataSourceMode.DataReader;

            var reader = ds.Select(new DataSourceSelectArguments()) as IDataReader;
            bool exists = reader.Read();

            reader.Close();

            if (exists)
            {
                // do authorization process.
                FormsAuthentication.RedirectFromLoginPage((o.SelectToken("email") as JValue).Value<string>(), false);
                Response.Redirect("~/SecurePage.aspx");
            }
            else
            {
                Session.Add("FbJson", profile);
                // redirect to registration page.
                Response.Redirect("~/Register.aspx");
            }
        }
    }
}