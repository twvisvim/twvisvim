using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    /// <summary>
    /// NWEmployeePhoto 的摘要描述
    /// </summary>
    public class NWEmployeePhoto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["NORTHWNDConnectionString"].ConnectionString);
            var command = new SqlCommand("SELECT Photo FROM Employees WHERE EmployeeID = @id", connection);
            command.Parameters.AddWithValue("@id", Convert.ToInt32(context.Request.QueryString["id"]));

            connection.Open();
            var reader = command.ExecuteReader();
            List<byte> imageData = new List<byte>();

            while (reader.Read())
            {
                // 設立緩衝區
                byte[] buffer = new byte[8192];

                // 取得資料總長度
                int length = (int)reader.GetBytes(0, 0, null, 0, 0);
                // 已讀取的總長度
                int readedTotalLength = 0;
                // 已讀取的資料量
                int readed = 0;

                do
                {
                    buffer = new byte[8192];
                    // 讀取資料 (必須略過OLE標頭，即前78 bytes)
                    readed = (int)reader.GetBytes(0, 78 + readedTotalLength, buffer, 0, buffer.Length);
                    // 加計總長度
                    readedTotalLength += readed;

                    // 判斷是否已讀完
                    if (readed == 0)
                        break;
                    else if (readedTotalLength == imageData.Count) // 已讀長度是否與總長度相同?
                    {
                        imageData.AddRange(buffer);
                        break;
                    }
                    else
                        imageData.AddRange(buffer);
                }
                while (readed > 0);
            }

            reader.Close();
            connection.Close();

            context.Response.BinaryWrite(imageData.ToArray());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}