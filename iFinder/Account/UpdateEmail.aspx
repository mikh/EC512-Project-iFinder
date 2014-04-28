<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="UpdateEmail.aspx.cs" Inherits="Account_UpdateEmail" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div>
         <h1><asp:Label ID="Label1" runat="server" Text="Update Email" /><br /></h1>
    <br />
    New Email Address:<br />
        <asp:TextBox ID="NewEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NewEmail" ErrorMessage="Email address is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NewEmail" Display="None" ErrorMessage="Please enter a valid email address." ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" EnableClientScript="False"></asp:RegularExpressionValidator>
        <br />
        <br />
        Password:<br />
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Password" ErrorMessage="Password is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update Email" />
        <br />
        <br />
    <br />
        <asp:Label ID="status" runat="server"></asp:Label>
        <p>
            <asp:HiddenField ID="hashpass" runat="server" />
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)" InsertCommand="INSERT INTO Users(UserName, Password, UserEmail) VALUES ( @user , @pass, @email)" UpdateCommand="UPDATE Users SET UserEmail = @newEmail WHERE (UserName = @UserName)">
            <SelectParameters>
                <asp:Parameter Name="UserName" Type="String" DefaultValue="User.Identity.Name" />
            </SelectParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="UserName" Name="user" PropertyName="Text" />
                <asp:ControlParameter ControlID="hashpass" Name="pass" PropertyName="Value" />
                <asp:ControlParameter ControlID="UserEmail" Name="email" PropertyName="Text" />
            </InsertParameters>
            <UpdateParameters>
                <asp:ControlParameter ControlID="NewEmail" DefaultValue="email" Name="newEmail" PropertyName="Text" Type="String" />
                <asp:Parameter DefaultValue="Anonymous" Type="String" Name="UserName" />
            </UpdateParameters>
        </asp:SqlDataSource>

    <br />
        <br />
    <br />
</div>
<p>
</p>
</asp:Content>



