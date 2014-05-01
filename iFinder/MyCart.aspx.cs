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
        if (User.Identity.IsAuthenticated)
        {
            SqlDataSource1.SelectParameters["userName"].DefaultValue = User.Identity.Name;
            Label1.Text = User.Identity.Name + "'s Shopping Cart";
            //logged_in.Text = User.Identity.Name;
            //user_label.Visible = false;
            //userName.Visible = false;
            //passWord.Visible = false;
            //password_label.Visible = false;
            //Login.Visible = false;
            //Register.Visible = false;
            //message_label.Text = "Welcome";
            //logged_in.Visible = true;
            //bLogout.Visible = true;
            //iForgotPass.Visible = false;
        }
        else
        {
            SqlDataSource1.SelectParameters["userName"].DefaultValue = "default";     
            //user_label.Visible = true;
            //userName.Visible = true;
            //passWord.Visible = true;
            //password_label.Visible = true;
            //Login.Visible = true;
            //Register.Visible = true;
            //message_label.Text = "";
            //logged_in.Visible = false;
            //bLogout.Visible = false;
            //iForgotPass.Visible = true;
        }
    }

    protected void bBuyItems_Click(object sender, EventArgs e)
    {

    }
    protected void bUpdateCart_Click(object sender, EventArgs e)
    {

    }
    protected void gvMyCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        SqlDataSource1.DeleteParameters["id"].DefaultValue = gvMyCart.DataKeys[e.RowIndex].Value.ToString();
        SqlDataSource1.Delete();
    }

    protected void gvMyCart_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        SqlDataSource1.DeleteParameters["id"].DefaultValue = gvMyCart.DataKeys[e.RowIndex].Value.ToString();     
        SqlDataSource1.Delete();
    }
    
}