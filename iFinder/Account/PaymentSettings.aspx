<%@ Page Language="C#" MasterPageFile="~/SuperMaster.master"  AutoEventWireup="true" CodeFile="PaymentSettings.aspx.cs" Inherits="Account_PaymentSettings" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
     <h1><asp:Label ID="Label1" runat="server" Text="Payment Options" /><br /></h1>
     Nothing to see here.<br />
     <br />
     <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/AccountSettings.aspx">&lt;Return to Account  Settings</asp:HyperLink>
        <br />
    <br />
    <br />
    <p>
    </p>
</asp:Content>


<asp:Content ID="Content2" runat="server" contentplaceholderid="login_asp">
    <asp:Label ID="message_label" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="logged_in" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="bLogout" runat="server" OnClick="bLogout_Click" Text="Logout" />
    &nbsp;&nbsp;&nbsp;
      <asp:HyperLink ID="CartLink" runat="server" NavigateUrl="~/MyCart.aspx">My Cart</asp:HyperLink>
      &nbsp;&nbsp;&nbsp;
      <asp:HyperLink ID="account_settings" runat="server" NavigateUrl="~/Account/AccountSettings.aspx" >Account Settings</asp:HyperLink>
&nbsp;
</asp:Content>



