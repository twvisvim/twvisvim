<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="ShoppingCart.ProductList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="新增資料" OnClick="Button1_Click" />
        <br />
        總庫存量：<asp:Label ID="lblStockQty" runat="server" />
        <br />
        <asp:GridView ID="gvProductList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="gvProductList_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="名稱" />
                <asp:BoundField DataField="Description" HeaderText="說明" />
                <asp:BoundField DataField="Price" HeaderText="價格/成本" />
                <asp:BoundField DataField="Qty" HeaderText="庫存" />
                <asp:TemplateField HeaderText="圖片">
                    <ItemTemplate>
                        <asp:Image ID="productPicture" runat="server" ImageUrl='<%# "~/ImageRender.ashx?id=" + Eval("Id").ToString() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="cmdUpdate" CommandArgument='<%# Eval("Id") %>' CommandName="ProductUpdate" runat="server" Text="修改..." />
                        <asp:Button ID="cmdDelete" OnClientClick="return confirm('確定要刪除?')" CommandArgument='<%# Eval("Id") %>' CommandName="ProductDelete" runat="server" Text=" 刪除 " />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
