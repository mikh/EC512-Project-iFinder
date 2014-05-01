<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="UpdateAddress.aspx.cs" Inherits="Account_UpdateAddress" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div dir="auto" style="float: none">
            <h1><asp:Label ID="Label1" runat="server" Text="Update Address Information" /><br /></h1>
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
                <asp:Label ID="LabelAddr" runat="server" Visible="False"></asp:Label>
                <br />
                <asp:Button ID="bUpdate" runat="server" OnClick="bUpdate_Click" Text="Update Address" />
                <br />
                <asp:Label ID="status" runat="server" Text="Label"></asp:Label>
                <br />
                <br />
                <br />
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Account/AccountSettings.aspx">&lt;Return to Account  Settings</asp:HyperLink>
            </asp:Panel>
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
    <asp:Label ID="logged_in" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="bLogout" runat="server" OnClick="bLogout_Click" Text="Logout" style="height: 26px" />
    &nbsp;&nbsp;&nbsp;
      <asp:HyperLink ID="CartLink" runat="server" NavigateUrl="~/MyCart.aspx">My Cart</asp:HyperLink>
      &nbsp;&nbsp;&nbsp;
      <asp:HyperLink ID="account_settings" runat="server" NavigateUrl="~/Account/AccountSettings.aspx" >Account Settings</asp:HyperLink>
&nbsp;
</asp:Content>



