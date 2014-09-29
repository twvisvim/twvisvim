<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWProduct2.aspx.cs" Inherits="ShoppingCart.NWProduct2" %>

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
        <asp:Repeater ID="repCategory" runat="server" DataSourceID="CategoryDS" OnItemDataBound="repCategory_ItemDataBound">
            <ItemTemplate>
                <h3><%# Eval("CategoryName") %></h3>   
                        <div>             
                <asp:Repeater ID="repProduct" runat="server">
                    <ItemTemplate>
                        <ul>
                            <li><%# Eval("ProductName") %> : 
                        <%# Convert.ToDouble(Eval("UnitPrice")).ToString("$###,##0.00")  %></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
                            </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
        <asp:SqlDataSource ID="CategoryDS" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" 
            SelectCommand="SELECT * FROM [Categories]" >
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="ProductDS" runat="server"
            ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" 
            SelectCommand="SELECT * FROM [Products] WHERE CategoryID = @id">
        </asp:SqlDataSource>
    </form>
  <script src="http://code.jquery.com/jquery-1.10.2.js"></script>
  <script src="http://code.jquery.com/ui/1.11.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $(document.getElementById("menu")).accordion({
                collapsible: true
            });
        });
    </script>
</body>
</html>
