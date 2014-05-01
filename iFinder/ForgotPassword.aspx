<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id ="pageOther">
    
        <h1>Get new password </h1>
        <p>&nbsp;</p>
    
 
            <asp:RadioButton ID="RadioButton1" runat="server" Text="Email me a new password" GroupName="getpass" OnCheckedChanged="RadioButton1_CheckedChanged" Checked="True" />
            <br __designer:mapid="1052" />
            <asp:RadioButton ID="RadioButton2" runat="server" Text="I want to solve my Security questions" GroupName="getpass" OnCheckedChanged="RadioButton2_CheckedChanged" />
            <br __designer:mapid="1054" />
    
 
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" Height="173px" HorizontalAlign="Left" Width="500px">
            Please enter email associated with the account<br />
            <br />
            <br />
            Email:<br />
            <asp:TextBox ID="UserEmail" runat="server" TextMode="Email"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UserEmail" Display="None" ErrorMessage="Please enter a valid email address." ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" EnableClientScript="False"></asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="status" runat="server"></asp:Label>
            <br />
            <asp:Button ID="bgetNewPass" runat="server" OnClick="loginButton_Click" Text="Get new password" />
        </asp:Panel>

        <asp:Panel ID="Panel2" runat="server" Height="173px" HorizontalAlign="Left" Width="500px">
            <br />
            <br />
            <br />
            What was the name of your pet?<br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get new Password" />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </asp:Panel>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT Id, UserName, Password, Role, UserEmail FROM Users WHERE (UserName = @UserName) AND (UserEmail = @UserEmail)">
            <SelectParameters>
                <asp:Parameter Name="UserName" Type="String" />
                <asp:ControlParameter ControlID="UserEmail" Name="UserEmail" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    <br />
        <br />
    <br />
    </div>   
</asp:Content>

