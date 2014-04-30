<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SuperMaster.master" CodeFile="~/Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="head" runat="server">
</asp:Content>

  <asp:Content ID="Content_login" ContentPlaceHolderID="login_asp" runat="server">
      <asp:Label ID="message_label" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="user_label" runat="server" Text="UserName:   "></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="userName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="password_label" runat="server" Text="    Password:   "></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="passWord" runat="server" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="logged_in" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Login" runat="server" Text="Login" OnClick="Login_Click" />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Register" runat="server" Text="Register" OnClick="Register_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="bLogout" runat="server" OnClick="bLogout_Click" Text="Logout" />
    &nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="iForgotPass" runat="server" NavigateUrl="~/ForgotPassword.aspx">Forgot Password?</asp:HyperLink>&nbsp;&nbsp;&nbsp;
      <asp:HyperLink ID="account_settings" runat="server" NavigateUrl="~/Account/AccountSettings.aspx" >Account Settings</asp:HyperLink>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)">
        <SelectParameters>
            <asp:ControlParameter ControlID="UserName" Name="UserName" PropertyName="Text" Type="String" />
        </SelectParameters>

    </asp:SqlDataSource>
&nbsp;
</asp:Content>

<asp:Content ID="Content_search" ContentPlaceHolderID="search_asp" runat="server">
    Product Search: &nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="search_bar" runat="server" Width="446px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" />
</asp:Content>

<asp:Content ID="Content_results" ContentPlaceHolderID="items" runat="server">
    <div class="child_left" style="padding: 10px; margin: auto; float: left; width: 20%; background-color: #FFFFFF; font-size: 12px; text-align:left;">
                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
    <asp:Label ID="results_label" runat="server" Text=""></asp:Label><br /><br /><br />
    <div style="text-align:center; align-items:center;">
    <asp:Repeater ID="results_repeater" runat="server" OnItemCommand="results_repeater_ItemCommand">
        <HeaderTemplate>
            <table border="1" style="align-items:center; text-align:center; float:inherit;">
                <tr>
                    <td> <b>&nbsp;&nbsp;&nbsp;ID&nbsp;&nbsp;&nbsp; </b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Manufacturer&nbsp;&nbsp;&nbsp;</b> </td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Quantity &nbsp;&nbsp;&nbsp;</b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Availability&nbsp;&nbsp;&nbsp; </b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Price&nbsp;&nbsp;&nbsp; </b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Resistance in Ohms&nbsp;&nbsp;&nbsp; </b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Tolerance&nbsp;&nbsp;&nbsp; </b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Power Rating in Watts&nbsp;&nbsp;&nbsp; </b></td>
                    <td> <b>&nbsp;&nbsp;&nbsp;Add To Cart&nbsp;&nbsp;&nbsp;</b></td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>               
                <tr>
                    <td>
                        <%# ((List<String>)Container.DataItem)[0] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[1] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[2] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[3] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[4] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[8] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[9] %>
                    </td>
                    <td>
                        <%# ((List<String>)Container.DataItem)[10] %>
                    </td>

                    <td>
                        <asp:HyperLink ID="cart_hyper" runat="server" NavigateUrl="~/MyCart.aspx">Add To Cart<!--<asp:Button ID="add2cart" runat="server" Width="80px" Text="Add to Cart" CommandName="add2cart"/>--></asp:HyperLink>
                    </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table><br />
        </FooterTemplate>
    </asp:Repeater>
        </div>
    <asp:Image ID="ball_image" runat="server" />

</asp:Content>


