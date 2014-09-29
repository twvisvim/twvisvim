<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ShoppingCart.Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>購物車有 <asp:Label ID="lblCartItems" runat="server" Text="0" /> 項商品<br /><asp:Button ID="cmdPlace" runat="server" Text="結帳" OnClick="cmdPlace_Click" style="height: 21px" /><asp:Button ID="cmdCancel" runat="server" Text="清空" OnClick="cmdCancel_Click" /></p>
        </div>
        <div>
        <asp:GridView ID="gvProducts" AutoGenerateColumns="false" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="gvProducts_RowCommand">
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
            <Columns>
                <asp:BoundField DataField="Id" />
                <asp:BoundField DataField="Caption" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="cmdAddToCart" runat="server" Text="加入購物車" CommandName="AddToCart" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></div>
    </form>
</body>
</html>
