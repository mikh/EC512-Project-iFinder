<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master"  AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="Account_UpdatePassword" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div>
    <br />
    Old
        Password:<br />
        <asp:TextBox ID="OldPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="OldPassword" ErrorMessage="Password is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <br />
    <br />
    New
        Password:<br />
        <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewPassword" ErrorMessage="Password is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <br />
    <br />
    Reenter new
        Password:<br />
        <asp:TextBox ID="NewPassword2" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="NewPassword2" ErrorMessage="Password is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="NewPassword2" ErrorMessage="Passwords do not match" SetFocusOnError="True"></asp:CompareValidator>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update Password" />
        <br />
        <br />
        <asp:Label ID="status" runat="server"></asp:Label>
        <br />
        <p>
            <asp:HiddenField ID="hashpass" runat="server" />
        </p>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringUser %>" SelectCommand="SELECT * FROM [Users] WHERE ([UserName] = @UserName)" InsertCommand="INSERT INTO Users(UserName, Password, UserEmail) VALUES ( @user , @pass, @email)" UpdateCommand="UPDATE Users SET Password = @newPass WHERE (UserName = @UserName)">
            <SelectParameters>
                <asp:Parameter Name="UserName" Type="String" DefaultValue="Anonymous" />
            </SelectParameters>
            <InsertParameters>
                <asp:ControlParameter ControlID="UserName" Name="user" PropertyName="Text" />
                <asp:ControlParameter ControlID="hashpass" Name="pass" PropertyName="Value" />
                <asp:ControlParameter ControlID="UserEmail" Name="email" PropertyName="Text" />
            </InsertParameters>
            <UpdateParameters>
                <asp:ControlParameter ControlID="NewPassword2" DefaultValue="password" Name="newPass" PropertyName="Text" Type="String" />
                <asp:Parameter DefaultValue="Anonymous" Type="String" Name="UserName" />
            </UpdateParameters>
        </asp:SqlDataSource>

    <br />
    <br />
</div>
</asp:Content>


