<%@ Page Language="C#" MasterPageFile="~/SuperMaster.master" AutoEventWireup="true" CodeFile="ProductDescription.aspx.cs" Inherits="ProductDescription" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="items">
    <div id="pageOther">
        
        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1" Height="194px" Style="margin-left: 38px" Width="387px">
            <ItemTemplate>
                <div>
                    <h1><asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>' /><br /></h1>
                </div>
                <br />
                <table>
                    <tr>
                        <td class="auto-style2">
                            <%--<img src="/Catalog/Images/<%#:Item.ImagePath %>" style="border: solid; height: 300px" alt="<%#:Item.ProductName %>" />--%>

                            <asp:Image ID="Image1" runat="server" Height="84px" ImageUrl="<%$ ConnectionStrings:ConnectionString %>" Width="203px" style="margin-top: 0px" />

                        </td>
                        <td class="auto-style2"></td>
                        <td style="vertical-align: top; text-align: left;" class="auto-style2">
                            <b>Description:</b><br />
                            <%--<%#:Item.Description %>
                            <br />
                            <span><b>Price:</b>&nbsp;<%#: String.Format("{0:c}", Item.UnitPrice) %></span><br /><span><b>Product Number:</b>&nbsp;<%#:Item.ProductID %></span><br />--%></td>
                    </tr>
                </table>
                <br />
                <div style="height: 220px">

                    <br />
                    <table>
                        <tr>
                            <td>

                            </td>
                            <td style="vertical-align: top; text-align: left;">
                                ID:
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Bind("ID") %>' />
                                <br />
                                manufacturer:
                                <asp:Label ID="manufacturerLabel" runat="server" Text='<%# Bind("manufacturer") %>' />
                                <br />
                                quantity:
                                <asp:Label ID="quantityLabel" runat="server" Text='<%# Bind("quantity") %>' />
                                <br />
                                availability:
                                <asp:Label ID="availabilityLabel" runat="server" Text='<%# Bind("availability") %>' />
                                <br />
                                price:
                                <asp:Label ID="priceLabel" runat="server" Text='<%# Bind("price") %>' />
                                <br />
                                minimum_quantity:
                                <asp:Label ID="minimum_quantityLabel" runat="server" Text='<%# Bind("minimum_quantity") %>' />
                                <br />
                                packaging:
                                <asp:Label ID="packagingLabel" runat="server" Text='<%# Bind("packaging") %>' />
                                <br />
                                series:
                                <asp:Label ID="seriesLabel" runat="server" Text='<%# Bind("series") %>' />
                                <br />
                                resistance_ohms:
                                <asp:Label ID="resistance_ohmsLabel" runat="server" Text='<%# Bind("resistance_ohms") %>' />
                                <br />
                                tolerance:
                                <asp:Label ID="toleranceLabel" runat="server" Text='<%# Bind("tolerance") %>' />
                                <br />
                                power_watts:
                                <asp:Label ID="power_wattsLabel" runat="server" Text='<%# Bind("power_watts") %>' />
                                <br />
                                items_Id:
                                <asp:Label ID="items_IdLabel" runat="server" Text='<%# Bind("items_Id") %>' />
                            </td>
                        </tr>
                    </table>

                </div>
                <br />

            </ItemTemplate>
        </asp:FormView>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [electronics_resistor_table]"></asp:SqlDataSource>
        <br />
        <br />
    </div>
</asp:Content>


<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 315px;
        }
        .auto-style2 {
            height: 346px;
        }
    </style>
</asp:Content>



