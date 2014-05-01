using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_AccountSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (User.Identity.IsAuthenticated)
        {
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
            //Labelcemail.Text = User.Identity.Name;
            SqlDataSource1.SelectParameters["UserName"].DefaultValue = User.Identity.Name;
        }
        else
        {
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
    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }

}