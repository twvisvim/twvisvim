<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWProductMasterDetail.aspx.cs" Inherits="ShoppingCart.NWProductMasterDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
</head>
<body>
    <form id="form1" runat="server">
    <div id="menu">
    
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="CategoryDS"  OnItemCommand="Repeater1_ItemCommand">
            <ItemTemplate>
                <div><%# Eval("CategoryName") %> <asp:LinkButton ID="lnkViewProduct" runat="server" CommandArgument='<%# Eval("CategoryID") %>' Text="View Product" /></div>
            </ItemTemplate>
        </asp:Repeater>
    
        <asp:Repeater ID="Repeater2" runat="server" DataSourceID="ProductDS">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                    <li><%# Eval("ProductName") %>  
                        <%# Convert.ToDouble(Eval("UnitPrice")).ToString("$###,##0.00")  %></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
        <asp:SqlDataSource ID="CategoryDS" runat="server" 
            ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="ProductDS" runat="server" 
            ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" SelectCommand="SELECT * FROM [Products] WHERE ([CategoryID] = @CategoryID)">
            <SelectParameters>
                <asp:Parameter Name="CategoryID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
