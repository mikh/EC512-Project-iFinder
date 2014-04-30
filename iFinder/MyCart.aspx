<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="MyCart.aspx.cs" Inherits="MyCart" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="items">
    <div id="page">

        <body>

                <div class="container">
                    <h1>Shopping Cart</h1>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:history.go(-1);">Back to Products   </asp:HyperLink>
                     or 
                    <a href="Default.aspx"> Search for a new item</a>
 
                    <br /><br />
                    <asp:GridView runat="server" ID="gvMyCart" AutoGenerateColumns="False" EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" CellPadding="5" ShowFooter="True"  Height="340px" OnSelectedIndexChanged="gvMyCart_SelectedIndexChanged" DataSourceID="SqlDataSource1">
                        <HeaderStyle HorizontalAlign="Left" BackColor="#3D7169" ForeColor="#FFFFFF" />
                        <FooterStyle HorizontalAlign="Right" BackColor="#6C6B66" ForeColor="#FFFFFF" />
                        <AlternatingRowStyle BackColor="#F8F8F8" />
                        <Columns>
 
                            <asp:BoundField DataField="ProductName" HeaderText="ProductName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" SortExpression="ProductName" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
 
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
 
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" SortExpression="Quantity" ItemStyle-HorizontalAlign="Left" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Price" HeaderText="Price" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" SortExpression="Price" >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Total Price" ReadOnly="True" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" DataField="Price" >
<HeaderStyle HorizontalAlign="Right"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
 
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT [ProductID], [Quantity], [Price], [ProductName] FROM [MyCart]" DeleteCommand="DELETE FROM MyCart"></asp:SqlDataSource>
 
                    <br />
                    <asp:Button runat="server" ID="bUpdateCart" Text="Update Cart[Debug CLEAR]" OnClick="bUpdateCart_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="bBuyItems" runat="server" Text="Buy" OnClick="bBuyItems_Click" />
                </div>
        </body>
    </div>
</asp:Content>




<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .container {
            height: 519px;
        }
    </style>
</asp:Content>





