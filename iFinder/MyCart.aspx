<%@ Page Language="C#" MasterPageFile="~/SuperMaster.Master" AutoEventWireup="true" CodeFile="MyCart.aspx.cs" Inherits="MyCart" %>


<asp:Content ID="Content1" runat="server" contentplaceholderid="items">
    <asp:Label ID="results_label" runat="server" Text=""></asp:Label><br /><br /><br />
    <asp:Repeater ID="results_repeater" runat="server" OnItemCommand="results_repeater_ItemCommand">
        <ItemTemplate>
            <tr>
                <td>
                    <%# ((List<String>)Container.DataItem)[0] %>
                </td>
                <td>
                    <%# ((List<String>)Container.DataItem)[1] %>
                </td>
                <td>
                    <%# ((List<String>)Container.DataItem)[2] %>
                </td>
                <td>
                    <%# ((List<String>)Container.DataItem)[3] %>
                </td>
            </tr>
            <br />
            <br />
        </ItemTemplate>
        <FooterTemplate>
            </table><br />
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>



