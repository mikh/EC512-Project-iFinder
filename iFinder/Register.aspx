<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id ="pageOther" style="align-items:center; text-align:left;">
    
        <h1>Register</h1>
    

        <p>
            User name:<br />
            <asp:TextBox ID="UserName" runat="server" Width="146px"></asp:TextBox>
            <br />
            <br />
            Password:<br />
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            Re-enter Password:<br />
            <asp:TextBox ID="RPassword" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            Email:</p>
        <p>
            <asp:TextBox ID="UserEmail" runat="server" TextMode="Email"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UserEmail" Display="None" ErrorMessage="Please enter a valid email address." ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" EnableClientScript="False"></asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Button ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Register" /><br /><br />
            <asp:Button ID="cancelBtn" runat="server" Text="Cancel" OnClick="cancelBtn_Click" />
            <br />
            <br />
            <asp:Label ID="status" runat="server"></asp:Label>
        </p>
        <p>
             <br />
            <br />
            <asp:HiddenField ID="hashpass" runat="server" />
            <br />
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)" InsertCommand="INSERT INTO Users(UserName, Password, UserEmail) VALUES ( @user , @pass, @email)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserName" Name="UserName" PropertyName="Text" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="UserName" Name="user" PropertyName="Text" />
                <asp:ControlParameter ControlID="hashpass" Name="pass" PropertyName="Value" />
                <asp:ControlParameter ControlID="UserEmail" Name="email" PropertyName="Text" />
            </InsertParameters>
        </asp:SqlDataSource>

    </div>
</asp:Content>
