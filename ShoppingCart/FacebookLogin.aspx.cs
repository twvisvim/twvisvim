using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class FacebookLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdFacebookLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(
                "https://www.facebook.com/dialog/oauth?" +
                "client_id=783805638345245" +
                "&redirect_uri=http://localhost:50182/FacebookAuthorize.aspx" +
                "&scope=email,user_friends");
        }

        protected void cmdGoogleLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(
                "https://accounts.google.com/o/oauth2/auth?" +
                "client_id=311156891927-3pe38gnlbc268mqb18pj2761o6jmhfhf.apps.googleusercontent.com" +
                "&response_type=code" +
                "&scope=openid%20email" +
                "&redirect_uri=http://localhost:50182/GoogleAuthorize.aspx" +
                "&state=12345");
        }

        protected void cmdRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }

        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            string email = null;
            string password = this.txtPassword.Text;
            int userId;
            string userName = null;
            byte[] pwd = null, salt = null;
            byte[] pwdOrigin = Encoding.UTF8.GetBytes(this.txtPassword.Text);
            
            this.AccountDS.SelectParameters["username"].DefaultValue = this.txtUsername.Text;

            var reader = this.AccountDS.Select(DataSourceSelectArguments.Empty) as IDataReader;
            bool hasAccount = false; // default: account is not found.

            while (reader.Read())
            {
                // confirm account found.
                hasAccount = true;

                userId = reader.GetInt32(0);
                userName = reader.GetString(1);
                email = reader.GetString(4);

                int pwdLength = (int)reader.GetBytes(2, 0, null, 0, 0);
                int saltLength = (int)reader.GetBytes(3, 0, null, 0, 0);

                pwd = new byte[pwdLength];
                salt = new byte[saltLength];

                reader.GetBytes(2, 0, pwd, 0, pwd.Length);
                reader.GetBytes(3, 0, salt, 0, salt.Length);
            }

            reader.Close();

            if (!hasAccount)
            {
                this.lblLoginError.Text = "Username or password is incorrect.";
                return;
            }

            byte[] pwdsalt = new byte[pwdOrigin.Length + salt.Length];

            System.Buffer.BlockCopy(pwdOrigin, 0, pwdsalt, 0, pwdOrigin.Length);
            System.Buffer.BlockCopy(salt, 0, pwdsalt, pwdOrigin.Length, salt.Length);

            // Generate password hashed.
            SHA384CryptoServiceProvider passwordGenerator = new SHA384CryptoServiceProvider();
            byte[] pwdHashed = passwordGenerator.ComputeHash(pwdsalt);

            if (pwd.Length != pwdHashed.Length)
                this.lblLoginError.Text = "Username or password is incorrect.";

            for (int i = 0; i < pwd.Length; i++)
            {
                if (pwd[i] != pwdHashed[i])
                {
                    this.lblLoginError.Text = "Username or password is incorrect.";
                    return;
                }
            }

            // password check passed.
            FormsAuthentication.RedirectFromLoginPage(this.txtUsername.Text, false);
        }
    }
}