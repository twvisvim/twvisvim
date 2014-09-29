<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="ShoppingCart.CustomerList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <asp:Button ID="cmdAddNew" runat="server" Text="新增..." OnClick="cmdAddNew_Click" />
        <br />
        <br />
        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="8" GridLines="Vertical" OnRowCommand="gvCustomers_RowCommand">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="姓名" />
                <asp:BoundField DataField="Cellphone" HeaderText="手機" />
                <asp:BoundField DataField="BillAddress" HeaderText="寄送地址" />
                <asp:BoundField DataField="ShipAddress" HeaderText="帳單地址" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:TemplateField HeaderText="性別">
                    <ItemTemplate>
                        <asp:Label ID="lblGender" runat="server"
                            Text='<%# (Convert.ToBoolean(Eval("Gender"))) ? "男" : "女" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="cmdUpdate" CommandArgument='<%# Eval("Id") %>' CommandName="CustomerUpdate" runat="server" Text="修改..." />
                        <asp:Button ID="cmdDelete" OnClientClick="return confirm('確定要刪除?')" CommandArgument='<%# Eval("Id") %>' CommandName="CustomerDelete" runat="server" Text=" 刪除 " />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
