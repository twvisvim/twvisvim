<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacebookLogin.aspx.cs" Inherits="ShoppingCart.FacebookLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title><!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-offset-1 col-md-4">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-3">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">Login with:</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="cmdFacebookLogin" runat="server" CssClass="btn btn-default btn-block" Text="Facebook Login" OnClick="cmdFacebookLogin_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Button ID="cmdGoogleLogin" runat="server" CssClass="btn btn-default btn-block" Text="Google Login" OnClick="cmdGoogleLogin_Click" />
                            </div>
                        </div>
                    </div>        
                </div>
                <div class="col-md-4 col-md-offset-1">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-3">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">Login with Account:</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">&nbsp;</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">Username:</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3"><asp:TextBox ID="txtUsername" CssClass="form-control" runat="server" /></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">Password:</div>
                        </div>
                        <div class="row">
                            <div class="col-md-3"><asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" TextMode="Password" /></div>
                        </div>            
                        <div class="row">
                            <div class="col-md-3"><asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Text="&nbsp;" /></div>
                        </div>
                        <div class="row">
                            <div class="col-md-3"> 
                                <asp:Button ID="cmdLogin" runat="server" CssClass="btn btn-default btn-block" Text="Login" OnClick="cmdLogin_Click" />
                                <asp:Button ID="cmdRegister" runat="server" CssClass="btn btn-default btn-block" Text="Register" OnClick="cmdRegister_Click" />
                            </div>
                        </div>
                    </div>
                 </div>
            </div>
        </div>
        <asp:SqlDataSource ID="AccountDS" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT [userId], [username], [password], [salt], [email] FROM [accounts] WHERE ([username] = @username)" DataSourceMode="DataReader">
            <SelectParameters>
                <asp:Parameter Name="Email" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
