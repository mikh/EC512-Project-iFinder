<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id ="pageOther">
    
        <h1>Get new password </h1>
    
 
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="173px" HorizontalAlign="Left" Visible="False" Width="500px">
            Not used yet, hidden.<br />
            <asp:RadioButton ID="RadioButton1" runat="server" Text="I forgot my password" Visible="False" />
            <br />
            <asp:RadioButton ID="RadioButton2" runat="server" Text="I forgot my UserName" Visible="False" />
            <br />
            <asp:RadioButton ID="RadioButton3" runat="server" Text="I forgot both my User name and Password" Visible="False" />
            <br />
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" Height="173px" HorizontalAlign="Left" Width="500px">
            Please enter User name and email associated with the account<br />
            <br />
            User name:<br />
            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
            <br />
            Email:<br />
            <asp:TextBox ID="UserEmail" runat="server"></asp:TextBox>
        </asp:Panel>
        <br />
        <br />
        <asp:Button ID="bgetNewPass" runat="server" OnClick="loginButton_Click" Text="Get new password" />
        &nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="status" runat="server"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT Id, UserName, Password, Role, UserEmail FROM Users WHERE (UserName = @UserName) AND (UserEmail = @UserEmail)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserName" Name="UserName" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="UserEmail" Name="UserEmail" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    <br />
    <br />
    </div>   
</asp:Content>

