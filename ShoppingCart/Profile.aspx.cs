using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart
{
    public partial class Profile : System.Web.UI.Page
    {
        private string _key = "Xc8UMKqfXDhrn8g7+IXfhIjg5SJ/K65lZmw+MkmCb3k=";
        private string _iv = "63x0XUT8YZYnPkoAeFXWlw==";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT Name, Address, Phone FROM Profiles WHERE Id = @id", connection);

                command.Parameters.AddWithValue("@id", Convert.ToInt32(Request.QueryString["id"]));

                connection.Open();
                IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    int nameLength = (int)reader.GetBytes(0, 0, null, 0, 0);
                    int addrLength = (int)reader.GetBytes(1, 0, null, 0, 0);
                    int phoneLength = (int)reader.GetBytes(2, 0, null, 0, 0);

                    byte[] nameData = new byte[nameLength];
                    byte[] addrData = new byte[addrLength];
                    byte[] phoneData = new byte[phoneLength];

                    reader.GetBytes(2, 0, nameData, 0, nameData.Length);
                    reader.GetBytes(1, 0, addrData, 0, addrData.Length);
                    reader.GetBytes(2, 0, phoneData, 0, phoneData.Length);

                    this.txtName.Text = Encoding.UTF8.GetString(this.Decrypt(nameData));
                    this.txtAddress.Text = Encoding.UTF8.GetString(this.Decrypt(addrData));
                    this.txtPhone.Text = Encoding.UTF8.GetString(this.Decrypt(phoneData));
                }

                reader.Close();
            }
        }

        protected void cmdOK_Click(object sender, EventArgs e)
        {
            SqlDataSource dsAccount = new SqlDataSource(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString,
                "SELECT UserId FROM Accounts WHERE Email = @email");
            dsAccount.SelectParameters.Add("email", HttpContext.Current.User.Identity.Name);
            dsAccount.DataSourceMode = SqlDataSourceMode.DataReader;

            int userId = 0;
            var reader = dsAccount.Select(DataSourceSelectArguments.Empty) as IDataReader;

            while (reader.Read())
            {
                userId = reader.GetInt32(0); 
            }

            if (userId < 1)
                return;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
            SqlCommand command = new SqlCommand("INSERT INTO Profiles VALUES (@id, @name, @address, @phone)", connection);

            command.Parameters.AddWithValue("@id", userId);
            command.Parameters.AddWithValue("@name", this.Encrypt(Encoding.UTF8.GetBytes(this.txtName.Text)));
            command.Parameters.AddWithValue("@address", this.Encrypt(Encoding.UTF8.GetBytes(this.txtAddress.Text)));
            command.Parameters.AddWithValue("@phone", this.Encrypt(Encoding.UTF8.GetBytes(this.txtPhone.Text)));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Response.Redirect(Request.RawUrl);
        }

        private byte[] Encrypt(byte[] Data)
        {
            AesCryptoServiceProvider encryptionProvider = new AesCryptoServiceProvider();
            encryptionProvider.Key = Convert.FromBase64String(this._key);
            encryptionProvider.IV = Convert.FromBase64String(this._iv);

            MemoryStream dataStream = new MemoryStream();
            CryptoStream stream = new CryptoStream(dataStream, encryptionProvider.CreateEncryptor(), CryptoStreamMode.Write);

            stream.Write(Data, 0, Data.Length);
            stream.FlushFinalBlock();

            byte[] encryptedData = dataStream.ToArray();
            dataStream.Close();
            return encryptedData;
        }

        private byte[] Decrypt(byte[] Data)
        {
            AesCryptoServiceProvider encryptionProvider = new AesCryptoServiceProvider();
            encryptionProvider.Key = Convert.FromBase64String(this._key);
            encryptionProvider.IV = Convert.FromBase64String(this._iv);

            MemoryStream dataStream = new MemoryStream(Data);
            CryptoStream stream = new CryptoStream(dataStream, encryptionProvider.CreateDecryptor(), CryptoStreamMode.Read);

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

            stream.Close();

            dataStream.Close();
            return data.ToArray();
        }
    }
}