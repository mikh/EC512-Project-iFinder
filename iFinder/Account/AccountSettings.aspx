<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="AccountSettings.aspx.cs" Inherits="Account_AccountSettings" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div dir="auto" style="float: none">
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <div style="height: 178px">
            <div dir="ltr" style="float: left; width: 481px; right: 6px; height: 105px;">
                Inside Div on the left.
                <br />
            </div>
            Div on the right<br />
            <br />
            <br />
            Neeed to figure out content<br />
            <br />
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/UpdateEmail.aspx">Update Email Address</asp:HyperLink>
            <br />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Account/UpdatePassword.aspx">Change Password</asp:HyperLink>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>


