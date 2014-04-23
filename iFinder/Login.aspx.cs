using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;


public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void loginButton_Click(object sender, EventArgs e)
    {
        try  //catches blank User name
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count == 0)
            {
                //status
            }
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Text, "SHA1");
            DataRow row = dv.Table.Rows[0];
            string temppass = (string)row["Password"];
            if (temppass == hashpass)
            {
                //authenticated
                FormsAuthentication.RedirectFromLoginPage(UserName.Text, false);
                status.Text = "Login OK.";
                return;
            }

        }
        catch
        {
            //Not authenticated
        }
        status.Text = "Login failed.";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
}