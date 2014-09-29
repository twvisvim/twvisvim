<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWOrderMasterDetail.aspx.cs" Inherits="ShoppingCart.NWOrderMasterDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="gvOrderList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" Caption="北風資料庫訂單列表" CellPadding="4" DataKeyNames="OrderID" DataSourceID="OrderDS" OnRowDataBound="gvOrderList_RowDataBound" OnPageIndexChanging="gvOrderList_PageIndexChanging" OnRowCommand="gvOrderList_RowCommand" OnPreRender="gvOrderList_PreRender" OnDataBound="gvOrderList_DataBound">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="OrderID" InsertVisible="False" ReadOnly="True" SortExpression="OrderID" />
                <asp:TemplateField HeaderText="Seller and Buyer">
                    <ItemTemplate>
                        <p>Seller: <%# string.Format("{0} {1}", Eval("FirstName"), Eval("LastName"))  %></p>
                        <p>Buyer: <%# Eval("CompanyName") %></p>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" DataFormatString="{0:yyyy/MM/dd}" SortExpression="OrderDate" />
                <asp:BoundField DataField="RequiredDate" HeaderText="RequiredDate" DataFormatString="{0:yyyy/MM/dd}" SortExpression="RequiredDate" />
                <asp:BoundField DataField="ShippedDate" HeaderText="ShippedDate" DataFormatString="{0:yyyy/MM/dd}" SortExpression="ShippedDate" />
                <asp:BoundField DataField="Freight" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" HeaderText="Freight" SortExpression="Freight" />
                <asp:TemplateField HeaderText="Ship Information">
                    <ItemStyle />
                    <ItemTemplate>
                        <div>
                            <p><%# Eval("ShipName") %></p>
                            <p><%# Eval("ShipAddress") %><br />
                            <%# Eval("ShipCity") %><br />
                            <%# Eval("ShipRegion") %><br />
                            <%# Eval("ShipPostalCode") %><br />
                            <%# Eval("ShipCountry") %>
                                </p>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" HeaderText="Amount" SortExpression="Amount" />
            </Columns>
            <PagerTemplate>
                <asp:Button ID="cmdFirstPage" runat="server" CommandName="FirstPage" Text="<<" />
                <asp:Button ID="cmdPreviousPage" runat="server" CommandName="PreviousPage" Text="<" />
                Page <asp:Label ID="lblPageIndex" runat="server" /> / <asp:Label ID="lblPageCount" runat="server" />
                Move To: <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="cboMovePage_SelectedIndexChanged" ID="cboMovePage" runat="server" />
                Size: <asp:DropDownList AutoPostBack="true" ID="cboPageSize" runat="server" OnSelectedIndexChanged="cboPageSize_SelectedIndexChanged">
                         <asp:ListItem Value="10">10</asp:ListItem>
                         <asp:ListItem Value="20">20</asp:ListItem>
                         <asp:ListItem Value="30">30</asp:ListItem>
                         <asp:ListItem Value="40">40</asp:ListItem>
                         <asp:ListItem Value="50">50</asp:ListItem>
                      </asp:DropDownList>
                Size: <asp:TextBox ID="txtPageSize" runat="server" Text="10" Width="30px" MaxLength="5" /><asp:Button ID="cmdSetPageSize" runat="server" CommandName="SetPageSize" Text="Set" />
                <asp:Button ID="cmdNextPage" runat="server" CommandName="NextPage" Text=">" />
                <asp:Button ID="cmdLastPage" runat="server" CommandName="LastPage" Text=">>" />
            </PagerTemplate>            
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
        <asp:SqlDataSource ID="OrderDS" runat="server" ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" 
            SelectCommand=" SELECT TOP 100 o.OrderID, o.OrderDate, o.RequiredDate, o.ShippedDate, o.ShipVia, 
                                           o.Freight, o.ShipName, o.ShipAddress, o.ShipRegion, o.ShipCity, o.ShipCountry,
			                               o.ShipPostalCode, c.CompanyName, e.FirstName, e.LastName, s.CompanyName,
			                               (SELECT ISNULL(SUM(UnitPrice*Quantity*Discount), 0) FROM [Order Details] WHERE OrderID = o.OrderID) AS Amount 
                            FROM Orders o INNER JOIN Customers c ON o.CustomerID = c.CustomerID
                                          INNER JOIN Employees e ON o.EmployeeID = e.EmployeeID
			                              INNER JOIN Suppliers s ON o.ShipVia = s.SupplierID
                            ORDER BY o.OrderDate DESC">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="OrderDetailDS" runat="server" ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" 
            SelectCommand="SELECT od.OrderID, p.ProductName, od.UnitPrice, od.Quantity, 
                                  od.UnitPrice * od.Quantity AS Amount
                           FROM   [Order Details] od INNER JOIN Products p ON od.ProductID = p.ProductID
                           WHERE  od.OrderID = @OrderID">
            <SelectParameters>
                <asp:Parameter Name="OrderID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
