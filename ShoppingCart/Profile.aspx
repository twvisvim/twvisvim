<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ShoppingCart.Profile" %>

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
                <div class="col-md-3"><h2>使用者設定檔</h2></div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">姓名:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtName" CssClass="form-control" runat="server" /></div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">地址:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" /></div>
            </div>            
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">電話:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtPhone" CssClass="form-control" runat="server" /></div>
            </div>            
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3"> 
                    <asp:Button ID="cmdOK" runat="server" CssClass="btn btn-default btn-block" Text=" 儲存 " OnClick="cmdOK_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
