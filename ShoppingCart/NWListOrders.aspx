<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWListOrders.aspx.cs" Inherits="ShoppingCart.NWListOrders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataPager ID="DataPager2" runat="server" PageSize="15" PagedControlID="lvOrders">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link" 
                    ShowFirstPageButton="True" 
                    ShowNextPageButton="False" 
                    ShowPreviousPageButton="True"
                    ShowLastPageButton="false" />
                <asp:NumericPagerField ButtonCount="4" NextPageText="(More)" PreviousPageText="(LESS)" />
                <asp:NextPreviousPagerField 
                    ButtonType="Button" 
                    ShowLastPageButton="True" 
                    ShowNextPageButton="True" 
                    ShowPreviousPageButton="False"
                    ShowFirstPageButton="False"  />
            </Fields>
        </asp:DataPager>
        <asp:ListView ID="lvOrders" runat="server" DataSourceID="OrderDS">
            <LayoutTemplate>
                <ul>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li><%# Eval("OrderID") + ":" +  Eval("CompanyName") %></li>
            </ItemTemplate>
        </asp:ListView>
    </div>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvOrders">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link" 
                    ShowFirstPageButton="True" 
                    ShowNextPageButton="False" 
                    ShowPreviousPageButton="True"
                    ShowLastPageButton="false" />
                <asp:NumericPagerField ButtonCount="4" NextPageText="(More)" PreviousPageText="(LESS)" />
                <asp:NextPreviousPagerField 
                    ButtonType="Button" 
                    ShowLastPageButton="True" 
                    ShowNextPageButton="True" 
                    ShowPreviousPageButton="False"
                    ShowFirstPageButton="False"  />
                </Fields>
        </asp:DataPager>
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
    </form>
</body>
</html>
