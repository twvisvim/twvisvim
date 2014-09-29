<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NWEmployeeList2.aspx.cs" Inherits="ShoppingCart.NWEmployeeList2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ListView ID="ListView1" GroupItemCount="3" runat="server" DataSourceID="SqlDataSource1">
            <LayoutTemplate>
                <table>
                    <asp:PlaceHolder ID="groupPlaceHolder" runat="server" />
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td>
                    <asp:Image ID="imageEmployee" ImageUrl='<%# string.Format("NWEmployeePhoto.ashx?id={0}", Eval("EmployeeID")) %>' runat="server" />
                    <br />
                    <%# string.Format("{0} {1}", Eval("FirstName"), Eval("LastName")) %>
                </td>
            </ItemTemplate>
        </asp:ListView>
    <div>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NORTHWNDConnectionString %>" SelectCommand="SELECT [EmployeeID], [LastName], [FirstName], [Title] FROM [Employees]"></asp:SqlDataSource>
    </form>
</body>
</html>
