using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdOK_Click(object sender, EventArgs e)
        {
            this.AccountDS.Insert();
        }

        protected void AccountDS_Inserting(object sender, SqlDataSourceCommandEventArgs e)
        {
            // Generate salt for password hashing.
            RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider();
            byte[] salts = new byte[64];
            saltGenerator.GetBytes(salts);

            byte[] passwordOrigin = Encoding.UTF8.GetBytes(this.txtConfirmPassword.Text);
            byte[] pwdsalt = new byte[passwordOrigin.Length + salts.Length];

            System.Buffer.BlockCopy(passwordOrigin, 0, pwdsalt, 0, passwordOrigin.Length);
            System.Buffer.BlockCopy(salts, 0, pwdsalt, passwordOrigin.Length, salts.Length);

            // Generate password hashed.
            SHA384CryptoServiceProvider passwordGenerator = new SHA384CryptoServiceProvider();
            byte[] pwd = passwordGenerator.ComputeHash(pwdsalt);

            e.Command.Parameters["@name"].Value = this.txtUserName.Text;
            e.Command.Parameters["@pwd"].Value = pwd;
            e.Command.Parameters["@salt"].Value = salts;
            e.Command.Parameters["@email"].Value = this.txtEmail.Text;
        }

        protected void AccountDS_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {

            if (e.Exception != null)
                throw e.Exception;

            Response.Redirect("~/FacebookLogin.aspx");
        }
    }
}