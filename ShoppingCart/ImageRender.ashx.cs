using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingCart
{
    /// <summary>
    /// ImageRender 的摘要描述
    /// </summary>
    public class ImageRender : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["CartDb"].ConnectionString);
            SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Body FROM ProductImages WHERE ProductId = @id";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@id", Convert.ToInt32(context.Request.QueryString["id"]));

            connection.Open();

            // 加入 SequentialAccess 以要求 Reader 以順序方式存取欄位內容。
            var reader = command.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SequentialAccess);
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
                    // 讀取資料
                    readed = (int)reader.GetBytes(0, readedTotalLength, buffer, 0, buffer.Length);
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

            // 輸出影像資料到 HTTP Output Stream
            context.Response.BinaryWrite(imageData.ToArray());
            // 設定資料型別為 JPEG.
            context.Response.ContentType = "image/jpeg";
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}