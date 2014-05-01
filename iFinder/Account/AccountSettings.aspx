﻿<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="AccountSettings.aspx.cs" Inherits="Account_AccountSettings" %>

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
            <asp:Panel ID="Panel1" runat="server" Height="343px">
                <br />
                <br />
                Street Address:
                <asp:TextBox ID="tbAddress" runat="server" Width="421px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbAddress" ErrorMessage="Please enter a street address"></asp:RequiredFieldValidator>
                <br />
                City:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbCity" runat="server" Width="139px"></asp:TextBox>
                &nbsp;&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbCity" ErrorMessage="Please enter a City"></asp:RequiredFieldValidator>
                <br />
                State:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbState" runat="server" Width="114px"></asp:TextBox>
                &nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbState" ErrorMessage="please enter a state"></asp:RequiredFieldValidator>
                <br />
                ZipCode:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="tbZip" runat="server" Width="119px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbZip" ErrorMessage="please enter zipcode"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label ID="LabelAddr" runat="server"></asp:Label>
                <br />
                <asp:Button ID="bUpdate" runat="server" OnClick="bUpdate_Click" Text="Update Address" />
                <br />
                <br />
            </asp:Panel>
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


