<%@ Page Title="" Language="C#" MasterPageFile="~/Master1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    This is coming from a content page (page 1)

    <asp:Label ID="debug" runat="server" Text="Label"></asp:Label>

    <asp:ListBox ID="list_debug" runat="server" Height="390px" Width="302px"></asp:ListBox>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [electronics_resistor_table]"></asp:SqlDataSource>
</asp:Content>



