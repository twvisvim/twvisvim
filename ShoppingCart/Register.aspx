<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ShoppingCart.Register" %>

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
                <div class="col-md-3"><h2>Register</h2></div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">Username:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtUserName" CssClass="form-control" runat="server" /></div>
            </div>
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">Password:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password" /></div>
            </div>            
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">Confirm Password:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtConfirmPassword" CssClass="form-control" runat="server" TextMode="Password" /></div>
            </div>            
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3">Email:</div>
            </div>
            <div class="row">
                <div class="col-md-3"><asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" /></div>
            </div>            
            <div class="row">
                <div class="col-md-3">&nbsp;</div>
            </div>
            <div class="row">
                <div class="col-md-3"> 
                    <asp:Button ID="cmdOK" runat="server" CssClass="btn btn-default btn-block" Text=" OK " OnClick="cmdOK_Click" />
                </div>
            </div>
        </div>
        <asp:SqlDataSource ID="AccountDS" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
            InsertCommand="INSERT INTO Accounts (Username, Password, Salt, Email) VALUES (@name, @pwd, @salt, @email)" 
            SelectCommand="SELECT UserId FROM Accounts" OnInserted="AccountDS_Inserted" OnInserting="AccountDS_Inserting">
            <InsertParameters>
                <asp:Parameter Name="name" />
                <asp:Parameter Name="pwd" />
                <asp:Parameter Name="salt" />
                <asp:Parameter Name="email" />
            </InsertParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
