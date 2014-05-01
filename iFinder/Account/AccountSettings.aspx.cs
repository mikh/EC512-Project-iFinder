﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
public partial class Account_AccountSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       if (User.Identity.IsAuthenticated)
        {
            logged_in.Text = User.Identity.Name;
            message_label.Text = "Welcome";
            bLogout.Visible = true;
            //Labelcemail.Text = User.Identity.Name;
            SqlDataSource1.SelectParameters["UserName"].DefaultValue = User.Identity.Name;
        }
   
    }
    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }

    protected void bLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Default.aspx");
    }
}