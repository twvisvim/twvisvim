<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWProduct.aspx.cs" Inherits="ShoppingCart.NWProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rep" runat="server" DataSourceID="SqlDataSource1">
            <HeaderTemplate>
                <div>
            </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <span> <%# Eval("ProductName") %>  
                        <%# Convert.ToDouble(Eval("UnitPrice")).ToString("$###,##0.00")  %></span>
                </div>                       
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div style="background-color: #0000ff; color: #ffffff;">
                    <span> <%# Eval("ProductName") %>  
                        <%# Convert.ToDouble(Eval("UnitPrice")).ToString("$###,##0.00")  %></span>
                </div>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" 
            SelectCommand="SELECT * FROM [Products]" 
            InsertCommand="" 
            UpdateCommand="" 
            DeleteCommand="">
            <SelectParameters>

            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
