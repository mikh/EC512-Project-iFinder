<%@ Page Language="C#" MasterPageFile="~/Master1.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div>
    
        <h1>Register</h1>
    
    </div>
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
            <br />
            <br />
            <asp:Button ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Register" />
            <br />
            <br />
            <asp:Label ID="status" runat="server"></asp:Label>
            <br />
            <br />
            <asp:HiddenField ID="hashpass" runat="server" />
            <br />
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)" InsertCommand="INSERT INTO Users(UserName, Password) VALUES ( @user , @pass)">
            <SelectParameters>
                <asp:ControlParameter ControlID="UserName" Name="UserName" PropertyName="Text" Type="String" />
            </SelectParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="UserName" Name="user" PropertyName="Text" />
                <asp:ControlParameter ControlID="hashpass" Name="pass" PropertyName="Value" />
            </InsertParameters>
        </asp:SqlDataSource>


</asp:Content>
