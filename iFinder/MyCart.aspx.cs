using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;

public partial class MyCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            SqlDataSource1.SelectParameters["userName"].DefaultValue = User.Identity.Name;
            Label1.Text = User.Identity.Name + "'s Shopping Cart";
            logged_in.Text = User.Identity.Name;
            user_label.Visible = false;
            userName.Visible = false;
            passWord.Visible = false;
            password_label.Visible = false;
            Login.Visible = false;
            Register.Visible = false;
            message_label.Text = "Welcome";
            logged_in.Visible = true;
            bLogout.Visible = true;
            iForgotPass.Visible = false;
        }
        else
        {
            SqlDataSource1.SelectParameters["userName"].DefaultValue = "default";
            user_label.Visible = true;
            userName.Visible = true;
            passWord.Visible = true;
            password_label.Visible = true;
            Login.Visible = true;
            Register.Visible = true;
            message_label.Text = "";
            logged_in.Visible = false;
            bLogout.Visible = false;
            iForgotPass.Visible = true;
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

    protected void Login_Click(object sender, EventArgs e)
    {
        try  //catches blank User name
        {
            DataView dv = (DataView)SqlDataSource2.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count == 0)
            {
                //status
            }
            string hashpass = FormsAuthentication.HashPasswordForStoringInConfigFile(passWord.Text, "SHA1");
            DataRow row = dv.Table.Rows[0];
            string temppass = (string)row["Password"];
            if (temppass == hashpass)
            {
                //authenticated
                FormsAuthentication.RedirectFromLoginPage(userName.Text, false);
                message_label.Text = "Login OK.";
                Session["force_postback"] = true;
                return;
            }

        }
        catch
        {
            //Not authenticated
            message_label.Text = "Error with authentication.";
        }
        message_label.Text = "Login failed.";
    }
    protected void Register_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
    protected void bLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("MyCart.aspx");
    }
    protected void gvMyCart_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Qchanged(object sender, EventArgs e)
    {
        int newQ = 1;
        foreach (GridViewRow row in gvMyCart.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                TextBox qbox = row.FindControl("qbox") as TextBox;
            }
        }
        //GridViewRow row = gvMyCart.Rows[e.RowIndex];
        //TextBox qbox = (TextBox)row.FindControl("qbox");  
        string connString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\UserTemp.mdf;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();

        string updateString = "UPDATE [MyCart] SET [Quantity] = @newQ WHERE [UserName] = @user";
       
        //newQ = Convert.ToInt32(qbox.Text);

        SqlCommand cmd = new SqlCommand(updateString, conn);
        cmd.Parameters.AddWithValue("@newQ", newQ);
        if (User.Identity.IsAuthenticated)
        {
            cmd.Parameters.AddWithValue("@user", User.Identity.Name);
        }
        else
        {
            cmd.Parameters.AddWithValue("@user", "default");
        }

        cmd.ExecuteNonQuery();
        conn.Close();
    }
}