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
                    <asp:GridView runat="server" ID="gvMyCart" AutoGenerateColumns="False" EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" CellPadding="5" ShowFooter="True"  Height="340px" DataSourceID="SqlDataSource1">
                        <HeaderStyle HorizontalAlign="Left" BackColor="#3D7169" ForeColor="#FFFFFF" />
                        <FooterStyle HorizontalAlign="Right" BackColor="#6C6B66" ForeColor="#FFFFFF" />
                        <AlternatingRowStyle BackColor="#F8F8F8" />
                        <Columns>
 
                            <asp:BoundField DataField="ProductID" HeaderText="ProductID" HeaderStyle-HorizontalAlign="Left" SortExpression="ProductID" ItemStyle-HorizontalAlign="Left" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" SortExpression="Quantity" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                <asp:TextBox ID="TxtFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
 
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [MyCart]" DeleteCommand="DELETE FROM [MyCart] WHERE [ProductID] = @original_ProductID AND (([Id] = @original_Id) OR ([Id] IS NULL AND @original_Id IS NULL)) AND (([UserName] = @original_UserName) OR ([UserName] IS NULL AND @original_UserName IS NULL)) AND (([Quantity] = @original_Quantity) OR ([Quantity] IS NULL AND @original_Quantity IS NULL)) AND (([Price] = @original_Price) OR ([Price] IS NULL AND @original_Price IS NULL)) AND (([ProductName] = @original_ProductName) OR ([ProductName] IS NULL AND @original_ProductName IS NULL))" ConflictDetection="CompareAllValues" InsertCommand="INSERT INTO [MyCart] ([Id], [UserName], [ProductID], [Quantity], [Price], [ProductName]) VALUES (@Id, @UserName, @ProductID, @Quantity, @Price, @ProductName)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [MyCart] SET [Id] = @Id, [UserName] = @UserName, [Quantity] = @Quantity, [Price] = @Price, [ProductName] = @ProductName WHERE [ProductID] = @original_ProductID AND (([Id] = @original_Id) OR ([Id] IS NULL AND @original_Id IS NULL)) AND (([UserName] = @original_UserName) OR ([UserName] IS NULL AND @original_UserName IS NULL)) AND (([Quantity] = @original_Quantity) OR ([Quantity] IS NULL AND @original_Quantity IS NULL)) AND (([Price] = @original_Price) OR ([Price] IS NULL AND @original_Price IS NULL)) AND (([ProductName] = @original_ProductName) OR ([ProductName] IS NULL AND @original_ProductName IS NULL))">
                        <DeleteParameters>
                            <asp:Parameter Name="original_ProductID" Type="Int32" />
                            <asp:Parameter Name="original_Id" Type="Int32" />
                            <asp:Parameter Name="original_UserName" Type="String" />
                            <asp:Parameter Name="original_Quantity" Type="Int32" />
                            <asp:Parameter Name="original_Price" Type="Decimal" />
                            <asp:Parameter Name="original_ProductName" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                            <asp:Parameter Name="UserName" Type="String" />
                            <asp:Parameter Name="ProductID" Type="Int32" />
                            <asp:Parameter Name="Quantity" Type="Int32" />
                            <asp:Parameter Name="Price" Type="Decimal" />
                            <asp:Parameter Name="ProductName" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Id" Type="Int32" />
                            <asp:Parameter Name="UserName" Type="String" />
                            <asp:Parameter Name="Quantity" Type="Int32" />
                            <asp:Parameter Name="Price" Type="Decimal" />
                            <asp:Parameter Name="ProductName" Type="String" />
                            <asp:Parameter Name="original_ProductID" Type="Int32" />
                            <asp:Parameter Name="original_Id" Type="Int32" />
                            <asp:Parameter Name="original_UserName" Type="String" />
                            <asp:Parameter Name="original_Quantity" Type="Int32" />
                            <asp:Parameter Name="original_Price" Type="Decimal" />
                            <asp:Parameter Name="original_ProductName" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
 
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





