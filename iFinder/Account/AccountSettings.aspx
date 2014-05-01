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
                <asp:HyperLink ID="HyperLink4" runat="server">Update Address Information</asp:HyperLink>
            <br />
            <br />
            <br />
            <br />
            <br />
                More stuff here:<br />
                <br />
                <asp:Label ID="Labelcemail" runat="server"></asp:Label>
                <br />
                <br />
                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="SqlDataSource1" Height="50px" Width="243px">
                    <Fields>
                        <asp:BoundField DataField="UserEmail" HeaderText="Current Email:" SortExpression="UserEmail" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT [UserEmail] FROM [Users] WHERE ([UserName] = @UserName)">
                <SelectParameters>
                    <asp:Parameter Name="UserName" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        <br />
    </div>
</asp:Content>


