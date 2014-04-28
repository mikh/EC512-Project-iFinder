using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;


public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /*
    public void SendEmail(string uEmail)
    {
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(uEmail);
        mail.From = new MailAddress("from gmail address", "Email head", System.Text.Encoding.UTF8);
        mail.Subject = "Your new iFinder Password";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "Your new password is:";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("from gmail address", "your gmail account password");
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
        try
        {
            client.Send(mail);
            Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
            Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');if(alert){ window.location='SendMail.aspx';}</script>");
        }
    }
    */
    protected void loginButton_Click(object sender, EventArgs e)
    {
        try  //catches blank User name
        {
            DataView dv = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            if (dv.Table.Rows.Count == 0)
            {
                //status
            }
            string uEmail = UserEmail.Text;
            DataRow row = dv.Table.Rows[0];
            string temp = (string)row["Email"];
            if (temp == uEmail)
            {

                status.Text = "A new password has been sent to the address above.";
                return;
            }

        }
        catch
        {
            //Not authenticated
            status.Text = "Error with authentication.";
        }
        status.Text = "an email will be sent to the above email should it match with an existing account.";
    }
}