<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="MyCart.aspx.cs" Inherits="MyCart" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="items">
    <div id="page">

        <body>

                <div class="container">
                    <h1>
                        <asp:Label ID="Label1" runat="server" Text="Shopping Cart"></asp:Label>
                    </h1>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:history.go(-1);">Back to Products   </asp:HyperLink>
                     or 
                    <a href="Default.aspx"> Search for a new item</a>
 
                    <br /><br />
                    <asp:GridView runat="server" ID="gvMyCart" AutoGenerateColumns="False" EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" CellPadding="5" ShowFooter="True"  Height="340px" DataSourceID="SqlDataSource1" DataKeyNames="Id" OnRowDeleted="gvMyCart_RowDeleted" OnRowDeleting="gvMyCart_RowDeleting1">
                        <HeaderStyle HorizontalAlign="Left" BackColor="#3D7169" ForeColor="#FFFFFF" />
                        <FooterStyle HorizontalAlign="Right" BackColor="#6C6B66" ForeColor="#FFFFFF" />
                        <AlternatingRowStyle BackColor="#F8F8F8" />
                        <Columns>
 
                            <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-HorizontalAlign="Left" SortExpression="Id" ItemStyle-HorizontalAlign="Left" InsertVisible="False" ReadOnly="True" Visible="False" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" HeaderText="UserName" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" SortExpression="UserName" Visible="False" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" HeaderText="ProductID" SortExpression="ID" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
<%--                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" >
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>--%>
                             <asp:TemplateField ShowHeader="False" HeaderText="Quantity">
                                 <ItemTemplate>
                                     <asp:TextBox ID="qbox" runat="server" Text='<%# Eval("Quantity") %>' ></asp:TextBox>                                
                                 </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                 <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                            <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Price" >
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:CommandField runat="server" ShowDeleteButton="True" DeleteText="Remove" EditText="Update Quantity    " />
 
                        </Columns>
                    </asp:GridView>
 
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT DISTINCT Id, UserName, ProductID, Quantity, Price, ProductName, Price * Quantity AS Total FROM MyCart WHERE (UserName = @userName)" DeleteCommand="DELETE FROM MyCart WHERE (Id = @id)" OldValuesParameterFormatString="original_{0}">
                        <DeleteParameters>
                            <asp:Parameter Name="id" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:Parameter Name="userName" />
                        </SelectParameters>
                    </asp:SqlDataSource>
 
                    <br />
                    <asp:Button runat="server" ID="bUpdateCart" Text="Update Cart" OnClick="bUpdateCart_Click" />
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





