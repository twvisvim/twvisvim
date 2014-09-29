<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerForm.aspx.cs" Inherits="ShoppingCart.CustomerForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>姓名：
            <asp:TextBox ID="txtName" runat="server" />
        </div>
        <div>手機：
            <asp:TextBox ID="txtCellphone" runat="server" />
        </div>
        <div>帳單地址：
            <asp:TextBox ID="txtBillAddress" runat="server" />
        </div>
        <div>寄送地址：
            <asp:TextBox ID="txtShipAddress" runat="server" />
        </div>
        <div>Email:
            <asp:TextBox ID="txtEmail" runat="server" />
        </div>
        <div>性別：
            <asp:DropDownList ID="cboGender" runat="server">
                <asp:ListItem Value="0">女性 (Female)</asp:ListItem>
                <asp:ListItem Value="1">男性 (Male)</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Button ID="cmdSave" runat="server" Text=" 儲存 " OnClick="cmdSave_Click" />
        </div>
    </div>
    </form>
</body>
</html>
