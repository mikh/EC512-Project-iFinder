<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="UpdateEmail.aspx.cs" Inherits="Account_UpdateEmail" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div>
    <br />
    Old Email Address:<br />
        <asp:TextBox ID="OldEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="OldEmail" ErrorMessage="Email address is required" ValidationGroup="RequiredFields"></asp:RequiredFieldValidator>
        <br />
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
</div>
<p>
</p>
</asp:Content>



