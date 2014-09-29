using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class DataEncrypt : System.Web.UI.Page
    {
        private string _key = "37TUsn3BlBqtiC9hGY7N7JOjNJwTNKOQZf8toLRpWpw=";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdEncrypt_Click(object sender, EventArgs e)
        {
            AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
            provider.Key = Convert.FromBase64String(this._key);
            provider.GenerateIV();

            Session["IV"] = provider.IV;
            
            MemoryStream dataStream = new MemoryStream();
            CryptoStream stream = new CryptoStream(
                dataStream,
                provider.CreateEncryptor(),
                CryptoStreamMode.Write);

            byte[] plainText = Encoding.UTF8.GetBytes(this.txtPlainText.Text);
            stream.Write(plainText, 0, plainText.Length);
            stream.FlushFinalBlock();

            this.txtCipherText.Text = Convert.ToBase64String(dataStream.ToArray());
            stream.Close();
            this.txtPlainText.Text = "";
        }

        protected void cmdDecrypt_Click(object sender, EventArgs e)
        {
            AesCryptoServiceProvider provider = new AesCryptoServiceProvider();
            provider.Key = Convert.FromBase64String(this._key);
            provider.IV = (byte[])Session["IV"];

            byte[] cipherText = Convert.FromBase64String(this.txtCipherText.Text);
            MemoryStream dataStream = new MemoryStream(cipherText);
            CryptoStream stream = new CryptoStream(
                dataStream,
                provider.CreateDecryptor(),
                CryptoStreamMode.Read);

            List<byte> data = new List<byte>();
            int dataLoaded = 0;

            do
            {
                byte[] buffer = new byte[4096];
                dataLoaded = stream.Read(buffer, 0, buffer.Length);

                if (dataLoaded == 0)
                    break;
                else if (dataLoaded < buffer.Length)
                {
                    for (int i = 0; i < dataLoaded; i++)
                        data.Add(buffer[i]);
                }
                else
                    data.AddRange(buffer);
            }
            while (dataLoaded > 0);

            this.txtPlainText.Text = Encoding.UTF8.GetString(data.ToArray());
            stream.Close();
        }
    }
}