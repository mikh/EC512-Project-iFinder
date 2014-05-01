<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="AccountSettings.aspx.cs" Inherits="Account_AccountSettings" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div dir="auto" style="float: none">
            <h1><asp:Label ID="Label1" runat="server" Text="Account Settings" /><br /></h1>
&nbsp;<div style="height: 178px">
            <div dir="ltr" style="float: left; width: 481px; right: 6px; height: 105px;">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/UpdateEmail.aspx">Update Email Address</asp:HyperLink>
            <br />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Account/UpdatePassword.aspx">Change Password</asp:HyperLink>
                <br />
            </div>
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Account/PaymentSettings.aspx">Payment Settings</asp:HyperLink>
            <br />
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Account/UpdateAddress.aspx">Update Address Information</asp:HyperLink>
            <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <asp:Label ID="Labelcemail" runat="server"></asp:Label>
                <br />
                <br />
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSource1" Height="50px" OnPageIndexChanging="DetailsView1_PageIndexChanging" Width="243px">
                    <Fields>
                        <asp:BoundField DataField="UserEmail" HeaderText="Current Email:" SortExpression="UserEmail" />
                        <asp:BoundField DataField="Address" HeaderText="Current Address:" ReadOnly="True" SortExpression="Address" />
                        <asp:CommandField ShowEditButton="True" />
                    </Fields>
                </asp:DetailsView>
                <br />
                <br />
            <br />
            <br />
            <br />
        </div>
        <br />
        <br />
        <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        <br />
        <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT [UserEmail], [Address] FROM [Users] WHERE ([UserName] = @UserName)" UpdateCommand="UPDATE Users SET Address = @newA WHERE (UserName = @UserName)">
                <SelectParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserName" />
                    <asp:ControlParameter ControlID="LabelAddr" Name="newA" PropertyName="Text" />
                </UpdateParameters>
            </asp:SqlDataSource>
        <br />
    </div>
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



