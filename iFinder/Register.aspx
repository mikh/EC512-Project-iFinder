<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div id ="pageOther" style="align-items:center; text-align:center;">
    
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
            <asp:TextBox ID="UserEmail" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Register" /><br /><br />
            <asp:Button ID="cancelBtn" runat="server" Text="Cancel" OnClick="cancelBtn_Click" />
            <br />
            <br />
            <asp:Label ID="status" runat="server"></asp:Label>
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
