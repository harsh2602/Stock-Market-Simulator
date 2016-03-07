using System;
using StockMarketSimulator.Utilities;

namespace StockMarketSimulator.Forms
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3.Text == "" || TextBox4.Text == "" || TextBox5.Text == "" || TextBox6.Text == "")
            {
                Label1.Text = "No Empty Fields allowed";
            }
            else if (TextBox4.Text == TextBox5.Text)
            {
                var result = Records.CreateNewRecord(TextBox4.Text, TextBox6.Text, TextBox1.Text, TextBox2.Text, TextBox3.Text, 10000);
                if (result == -1)
                    Label1.Text = "Username already in Use. Please try some other Username";
                else
                {
                    Response.Redirect("~/Forms/Home.aspx");
                }
            }
            else
            {
                Label1.Text = "Passwords do not match!";
            }
        }

        protected void Button2_Click(object sender, EventArgs e) //Button Click to go back to home
        {
            Response.Redirect("~/Forms/Home.aspx");
        }
    }
}