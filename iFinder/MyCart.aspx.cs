using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MyCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void results_repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void bBuyItems_Click(object sender, EventArgs e)
    {

    }
    protected void bUpdateCart_Click(object sender, EventArgs e)
    {
        SqlDataSource1.Delete();
    }
    protected void gvMyCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //SqlDS_Cart.DeleteParameters["id"].DefaultValue = cartCount.ToString();
        SqlDataSource1.Delete();
    }
    protected void gvMyCart_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvMyCart_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {

    }
    protected void gvMyCart_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        SqlDataSource1.DeleteParameters["id"].DefaultValue = gvMyCart.DataKeys[e.RowIndex].Value.ToString();     
        SqlDataSource1.Delete();
    }
}