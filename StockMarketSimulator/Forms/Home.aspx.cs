using System;
using StockMarketSimulator.Utilities;
using BusinessTier;

namespace StockMarketSimulator.Forms
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        //Login Button
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            //TextBox1 = username
            //TextBox2 = password
            var ReturnCode = Records.CheckCredentials(TextBox1.Text, TextBox2.Text);

            if (ReturnCode == -1)
                Label1.Text = "Invalid Username or Password";
            else
            {
                Session["Username"] = TextBox1.Text;
                Response.Redirect("~/Forms/UserPage.aspx");
            }
        }
        /*
        protected void Button1_Click(object sender, EventArgs e)
        {
            var biz = new Business();
            Session["User"] = biz.GetUser(TextBox1.Text, TextBox2.Text);    
        }
        */
    }
}
    
