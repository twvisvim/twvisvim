using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class GetFacebookProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var client = new WebClient();
            Response.Write(client.DownloadString("https://graph.facebook.com/me?access_token=CAALI3filgh0BAEvZCcCMM2dg05HLmJRzpP1ZCmzSQ9A3FXMqeQubuaYWNY1aA2zzNCnJHz9Y6ipuyWxWlLWrDP7ZAApkqYZBePjvbNH5EI8ynrTGboE99uQlu4mrZCg3owNAhxfhFfUODkjp0GOIVbzHWq3PZBaZA2BrkniC9qxOFmzEH87kHl30YBdyIKWu2oGTZBqSJ9vQR8UaUii08zpa"));
        }
    }
}