using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class Account_UpdatePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.SelectParameters["UserName"].DefaultValue = User.Identity.Name;
        SqlDataSource1.UpdateParameters["UserName"].DefaultValue = User.Identity.Name;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       // SqlDataSource1.Select(DataSourceSelectArguments.Empty);
       // status.Text = "";
        try  //catches blank User name
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count == 0)
            {
                //status
                status.Text = User.Identity.Name;
            }
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(OldPassword.Text, "SHA1");
            DataRow row = dv.Table.Rows[0];
            string temppass = (string)row["Password"];
            if (temppass == hashpass)
            {
                //authenticated
                FormsAuthentication.SignOut();
                string hashpass0 = FormsAuthentication.HashPasswordForStoringInConfigFile(NewPassword.Text, "SHA1");
                SqlDataSource1.Update();
                Response.Redirect("UpdatePassword.aspx");
                status.Text = "Password Updated."; //not done yet, inc

            }

         }
        catch
        {
            //Not authenticated
            status.Text = "Error with authentication.";
        }
     //   status.Text = "Unable to update password.";
    }
}