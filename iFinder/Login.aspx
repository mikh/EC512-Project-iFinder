<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id ="pageOther">
    
        <h1>Login</h1>
    
 
        User name:<br />
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="UserName" ErrorMessage="Username is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <br />
        <br />
        Password:<br />
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Password" ErrorMessage="Password is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="loginButton_Click" ValidationGroup="RequiredFields" Text="Login" />
        &nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="iForgotPassword" runat="server" NavigateUrl="~/ForgotPassword.aspx">Forgot Password?</asp:HyperLink>
        <br />
        <br />
        <asp:Label ID="status" runat="server"></asp:Label>
        <br />
        <div>
            <br />
            
        </div>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserName" Name="UserName" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Register" />
        <br />
        <br />
    <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">&lt;Return to homepage</asp:HyperLink>
    <br />
    </div>   
</asp:Content>

