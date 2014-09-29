<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWEmployeeList.aspx.cs" Inherits="ShoppingCart.NWEmployeeList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="EmployeeDS" ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" 
             SelectCommand="SELECT EmployeeID, FirstName, LastName, Title FROM Employees" runat="server"></asp:SqlDataSource>
        <asp:ListView ID="lvEmployees" runat="server" GroupItemCount="3" DataSourceID="EmployeeDS">
            <LayoutTemplate>
                <table>
                    <asp:PlaceHolder ID="groupPlaceholder" runat="server" />
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td>
                    <asp:Image ID="employeeImage" runat="server" ImageUrl='<%# "NWEmployeePhoto.ashx?id=" + Eval("EmployeeID") %>' /><br />
                    <%# Eval("FirstName") + " " + Eval("LastName") %><br />
                    <%# Eval("Title") %>
                </td>
            </ItemTemplate>
        </asp:ListView>

        <asp:DataList ID="dlEmployees" DataSourceID="EmployeeDS" runat="server" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal">
            <ItemTemplate>
                <asp:Image ID="employeeImage" runat="server" ImageUrl='<%# "NWEmployeePhoto.ashx?id=" + Eval("EmployeeID") %>' /><br />
                <%# string.Format("{0} {1}", Eval("FirstName"), Eval("LastName")) %><br />
                <%# Eval("Title") %>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
