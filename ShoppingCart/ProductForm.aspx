<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="ShoppingCart.ProductForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div>
          <p>名稱：<asp:TextBox ID="txtProductName" runat="server" /></p>
          <p>說明：<asp:TextBox ID="txtDescription" runat="server" /></p>
          <p>價格：<asp:TextBox ID="txtPrice" Width="100px" MaxLength="10" runat="server" /></p>
          <p>成本：<asp:TextBox ID="txtCost" Width="100px" MaxLength="10" runat="server" /></p>
          <p>數量：<asp:TextBox ID="txtQty" Width="100px" MaxLength="10" runat="server" /></p>
          <p>圖檔：<asp:FileUpload ID="fileProductPicture" runat="server" /></p>
      </div>
    <div>
        <asp:Button ID="cmdSave" runat="server" Text=" 儲存 " OnClick="cmdSave_Click" />
    </div>
    </form>
</body>
</html>
