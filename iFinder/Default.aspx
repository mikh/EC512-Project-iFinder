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
    <asp:Label ID="results_label" runat="server" Text=""></asp:Label><br /><br /><br />
    <asp:Repeater ID="results_repeater" runat="server" OnItemCommand="results_repeater_ItemCommand">
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
            </tr>
            <br />
            <br />
        </ItemTemplate>
        <FooterTemplate>
            </table><br />
        </FooterTemplate>
    </asp:Repeater>


</asp:Content>
