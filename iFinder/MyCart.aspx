<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="MyCart.aspx.cs" Inherits="MyCart" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="items">
    <div id="page">

        <body>

                <div class="container">
                    <h1>
                        <asp:Label ID="Label1" runat="server" Text="Shopping Cart"></asp:Label>
                    </h1>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:history.go(-1);">&lt;Go back   </asp:HyperLink>
                     or 
                    <a href="Default.aspx"> Search for a new item</a>
 
                    <br /><br />
                    <asp:GridView runat="server" ID="gvMyCart" AutoGenerateColumns="False" EmptyDataText="There is nothing in your shopping cart." GridLines="None" Width="100%" CellPadding="5" ShowFooter="True"  Height="340px" DataSourceID="SqlDataSource1" DataKeyNames="Id"  OnRowDeleting="gvMyCart_RowDeleting1" OnSelectedIndexChanged="gvMyCart_SelectedIndexChanged">
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
                            <asp:BoundField DataField="Id" HeaderText="ProductID" SortExpression="Id" >
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
                                     <asp:TextBox ID="qbox" OnTextChanged="Qchanged" runat="server" Text='<%# Eval("Quantity") %>' ></asp:TextBox>                                
                                 </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Left" />
                                 <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                            <asp:BoundField DataField="Price" HeaderText="Price per Unit" SortExpression="Price" >
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
 
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT  Id, UserName, ProductID, Quantity, Price, ProductName, Price * Quantity AS Total FROM MyCart WHERE (UserName = @userName)" DeleteCommand="DELETE FROM MyCart WHERE Id = @id" OldValuesParameterFormatString="original_{0}">
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





<asp:Content ID="Content3" runat="server" contentplaceholderid="login_asp">
    <asp:Label ID="message_label" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="user_label" runat="server" Text="UserName:   "></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="userName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="password_label" runat="server" Text="    Password:   "></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="passWord" runat="server" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="logged_in" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="bLogout" runat="server" OnClick="bLogout_Click" Text="Logout" />
    &nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="iForgotPass" runat="server" NavigateUrl="~/ForgotPassword.aspx">Forgot Password?</asp:HyperLink>
      <asp:HyperLink ID="CartLink" runat="server" NavigateUrl="~/MyCart.aspx">My Cart</asp:HyperLink>
      &nbsp;&nbsp;&nbsp;
      <asp:HyperLink ID="account_settings" runat="server" NavigateUrl="~/Account/AccountSettings.aspx" >Account Settings</asp:HyperLink>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)">
        <SelectParameters>
            <asp:ControlParameter ControlID="userName" DefaultValue="Anonymous" Name="UserName" PropertyName="Text" Type="String" />
        </SelectParameters>

    </asp:SqlDataSource>
&nbsp;
</asp:Content>






