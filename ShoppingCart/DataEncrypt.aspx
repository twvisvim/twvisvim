<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataEncrypt.aspx.cs" Inherits="ShoppingCart.DataEncrypt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3"><h2>加解密</h2></div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">明文:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtPlainText" TextMode="MultiLine" Width="500px" Height="200px" CssClass="form-control" runat="server" /></div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:Button ID="cmdEncrypt" runat="server" Text=" 加密 " OnClick="cmdEncrypt_Click" /></div>
            </div>
            <div class="row">
                <div class="col-md-3">密文:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtCipherText" TextMode="MultiLine" Width="500px" Height="200px" CssClass="form-control" runat="server" /></div>
            </div>            
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:Button ID="cmdDecrypt" runat="server" Text=" 解密 " OnClick="cmdDecrypt_Click" /></div>
            </div>
            <div class="row">
                <div class="col-md-3"> 
                </div>
            </div>
        </div>
    </form>
</body>
</html>
