using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class GetEncryptionKey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RNGCryptoServiceProvider keyGenerator = new RNGCryptoServiceProvider();
            byte[] data = new byte[32];
            keyGenerator.GetBytes(data);

            Response.Write(Convert.ToBase64String(data));
        }
    }
}